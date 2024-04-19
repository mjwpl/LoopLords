using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Data.Models;
using Mobile.Helpers;
using System.Collections.ObjectModel;

namespace Mobile.ViewModel
{
    public partial class TodayViewModel : ObservableObject
    {
        private readonly LocalDbContext localDbContext;
        private readonly IMapper mapper;

        public ObservableCollection<ItemDto> Items { get; set; }

        [ObservableProperty]
        string title;

        public TodayViewModel()
        {
            localDbContext = ServiceHelper.GetService<LocalDbContext>();
            mapper = ServiceHelper.GetService<IMapper>();
            Items = new ObservableCollection<ItemDto>();
            Title = "Siema";
        }

        [RelayCommand]
        public async Task LoadItems()
        {
            var loadedItems = await localDbContext.Items.Include(i => i.History).ToListAsync();
            var sortedItems = loadedItems.OrderBy(o => o.DaysToNextScheduledDate).Where(w => w.Warning == true || w.DaysSinceLastOccurrence == 0).ToList();

            Items.Clear();
            foreach (var item in sortedItems)
            {
                ItemDto dto = mapper.Map<ItemDto>(item);
                Items.Add(dto);
            }
        }

        [RelayCommand]
        public async Task<int> CheckItem(int itemId)
        {
            var chk = new ItemHistory();
            chk.Done = DateTime.Now;
            chk.ItemId = itemId;
            localDbContext.History.Add(chk);
            await localDbContext.SaveChangesAsync();

            await LoadItems();

            return chk.Id;
        }
    }
}
