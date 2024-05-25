using Mobile.Data.Enums;

public static class EnumerableExtensions
{
    public static double Median(this IEnumerable<double> source)
    {
        var sortedList = source.OrderBy(x => x).ToList();
        int count = sortedList.Count;
        if (count % 2 == 0)
        {
            int midIndex = count / 2;
            return (sortedList[midIndex - 1] + sortedList[midIndex]) / 2;
        }
        else
        {
            return sortedList[count / 2];
        }
    }

    public static bool GetSettingsBoolValueEnum(string value)
    {
        if (Enum.TryParse<SettingsBoolEnum>(value, true, out SettingsBoolEnum result))
        {
            return result == SettingsBoolEnum.TRUE;
        }

        throw new InvalidOperationException($"Invalid value '{value}' for enum type SettingsBoolEnum.");
    }
}