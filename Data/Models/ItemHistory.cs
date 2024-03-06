using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data.Models
{
    /// <summary>
    /// Represents the history of occurrences for an item.
    /// </summary>
    public class ItemHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The date and time when the occurrence was recorded.
        /// </summary>
        public DateTime Done { get; set; }

        /// <summary>
        /// The ID of the item associated with this history record.
        /// </summary>
        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }

        /// <summary>
        /// The item associated with this history record.
        /// </summary>
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public virtual Item Item { get; set; }
    }
}
