using NSubstitute;
using NUnit.Framework;

namespace LogAn.nUnitTests
{
    class ConfigurationManagerTests
    {
        [Test]
        public void IsConfigured_Always_ReturnsTrue()
        {
            LoggingFacility.Logger = Substitute.For<ILogger>();
            ConfigurationManager cm = new ConfigurationManager();

            bool configured = cm.IsConfigured("something");

            Assert.IsTrue(configured);
        }

        [TearDown]
        public void TearDown()
        {
            LoggingFacility.Logger = null;
        }
    }
}
