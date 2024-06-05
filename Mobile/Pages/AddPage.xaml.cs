using CommunityToolkit.Maui.Views;
using Mobile.Data.Models;
using Mobile.Validators;

namespace Mobile.Pages;

public partial class AddPage : Popup
{
    private readonly AddValidator _validator = new AddValidator();
    private List<string> _colors = new List<string> { "#1d72a0", "#e37238", "#9117f1", "#3eb490", "#d33682" };
    private Item _item = new();

    public AddPage(Item? item = null)
    {
        InitializeComponent();

        if (item is not null)
        {
            _item = item;

            NameEntry.Text = item.Name;
            HideInDaysEntry.Text = item.HideInDays.ToString();
            this.Color = Color.FromArgb(item.Color);
        } else
        {
            this.Color = Color.FromArgb(_colors[new Random().Next(0, _colors.Count)]);
        }
    }

    private async void Ok_Clicked(object sender, EventArgs e)
    {
        _item.Name = NameEntry.Text;
        _item.HideInDays = HideInDaysEntry.Text is null ? 0 : int.Parse(HideInDaysEntry.Text);
        _item.Color = this.Color.ToHex();

        var validationResult = _validator.Validate(_item); 
        if (!validationResult.IsValid)
        {
            string errors = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
            Application.Current?.MainPage?.DisplayAlert("Error", errors, "OK");
            return;
        }

        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(_item, cts.Token);
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(null, cts.Token);
    }

    private void ChangeColor_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            this.Color = button.BackgroundColor;
        }        
    }

}