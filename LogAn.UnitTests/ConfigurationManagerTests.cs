using Moq;
using Xunit;

namespace LogAn.UnitTests
{
    public class ConfigurationManagerTests
    {
        [Fact]
        public void IsConfigured_Always_ReturnsTrue()
        {
            LoggingFacility.Logger = new Mock<ILogger>().Object;
            ConfigurationManager cm = new ConfigurationManager();

            bool configured = cm.IsConfigured("something");

            Assert.True(configured);
        }
    }
}
