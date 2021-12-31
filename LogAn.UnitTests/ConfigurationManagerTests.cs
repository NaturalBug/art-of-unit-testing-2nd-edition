using Xunit;

namespace LogAn.UnitTests
{
    public class ConfigurationManagerTests : BaseTestsClass
    {

        [Fact]
        public void IsConfigured_Always_ReturnsTrue()
        {
            FakeTheLogger();
            ConfigurationManager cm = new ConfigurationManager();

            bool configured = cm.IsConfigured("something");

            Assert.True(configured);
        }
    }
}
