using System;

namespace TimeLogger
{
    public class SystemTime
    {
        private static DateTime date;

        public static DateTime Now
        {
            get
            {
                if (date != DateTime.MinValue)
                {
                    return date;
                }
                return date;
            }
        }

        public static void Set(DateTime custom)
        {
            date = custom;
        }

        public static void Reset()
        {
            date = DateTime.MinValue;
        }
    }
}