using System.Globalization;
using System.Threading;

namespace TimeLogger
{
    public static class TimeLogger
    {
        public static string CreateMessage(string info)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de");
            return SystemTime.Now.ToShortDateString() + " " + info;
        }
    }
}
