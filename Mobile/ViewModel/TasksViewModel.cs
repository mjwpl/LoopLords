using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Data.Models;
using Mobile.Helpers;
using System.Collections.ObjectModel;

namespace Mobile.ViewModel
{
    public partial class TasksViewModel : ObservableObject
    {
        private readonly LocalDbContext _localDbContext;

        public ObservableCollection<Item> Items { get; set; } = new();

        public TasksViewModel()
        {
            _localDbContext = ServiceHelper.GetService<LocalDbContext>();
        }

        [RelayCommand]
        public async Task LoadItems()
        {
            var loadedItems = await _localDbContext.Items.Include(i => i.History).ToListAsync();
            
            Items.Clear();
            foreach (var item in loadedItems)
            {
                Items.Add(item);
            }
        }
    }
}
