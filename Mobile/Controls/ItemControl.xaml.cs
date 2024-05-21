using CommunityToolkit.Maui.Extensions;
using Mobile.Data.Models;
using Mobile.Helpers;
using Mobile.ViewModel;

namespace Mobile.Controls;

public partial class ItemControl : ContentView
{
    private bool isExpanded = false;
    private string _highlightColor = "#e37138";
    private const int _collapseHeight = 80;
    private const int _expandedHeight = 300;
    private const uint _animationDuration = 800;
    private Easing _easing = Easing.CubicOut;

    private DoneDate _selectedDate;

    public TasksViewModel TasksViewModel;

    public ItemControl()
    {
        InitializeComponent();
        _selectedDate = new DoneDate { RealDate = DateTime.Now };
        DatePicker.DateSelected += (s, e) =>
        {
            _selectedDate.RealDate = e.NewDate;
            DoneButton.Text = $"Done {_selectedDate.DisplayDate}";
        };
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        TasksViewModel = Application.Current.Handler.MauiContext.Services.GetService<TasksViewModel>();
        DoneButton.Text = $"Done {_selectedDate.DisplayDate}";
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        TasksViewModel = Application.Current.Handler.MauiContext.Services.GetService<TasksViewModel>();

        DatePicker.MaximumDate = DateTime.Now;

        if (BindingContext is Item item)
        {
            if (item.DaysSinceLastOccurrence != null && item.DaysSinceLastOccurrence > 0)
            {
                DatePicker.MinimumDate = DateTime.Now.AddDays(-(int)item.DaysSinceLastOccurrence + 1);
            }
        }
       
    }


    private void Button_Clicked(object sender, EventArgs e)
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

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Item item)
        {
            await TasksViewModel.DeleteItem(item);
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

}
