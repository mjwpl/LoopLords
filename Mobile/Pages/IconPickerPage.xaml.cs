using Microsoft.Maui.Controls;
using Mobile.Helpers;
using System.Collections.ObjectModel;

namespace Mobile.Pages
{
    public partial class IconPickerPage : ContentPage
    {
        private readonly TaskCompletionSource<string> _taskCompletionSource;
        private ObservableCollection<IconItem> _filteredIcons;
        private CollectionView collectionView;
        public ObservableCollection<IconItem> Icons { get; private set; }

        public IconPickerPage(TaskCompletionSource<string> taskCompletionSource)
        {
            InitializeComponent();
            _taskCompletionSource = taskCompletionSource;
            Icons = new ObservableCollection<IconItem>(GetMaterialIcons());
            _filteredIcons = new ObservableCollection<IconItem>(Icons);
            BindingContext = this;
            InitializeCollectionView();
        }

        private void InitializeCollectionView()
        {
            var searchEntry = new Entry
            {
                Placeholder = "Search icon (english only)",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            searchEntry.TextChanged += OnSearchTextChanged;

            collectionView = new CollectionView
            {
                ItemsLayout = new GridItemsLayout(6, ItemsLayoutOrientation.Vertical)
                {
                    VerticalItemSpacing = 10,
                    HorizontalItemSpacing = 10
                },
                ItemTemplate = new DataTemplate(() =>
                {
                    var button = new Button
                    {
                        FontFamily = "MaterialIcons",
                        FontSize = 30,
                        BackgroundColor = Colors.Black,
                        TextColor = Colors.WhiteSmoke
                    };

                    button.SetBinding(Button.TextProperty, nameof(IconItem.IconValue));

                    button.Clicked += async (sender, e) =>
                    {
                        if (sender is Button btn && btn.BindingContext is IconItem icon)
                        {
                            _taskCompletionSource.SetResult(icon.IconValue);
                            await Navigation.PopAsync();
                        }
                    };

                    return button;
                })
            };

            //collectionView.SetBinding(ItemsView.ItemsSourceProperty, nameof(_filteredIcons));

            Content = new StackLayout
            {
                Children = { searchEntry, collectionView }
            };
        }

        private List<IconItem> GetMaterialIcons()
        {
            var icons = new List<IconItem>();
            var fields = typeof(MaterialIcons).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (var field in fields)
            {
                if (field.IsLiteral && !field.IsInitOnly)
                {
                    icons.Add(new IconItem
                    {
                        IconName = field.Name,
                        IconValue = field.GetRawConstantValue().ToString()
                    });
                }
            }
            return icons;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = e.NewTextValue.ToLower();

            if (searchText.Length > 1)
            {
                _filteredIcons.Clear();
                foreach (var icon in Icons)
                {
                    if (icon.IconName.ToLower().Contains(searchText))
                    {
                        _filteredIcons.Add(icon);
                    }
                }

                collectionView.ItemsSource = null;
                collectionView.ItemsSource = _filteredIcons;
            } else
            {
                collectionView.ItemsSource = null;
            }
        }

        public class IconItem
        {
            public string IconName { get; set; }
            public string IconValue { get; set; }
        }
    }
}
