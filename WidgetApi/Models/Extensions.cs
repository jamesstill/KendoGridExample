namespace WidgetApi.Models
{
    public static class Extensions
    {
        public static long ToLong(this int i, long value)
        {
            if (i == 0)
                return value;

            return Convert.ToInt64(i);
        }
    }
}
