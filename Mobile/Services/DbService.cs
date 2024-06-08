using Mobile.Data;
using Mobile.Data.Models;
using Mobile.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mobile.Services
{
    public class DbService
    {
        private LocalDbContext _db;

        public DbService(LocalDbContext db)
        {
            _db = db;
            RunMigration();
        }

        private void RunMigration()
        {
            try
            {
                _db.Database.Migrate();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public string GetSettings(SettingsKeyEnum key)
        {
            var pair = _db.Settings.Where(w => w.Key == key).FirstOrDefault();
            return pair?.Value;
        }

        public void SetSettings(SettingsKeyEnum key, string value)
        {
            var pair = _db.Settings.AsNoTracking().FirstOrDefault(w => w.Key == key);

            if (pair != null)
            {
                var local = _db.Set<Settings>()
                    .Local
                    .FirstOrDefault(entry => entry.Key.Equals(key));

                if (local != null)
                {
                    _db.Entry(local).State = EntityState.Detached;
                }

                pair.Value = value;

                _db.Settings.Update(pair);
            }
            else
            {
                pair = new Settings { Key = key, Value = value };
                _db.Settings.Add(pair);
            }

            _db.SaveChanges();
        }


        public async Task<List<Item>> GetItems()
        {
            var itemList = await _db.Items.Include(i => i.History).ToListAsync();

            itemList = itemList.OrderByDescending(i => i.DaysSinceLastOccurrence).ToList();

            string setting = GetSettings(SettingsKeyEnum.SHOW_HIDDEN);
            var isShowHidden = EnumerableExtensions.GetSettingsBoolValueEnum(setting);
            if (isShowHidden) return itemList.ToList();
            else return itemList.Where(w => w.HideInDays == 0 || w.DaysSinceLastOccurrence > w.HideInDays || w.History.Count == 0).ToList();
        }

        public async Task<Item> SetItem(Item item)
        {
            var existingItem = await _db.Items.FindAsync(item.Id);

            if (existingItem != null)
            {
                _db.Entry(existingItem).State = EntityState.Detached;
                _db.Update(item);
            }
            else
            {
                _db.Items.Add(item);
            }

            await _db.SaveChangesAsync();
            return item;
        }

        public async Task RemoveItem(Item item)
        {
            var existingItem = await _db.Items.FindAsync(item.Id);

            if (existingItem != null)
            {
                _db.Items.Remove(existingItem);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<ItemHistory>> GetItemHistory(Item item)
        {
            var history = await _db.History.Where(w => w.ItemId == item.Id)
                                           .OrderByDescending(h => h.Done)
                                           .ToListAsync();
            for (int i = 0; i < history.Count - 1; i++)
            {
                int daysDifference = (history[i].Done.Date - history[i + 1].Done.Date).Days;
                history[i].DaysSinceLast = daysDifference == 0 ? (int?)null : daysDifference;
            }

            if (history.Count > 0)
            {
                history[history.Count - 1].DaysSinceLast = 0;
            }

            return history;
        }

        public async Task RemoveItemHistory(ItemHistory itemHistory)
        {
            var existingItemHistory = await _db.History.FindAsync(itemHistory.Id);

            if (existingItemHistory != null)
            {
                _db.History.Remove(existingItemHistory);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetAvailableColors()
        {
            string setting = GetSettings(SettingsKeyEnum.SHOW_HIDDEN);
            var isShowHidden = EnumerableExtensions.GetSettingsBoolValueEnum(setting);
            if (isShowHidden)
            {
                return await _db.Items.Select(x => x.Color).Distinct().ToListAsync();
            } else
            {
                var itemList = await _db.Items.Include(i => i.History).ToListAsync();
                return itemList.Where(w => w.HideInDays == 0 || w.DaysSinceLastOccurrence > w.HideInDays || w.History.Count == 0)
                    .Select(s => s.Color).Distinct().ToList();
            }
                
        }
           
        

    }
}
