using Microsoft.Extensions.DependencyInjection;
using Mobile.Helpers;

namespace Mobile.Pages;

public partial class MainPage : TabbedPage
{
    public NavigationPage TasksPage { get; set; } = new();
    public SettingsPage SettingsPage { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        TasksPage.Title = "Tasks";
        TasksPage.IconImageSource = new FontImageSource
        {
            FontFamily = "MaterialIcons",
            Glyph = MaterialIcons.Checklist
        };
        Children.Add(TasksPage);

        SettingsPage.Title = "Settings";
        SettingsPage.IconImageSource = new FontImageSource
        {
            FontFamily = "MaterialIcons",
            Glyph = MaterialIcons.Settings
        };
        Children.Add(SettingsPage);
    }
}
