using Mobile.Pages.IntroSubView;
using Mobile.ViewModel;

namespace Mobile.Pages;

public partial class IntroView : ContentPage
{
    private readonly TasksViewModel _vm;
    private List<ContentView> _pages = new List<ContentView>();

    public IntroView(TasksViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        NavigationPage.SetHasNavigationBar(this, false);

        carousel.ItemsSource = _pages;
        _pages.Add(new IntroSubViewOne(_vm));

    }
}