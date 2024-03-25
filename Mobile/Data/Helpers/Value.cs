namespace Mobile.Data.Helpers
{
    public static class Value
    {
        public static string Increment(string value)
        {
            return (Int32.Parse(value) + 1).ToString();
        }
    }
}
