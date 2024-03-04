using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int LoopInDays { get; set; }
        [Ignore]
        private int? _daysBeforeWarning {  get; set; }
        public int? DaysBeforeWarning
        {
            get => _daysBeforeWarning;
            set
            {
                if (LoopInDays == 1)
                {
                    // TODO: Localization will be required here
                    throw new InvalidOperationException("LoopInDays cannot be 1 when setting DaysBeforeWarning.");
                }

                if (value.HasValue && value < LoopInDays)
                {
                    _daysBeforeWarning = value;
                }
                else
                {
                    // TODO: Localization will be required here
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be less than LoopInDays.");
                }
            }
        }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public IEnumerable<ItemHistory> History { get; set; } = Enumerable.Empty<ItemHistory>();
        [Ignore]
        public bool Warning
        {
            get
            {
                if (DaysBeforeWarning.HasValue && History.Any())
                {
                    var lastHistoryDate = History.Max(h => h.Done);
                    var daysSinceLastHistory = (DateTime.Now - lastHistoryDate).Days;
                    return daysSinceLastHistory < LoopInDays - DaysBeforeWarning;
                }
                return false;
            }
        }
    }
}
