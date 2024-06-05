using Mobile.Data.Models;
using Mobile.Helpers;
using System.Globalization;

namespace Mobile.Controls
{
    public partial class RenderHistory : ContentView
    {
        public static readonly BindableProperty ItemProperty =
            BindableProperty.Create(nameof(Item), typeof(Item), typeof(Item));

        public Item Item
        {
            get => (Item)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public RenderHistory()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            WeeksStack.Children.Clear(); // Czyszczenie poprzednich elementów

            if (BindingContext is Item item)
            {
                Item = item;
                RenderHistoryTest(Item.History);
            }
        }

        public void RenderHistoryTest(List<ItemHistory> history)
        {
            var culture = CultureInfo.CurrentCulture;
            var today = DateTime.Today;

            var firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;
            var daysToFirstDayOfWeek = ((int)today.DayOfWeek - (int)firstDayOfWeek + 7) % 7;

            var startDate = today.AddDays(-21 - daysToFirstDayOfWeek);

            StackLayout stackLayout = new StackLayout();

            var earliestHistoryDate = history.Any() ? history.Min(item => item.Done.Date) : (DateTime?)null;

            for (int i = 0; i < 4; i++)
            {
                var weekLayout = new HorizontalStackLayout();

                for (int j = 0; j < 7; j++)
                {
                    var date = startDate.AddDays(i * 7 + j);

                    var isInHistory = history.Any(item => item.Done.Date == date.Date);

                    var isPast = date < today;
                    var isToday = date == today;
                    var isFuture = date > today;

                    var label = new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    };

                    if (isInHistory)
                    {
                        label.Text = MaterialIcons.Check_circle;
                    }
                    else if (earliestHistoryDate.HasValue && date < earliestHistoryDate.Value)
                    {
                        label.Text = MaterialIcons.Radio_button_unchecked;
                    }
                    else if (!earliestHistoryDate.HasValue && isPast)
                    {
                        label.Text = MaterialIcons.Radio_button_unchecked;
                    }
                    else if (isPast)
                    {
                        label.Text = MaterialIcons.Circle;
                    }
                    else if (isToday)
                    {
                        label.Text = MaterialIcons.Today;
                    }
                    else if (isFuture)
                    {
                        label.Text = MaterialIcons.Radio_button_unchecked;
                    }
                    else
                    {
                        label.Text = MaterialIcons.Circle;
                    }

                    label.FontFamily = "MaterialIcons";
                    weekLayout.Children.Add(label);
                }

                WeeksStack.Children.Add(weekLayout);
            }
        }
    }
}
