using Mobile.Helpers;
using Mobile.ViewModel;

namespace Mobile.Pages;

public partial class TasksPage : ContentPage
{
	private readonly TasksViewModel _vm;

	public TasksPage()
	{
		InitializeComponent();
        _vm = ServiceHelper.GetService<TasksViewModel>();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadItems();
    }
}