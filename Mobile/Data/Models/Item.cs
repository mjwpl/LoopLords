using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        [NotMapped]
        private int _loopInDays { get; set; }
        public int LoopInDays
        {
            get
            {
                return _loopInDays;
            }

            set
            {
                if (value == 1)
                {
                    // TODO: Localization will be required here
                    throw new InvalidOperationException("LoopInDays cannot be 1 when setting DaysBeforeWarning.");
                }
                _loopInDays = value;
            }
        }

        [NotMapped]
        private int? _daysBeforeWarning { get; set; }

        public int? DaysBeforeWarning
        {
            get => _daysBeforeWarning;
            set
            {
                if (value.HasValue && LoopInDays > 0 && value < LoopInDays)
                {
                    _daysBeforeWarning = value;
                }
                else if (LoopInDays > 0)
                {
                    // TODO: Localization will be required here
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be less than LoopInDays.");
                }
            }
        }

        public List<ItemHistory> History { get; set; }

        [NotMapped]
        public bool Warning
        {
            get
            {
                if (DaysBeforeWarning.HasValue && History.Any())
                {
                    var lastHistoryDate = History.Max(h => h.Done);
                    var daysSinceLastHistory = (DateTime.Now - lastHistoryDate).Days;
                    return daysSinceLastHistory <= LoopInDays - DaysBeforeWarning;
                }
                return false;
            }
        }

        [NotMapped]
        public int? DaysSinceLastOccurrence
        {
            get
            {
                if (History.Any())
                {
                    var lastHistoryDate = History.Max(h => h.Done);
                    return (DateTime.Now - lastHistoryDate).Days;
                }
                return null;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Id == other.Id && Name == other.Name && Description == other.Description && LoopInDays == other.LoopInDays && History.Count() == other.History.Count();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
