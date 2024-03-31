using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Data.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace Mobile.ViewModel
{
    public partial class ItemListViewModel : ObservableObject
    {
        private readonly LocalDbContext localDbContext;

        public ObservableCollection<Item> Items { get; set; }

        [ObservableProperty]
        string title;

        public ItemListViewModel(LocalDbContext db)
        {
            localDbContext = db;
            Items = new ObservableCollection<Item>();
            Title = "Siema";
        }

        [RelayCommand]
        public async Task LoadItems()
        {
            var loadedItems = await localDbContext.Items.Include(i => i.History).ToListAsync();
            Items.Clear();
            foreach (var item in loadedItems)
            {
                Items.Add(item);
            }

            Debug.WriteLine($"Załadowano {Items.Count}");
        }
    }
}
