using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobile.Data.Models
{
    public class ItemHistory
    {
        [Key]
        public int Id { get; set; }
        public DateTime Done { get; set; }
        public String Notes { get; set; } = String.Empty;

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
