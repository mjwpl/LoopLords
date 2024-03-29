using Mobile.View.IntroSubView;
using Mobile.ViewModel;
using System.Runtime.CompilerServices;

namespace Mobile.View;

public partial class IntroView : ContentPage
{
    private List<ContentView> _pages = new List<ContentView> { new IntroSubViewOne() };

    public IntroView()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        carousel.ItemsSource = _pages;

        // Tworzenie kropek do sygnalizowania stron
        for (int i = 0; i < _pages.Count; i++)
        {
            var dotLabel = new Label
            {
                Text = "●",
                FontSize = 25,

                TextColor = (i == 0) ? Colors.Green : Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Colors.Transparent,
                WidthRequest = 30,
                HeightRequest = 30,
                //CornerRadius = 15,
                Margin = new Thickness(8, 0)
            };
            dotsLayout.Children.Add(dotLabel);
        }

        // Ustawianie kropki dla aktualnej strony
        carousel.PositionChanged += async (sender, e) =>
        {
            for (int i = 0; i < dotsLayout.Children.Count; i++)
            {
                ((Label)dotsLayout.Children[i]).TextColor = (i == e.CurrentPosition) ? Colors.Green : Colors.Black;
            }

            var _currentPageIndex = e.CurrentPosition;


        };
    }
}