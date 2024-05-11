using Mobile.Data.Models;
using Mobile.Validators;

namespace Mobile.Pages;

public partial class AddPage : CommunityToolkit.Maui.Views.Popup
{
    private readonly AddValidator _validator = new AddValidator();
    private List<string> _colors = new List<string> { "#1d72a0", "#e37238", "#9117f1", "#13cca0", "#3eb490", "#d33682" };

    public AddPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var item = new Item
        {
            Name = NameEntry.Text,
            HideInDays = HideInDaysEntry.Text is null ? 0 : int.Parse(HideInDaysEntry.Text),
            Color = _colors[new Random().Next(0, _colors.Count)]
        };

        var validationResult = _validator.Validate(item);
        if (!validationResult.IsValid)
        {
            string errors = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));
            Application.Current?.MainPage?.DisplayAlert("Error", errors, "OK");
            return;
        }

        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(item, cts.Token);
    }
}