using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data.Models
{
    /// <summary>
    /// Represents an item in the database.
    /// </summary>
    /// <remarks>
    /// This class defines the structure and behavior of an item stored in the database.
    /// Each item has an identifier, a name, a description, and a loop duration in days.
    /// It also contains optional properties related to warning settings and a history of actions.
    /// </remarks>
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; } = String.Empty;

        [Ignore]
        private int _loopInDays {  get; set; }

        /// <summary>
        /// Gets or sets the duration of the item's loop in days.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when setting LoopInDays equal to 1.</exception>
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

        [Ignore]
        private int? _daysBeforeWarning { get; set; }

        /// <summary>
        /// Gets or sets the number of days before a warning is triggered for the item.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when setting DaysBeforeWarning with a value greater than or equal to LoopInDays.</exception>
        public int? DaysBeforeWarning
        {
            get => _daysBeforeWarning;
            set
            {
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

        /// <summary>
        /// Gets or sets the collection of history entries related to the item.
        /// </summary>
        [OneToMany(CascadeOperations = CascadeOperation.CascadeDelete)]
        public IEnumerable<ItemHistory> History { get; set; } = Enumerable.Empty<ItemHistory>();

        /// <summary>
        /// Gets a value indicating whether a warning should be triggered for the item based on its warning settings and history.
        /// </summary>
        [Ignore]
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

        /// <summary>
        /// The number of days since the last occurrence of the item. Returns null if there has been no occurrence yet or if the history is empty.
        /// </summary>
        [Ignore]
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
    }
}
