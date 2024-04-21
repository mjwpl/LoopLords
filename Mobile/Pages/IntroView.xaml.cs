using Mobile.Pages.IntroSubView;

namespace Mobile.Pages;

public partial class IntroView : ContentPage
{
    private List<ContentView> _pages = new List<ContentView> { new IntroSubViewOne() };

    public IntroView()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        carousel.ItemsSource = _pages;

       
    }
}