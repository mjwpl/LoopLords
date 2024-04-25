using Mobile.Helpers;
using Mobile.ViewModel;

namespace Mobile.Pages;

public partial class NewPage : ContentPage
{
    private NewViewModel _vm;

    public NewPage()
	{
		InitializeComponent();
        _vm = ServiceHelper.GetService<NewViewModel>();
        BindingContext = _vm;
    }
}