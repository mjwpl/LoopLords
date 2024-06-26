﻿using Mobile.Data.Helpers;
using Mobile.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Icon { get; set; } = MaterialIcons.Question_mark;
        public string Color { get; set; } = "#e37138";
        public int HideInDays { get; set; } = 0;
        public List<ItemHistory> History { get; set; } = new();

        [NotMapped]
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

        [NotMapped]
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

        [NotMapped]
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
            return Id == other.Id && Name == other.Name &&  History.Count() == other.History.Count();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
