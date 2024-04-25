using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Mobile.Data;
using Mobile.Data.Models;
using Mobile.Helpers;
using Mobile.Validators;

namespace Mobile.ViewModel
{
    public partial class NewViewModel : ObservableObject
    {
        [ObservableProperty]
        public string name = String.Empty;

        [ObservableProperty]
        private string description = String.Empty;

        [ObservableProperty]
        private int loopInDays;

        [ObservableProperty]
        private int? daysBeforeWarning;

        private LocalDbContext _localDbContext;

        private readonly NewValidator _validator = new NewValidator();

        public NewViewModel()
        {
            _localDbContext = ServiceHelper.GetService<LocalDbContext>();
        }

        [RelayCommand]
        public async Task AddItem()
        {
            var item = new Item()
            {
                Name = Name,
                Description = Description,
                LoopInDays = LoopInDays,
                DaysBeforeWarning = DaysBeforeWarning
            };

            var validationResult = _validator.Validate(this);
            if (!validationResult.IsValid)
            {
                Application.Current?.MainPage?.DisplayAlert("Error", "Please correct the errors below:", "OK");
                return;
            }

            _localDbContext.Items.Add(item);
            await _localDbContext.SaveChangesAsync();

            // Display a message to indicate that the item was added successfully
            Application.Current.MainPage.DisplayAlert("Item Added", "The new item has been added.", "OK");
        }
    }
}
