using NUnit.Framework;
using System;

namespace TimeLogger.nUnitTests
{
    public class TimeLoggerTests
    {
        [Test]
        public void SettingSystemTime_Always_ChangesTime()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));

            string output = TimeLogger.CreateMessage("a");

            StringAssert.Contains("01.01.2000", output);
        }

        [TearDown]
        public void AfterEachTest()
        {
            SystemTime.Reset();
        }
    }
}