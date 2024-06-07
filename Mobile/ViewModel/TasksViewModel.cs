using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Data.Models;
using Mobile.Services;
using System.Collections.ObjectModel;

namespace Mobile.ViewModel
{
    public partial class TasksViewModel : ObservableObject
    {
        public DbService _dbService;
        public ObservableCollection<Item> ItemsList { get; set; } = new();
        public List<string> AvailableColors { get; set; } = new();
        public string FilterColor = String.Empty;

        public TasksViewModel(DbService dbService)
        {
            _dbService = dbService;            
        }

        [RelayCommand]
        public async Task LoadItems()
        {
            AvailableColors = await _dbService.GetAvailableColors();
            var loadedItems = await _dbService.GetItems();

            if (FilterColor != String.Empty && FilterColor != "#000000") 
            {
                loadedItems = loadedItems.Where(i => i.Color == FilterColor).ToList();
            }

            var sortedItems = loadedItems.OrderBy(i => i.DaysSinceLastOccurrence == null ? 0 : 1)
                                         .ThenByDescending(i => i.DaysSinceLastOccurrence)
                                         .ToList();

            ItemsList.Clear();
            foreach (var item in sortedItems)
            {
                ItemsList.Add(item);
            }
        }

        public async Task AddItem(Item item)
        {
            await _dbService.SetItem(item);
            ItemsList.Add(item);
            AvailableColors = await _dbService.GetAvailableColors();
            OnPropertyChanged(nameof(AvailableColors));
            OnPropertyChanged(nameof(ItemsList));
        }

        public async Task UpdateItem(Item item)
        {
            await _dbService.SetItem(item);
            ItemsList.Remove(item);
            ItemsList.Add(item);
            await LoadItems();
            OnPropertyChanged(nameof(AvailableColors));
            OnPropertyChanged(nameof(ItemsList));
        }

        public async Task DeleteItem(Item item)
        {
            await _dbService.RemoveItem(item);
            ItemsList.Remove(item);
            AvailableColors = await _dbService.GetAvailableColors();
            OnPropertyChanged(nameof(AvailableColors));
            OnPropertyChanged(nameof(ItemsList));
        }

    }
}
