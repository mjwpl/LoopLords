using Mobile.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mobile.Data.Models
{
    public class Settings
    {
        [Key]
        public SettingsKeyEnum Key { get; set; }
        public string Value { get; set; } = String.Empty;
    }
}
