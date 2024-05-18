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
                    return RealDate.ToString("dd-MM-yyyy"); // TODO: use a format string for country
            }
        }
        public DateTime RealDate { get; set; }
    }
}
