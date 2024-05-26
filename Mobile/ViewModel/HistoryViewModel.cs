using CommunityToolkit.Mvvm.ComponentModel;
using Mobile.Services;
using System.Collections.ObjectModel;
using Mobile.Data.Models;
using CommunityToolkit.Mvvm.Input;


namespace Mobile.ViewModel
{
    public partial class HistoryViewModel : ObservableObject
    {
        public DbService _dbService;
        public ObservableCollection<ItemHistory> ItemsHistoryList { get; set; } = new();

        [RelayCommand]
        public async Task LoadItemHistory(Item item)
        {
            var loadedItemHistory = await _dbService.GetItemHistory(item);


            ItemsHistoryList.Clear();
            foreach (var itemHistory in loadedItemHistory)
            {
                ItemsHistoryList.Add(itemHistory);
            }
        }

        public async Task DeleteItemHistory(int id)
        {
            var itemHistory = ItemsHistoryList.FirstOrDefault(f => f.Id == id);
            await _dbService.RemoveItemHistory(itemHistory);
            ItemsHistoryList.Remove(itemHistory);
        }
    }
}
