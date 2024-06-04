namespace Mobile.Data.Helpers
{
    public static class Value
    {
        public static string Increment(string value)
        {
            return (Int32.Parse(value) + 1).ToString();
        }

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
    }
}
