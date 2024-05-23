using System.Globalization;

namespace Mobile.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class DaysSinceLastOccurrenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null) return "Never";
            if ((int)value == 0 ) return "Today";
                return $"{value} {((int)value == 1 ? "day" : "days")} ago";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
