using Mobile.ViewModel;

namespace Mobile.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}
}