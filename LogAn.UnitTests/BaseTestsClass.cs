using Moq;
using System;

namespace LogAn.UnitTests
{
    public class BaseTestsClass : IDisposable
    {
        public ILogger FakeTheLogger()
        {
            LoggingFacility.Logger = new Mock<ILogger>().Object;
            return LoggingFacility.Logger;
        }

        public void Dispose()
        {
            LoggingFacility.Logger = null;
        }
    }
}