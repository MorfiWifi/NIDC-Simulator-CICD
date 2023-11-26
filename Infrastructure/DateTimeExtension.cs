using System;

namespace Infrastructure
{
    public static class DateTimeExtension
    {
        public static long ConvertToTimestamp(this DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
    }
}
