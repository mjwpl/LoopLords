using Mobile.ViewModel;
using System.Diagnostics;

namespace Mobile.View;

public partial class ItemListView : ContentPage
{
    private ItemListViewModel _vm;

    public ItemListView(ItemListViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _vm = vm;

	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadItems();
    }
}