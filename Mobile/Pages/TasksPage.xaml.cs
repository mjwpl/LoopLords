using CommunityToolkit.Maui.Views;
using Mobile.Data.Models;
using Mobile.Helpers;
using Mobile.ViewModel;
using System.ComponentModel;

namespace Mobile.Pages;

public partial class TasksPage : ContentPage
{
    public readonly TasksViewModel tasksView;

    public TasksPage(TasksViewModel tvm)
    {
        InitializeComponent();
        tasksView = tvm;
        BindingContext = tasksView;
        tasksView.PropertyChanged += OnPropertyChanged;
        NavigationPage.SetHasNavigationBar(this, false);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await tasksView.LoadItems();
        SetButtonColors();
    }

    private async void AddTask(object sender, EventArgs e)
    {
        var result = await this.ShowPopupAsync(new AddPage(), CancellationToken.None);

        var item = result as Item;
        if (item is not null)
        {
            await tasksView.AddItem(item);
            await tasksView.LoadItems();
        }
        else return;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(tasksView.ItemsList))
        {
            colv.ItemsSource = null;
            colv.ItemsSource = tasksView.ItemsList;
        }

        if (e.PropertyName == nameof(tasksView.AvailableColors))
        {
            SetButtonColors();
        }
    }

    private void SetButtonColors()
    {
        ColorFilter.Children.Clear();

        var availableColors = tasksView.AvailableColors;

        if (availableColors.Count() > 1)
        {
            var all = new Button
            {
                Padding = 0,
                BorderColor = Colors.Gray,
                BorderWidth = 1,
                BackgroundColor = Colors.Black,
                CornerRadius = 20,
                FontFamily = "MaterialIcons",
                FontSize = 20,
                HeightRequest = 30,
                Text = MaterialIcons.Clear,
                TextColor = Colors.White,
                WidthRequest = 30
            };
            all.Clicked += FilterButtonClicked;
            ColorFilter.Children.Add(all);

            foreach (var color in availableColors)
            {
                var text = String.Empty;
                if (tasksView.FilterColor == color) text = MaterialIcons.Check;
                else text = String.Empty;

                var filter = new Button
                {
                    Padding = 0,
                    BackgroundColor = Color.FromArgb(color),
                    CornerRadius = 20,
                    FontFamily = "MaterialIcons",
                    FontSize = 20,
                    HeightRequest = 30,
                    Text = text,
                    TextColor = Colors.White,
                    WidthRequest = 30,
                    
                };

                filter.Clicked += FilterButtonClicked;

                ColorFilter.Children.Add(filter);
            }
        }
    }

    private async void FilterButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button button)
        {
            tasksView.FilterColor = button.BackgroundColor?.ToHex() ?? string.Empty;
        }
        await tasksView.LoadItems();
        SetButtonColors();
    }
}