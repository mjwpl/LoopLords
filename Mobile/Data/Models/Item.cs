using Mobile.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string HintForNextOccurrence { get; set; } = String.Empty;
        public string Icon { get; set; } = MaterialIcons.Question_mark;
        public string Color { get; set; } = "#e37138";
        public int HideInDays { get; set; } = 0;
        public List<ItemHistory> History { get; set; } = new();

        public int? DaysSinceLastOccurrence
        {
            get
            {
                if (History != null && History.Any())
                {
                    var lastHistoryDate = History.Max(h => h.Done.Date);
                    return (DateTime.Now.Date - lastHistoryDate).Days;
                }
                return null;
            }
        }

        public double? CalculateMedianDaysBetweenOccurrences
        {
            get
            {
                if (History != null && History.Count > 1)
                {
                    var sortedHistory = History.OrderBy(h => h.Done.Date).ToList();
                    var daysBetweenOccurrences = sortedHistory.Zip(sortedHistory.Skip(1), (prev, next) => (next.Done.Date - prev.Done.Date).TotalDays);
                    var median = daysBetweenOccurrences.Median();
                    return median;
                }
                return null;
            }

        }

        public bool IsVisibleBtn
        {
            get
            {
                if (History != null && History.Any())
                {
                    var lastHistoryDate = History.Max(h => h.Done.Date);
                    return lastHistoryDate != DateTime.Now.Date;
                }
                return true;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Item other = (Item)obj;
            return Id == other.Id && Name == other.Name && HintForNextOccurrence == other.HintForNextOccurrence && History.Count() == other.History.Count();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
