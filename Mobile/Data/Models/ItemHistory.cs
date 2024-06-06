using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Mobile.Data.Models
{
    public class ItemHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime Done { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [NotMapped]
        public int? DaysSinceLast { get; set; }

        [NotMapped]
        public string DisplayDate => Done.ToString("d", CultureInfo.CurrentCulture);
    }
}
