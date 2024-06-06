using System.Globalization;

namespace Mobile.Data.Models
{
    public class DoneDate
    {
        public string DisplayDate {
            get
            {
                if (RealDate.Date == DateTime.Now.Date)
                    return "today";
                else if (RealDate.Date == DateTime.Now.AddDays(-1).Date)
                    return "yesterday";
                else
                    return RealDate.ToString("d", CultureInfo.CurrentCulture); 
            }
        }
        public DateTime RealDate { get; set; }
    }
}
