using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Data.Models
{
    public class ItemHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Done { get; set; }
        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
