using CommunityToolkit.Maui.Views;
using Microsoft.Extensions.DependencyInjection;
using Mobile.Helpers;

namespace Mobile.Pages;

public partial class MainPage : TabbedPage
{
    private NavigationPage _tasksPage { get; set; }
    private SettingsPage _settingsPage { get; set; }
    private IconPickerPage _iconPage { get; set; }

    public MainPage(TasksPage tasksPage, SettingsPage settingsPage)
    {
        InitializeComponent();

        _tasksPage = new NavigationPage(tasksPage);
        _settingsPage = settingsPage;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _tasksPage.Title = "Tasks";
        _tasksPage.BackgroundColor = Color.FromArgb("#090d19");
        _tasksPage.IconImageSource = new FontImageSource
        {
            FontFamily = "MaterialIcons",
            Glyph = MaterialIcons.Checklist
        };
        Children.Add(_tasksPage);

        _settingsPage.Title = "Settings";
        _settingsPage.BackgroundColor = Color.FromArgb("#090d19");
        _settingsPage.IconImageSource = new FontImageSource
        {
            FontFamily = "MaterialIcons",
            Glyph = MaterialIcons.Settings
        };
        Children.Add(_settingsPage);

    }    
}