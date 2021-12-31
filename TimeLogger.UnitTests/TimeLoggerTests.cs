using System;
using Xunit;

namespace TimeLogger.UnitTests
{
    public class TimeLoggerTests
    {
        [Fact]
        public void SettingSystemTime_Always_ChangesTime()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));

            string output = TimeLogger.CreateMessage("a");

            Assert.Contains("01.01.2000", output);
        }
    }
}
