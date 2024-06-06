using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Mobile.Data.Models;
using Mobile.Localize;
using Mobile.Pages;
using Mobile.ViewModel;

namespace Mobile.Controls;

public partial class ItemControl : ContentView
{
    private bool isExpanded = false;
    private string _highlightColor = "#e37138";
    private const int _collapseHeight = 80;
    private const int _expandedHeight = 300;
    private const uint _animationDuration = 800;
    private readonly Easing _easing = Easing.CubicOut;

    private readonly DoneDate _selectedDate;

    public TasksViewModel TasksViewModel;

    public ItemControl()
    {
        InitializeComponent();
        _selectedDate = new DoneDate { RealDate = DateTime.Now };
        DatePicker.DateSelected += (s, e) =>
        {
            _selectedDate.RealDate = e.NewDate;
            //DoneButton.Text = $"{_selectedDate.DisplayDate}";
        };
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        TasksViewModel = Application.Current.Handler.MauiContext.Services.GetService<TasksViewModel>();
        //DoneButton.Text = $"{_selectedDate.DisplayDate}";
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        TasksViewModel = Application.Current.Handler.MauiContext.Services.GetService<TasksViewModel>();

        DatePicker.MaximumDate = DateTime.Now;

        if (BindingContext is Item item)
        {
            _highlightColor = item.Color;
            //if (!item.IsVisibleBtn) DoneButton.Text = "Well done";
            if (item.DaysSinceLastOccurrence != null && item.DaysSinceLastOccurrence > 0)
            {
                DatePicker.MinimumDate = DateTime.Now.AddDays(-(int)item.DaysSinceLastOccurrence + 1);
            }
        }
       
    }

    private void Chevron_Clicked(object sender, EventArgs e)
    {
        isExpanded = !isExpanded;

        if (isExpanded)
        {
            Expand();
        }
        else
        {
            Collapse();
        }

    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item bindingItem)
        {
            var page = new AddPage(bindingItem);

            var parentPage = FindParentPage(this);
            if (parentPage != null)
            {
                var result = await PopupExtensions.ShowPopupAsync(parentPage, page, CancellationToken.None);

                var item = result as Item;
                if (item != null)
                {
                    await TasksViewModel.UpdateItem(item);
                    await TasksViewModel.LoadItems();
                }
            }
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item item)
        {
            bool result = await Application.Current.MainPage.DisplayAlert(AppRes.POPUP_CONFIRMATION_TITLE, AppRes.POPUP_CONFIRMATION_CONTENT, AppRes.YES, AppRes.CANCEL);
            if (result) await TasksViewModel.DeleteItem(item);
        }
    }

    private async void Done_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item item)
        {
            item.History.Add(new ItemHistory { Done = _selectedDate.RealDate });
            await TasksViewModel.UpdateItem(item);
        }
    }

    private async void History_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item item)
        {
            var historyPage = Application.Current.Handler.MauiContext.Services.GetService<HistoryPage>();
            historyPage.Item = item;
            await Navigation.PushAsync(historyPage);
        }
    }

    private async void Icon_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item item)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            var iconPickerPage = new IconPickerPage(taskCompletionSource);
            await Navigation.PushAsync(iconPickerPage);

            var selectedIcon = await taskCompletionSource.Task;

            CircleText.Text = selectedIcon;
            item.Icon = selectedIcon;
            await TasksViewModel.UpdateItem(item);
        }
    }

    private void Expand()
    {
        Ramka.Animate("Expand", length: _animationDuration, animation: new Animation(x => Ramka.HeightRequest = x, _collapseHeight, _expandedHeight, easing: _easing));
        Ramka.BackgroundColorTo(Color.FromArgb(_highlightColor), length: _animationDuration, easing: _easing);
        Circle.BackgroundColorTo(Color.FromArgb("#ffffff"), length: _animationDuration, easing: _easing);
        CircleText.TextColorTo(Color.FromArgb(_highlightColor), length: _animationDuration, easing: _easing);
        Chevron.RotateTo(180, _animationDuration, _easing);
        Description.TextColorTo(Color.FromArgb("#ffffff"), length: _animationDuration, easing: _easing);
    }

    private void Collapse()
    {
        Ramka.Animate("Expand", length: _animationDuration, animation: new Animation(x => Ramka.HeightRequest = x, _expandedHeight, _collapseHeight, easing: _easing));
        Ramka.BackgroundColorTo(Color.FromArgb("#161c2c"), length: _animationDuration, easing: _easing);
        Circle.BackgroundColorTo(Color.FromArgb(_highlightColor), length: _animationDuration, easing: _easing);
        CircleText.TextColorTo(Color.FromArgb("#ffffff"), length: _animationDuration, easing: _easing);
        Chevron.RotateTo(0, _animationDuration, _easing);
        Description.TextColorTo(Color.FromArgb("#9a9ea4"), length: _animationDuration, easing: _easing);
    }

    private void FastCollapse()
    {
        Ramka.HeightRequest = _collapseHeight;
        Ramka.BackgroundColor = Color.FromArgb("#161c2c");
        Circle.BackgroundColor = Color.FromArgb(_highlightColor);
        CircleText.TextColor = Color.FromArgb("#ffffff");
        Chevron.Rotation = 0;
        Description.TextColor = Color.FromArgb("#9a9ea4");
        isExpanded = false;
    }

    private Page FindParentPage(Element element)
    {
        while (element != null)
        {
            if (element is Page page)
            {
                return page;
            }
            element = element.Parent;
        }
        return null;
    }
}
