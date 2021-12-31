using System;

namespace LogAn
{
    internal class LoggingFacility
    {
        private static ILogger logger;
        public static ILogger Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        internal static void Log(string text)
        {
            logger.Log(text);
        }
    }
}