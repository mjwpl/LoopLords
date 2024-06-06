using System.Globalization;

namespace Mobile.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MedianToDisplayConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is null)
                return "?";

            return value.ToString()!;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
