using Mobile.Data.Models;
using Mobile.Localize;
using Mobile.ViewModel;
using System.ComponentModel;

namespace Mobile.Pages;

public partial class HistoryPage : ContentPage
{
    public HistoryViewModel _hvm;
    public Item Item = new();

    public HistoryPage(HistoryViewModel hvm)
    {
        InitializeComponent();
        _hvm = hvm;
        BindingContext = _hvm;
        _hvm.PropertyChanged += OnPropertyChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _hvm.LoadItemHistory(Item);
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is HistoryViewModel itemHistory)
        {
            Button button = (Button)sender;
            int itemId = (int)button.CommandParameter;

            bool answer = await DisplayAlert(AppRes.POPUP_CONFIRMATION_TITLE, AppRes.POPUP_CONFIRMATION_CONTENT, AppRes.YES, AppRes.CANCEL);

            if (answer) await _hvm.DeleteItemHistory(itemId);
        }
    }

    private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        await _hvm.LoadItemHistory(Item);

        if (e.PropertyName == nameof(_hvm.ItemsHistoryList))
        {
            colv.ItemsSource = null;
            colv.ItemsSource = _hvm.ItemsHistoryList;
        }
    }
}
