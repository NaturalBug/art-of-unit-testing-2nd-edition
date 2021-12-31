using NSubstitute;
using NUnit.Framework;

namespace LogAn.nUnitTests
{
    public class BaseTestsClass
    {
        public ILogger FakeTheLogger()
        {
            LoggingFacility.Logger = Substitute.For<ILogger>();
            return LoggingFacility.Logger;
        }

        [TearDown]
        public void TearDown()
        {
            LoggingFacility.Logger = null;
        }
    }
}