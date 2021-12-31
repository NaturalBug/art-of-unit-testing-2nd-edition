using NUnit.Framework;

namespace LogAn.nUnitTests
{
    class ConfigurationManagerTests : BaseTestsClass
    {
        [Test]
        public void IsConfigured_Always_ReturnsTrue()
        {
            FakeTheLogger();
            ConfigurationManager cm = new ConfigurationManager();

            bool configured = cm.IsConfigured("something");

            Assert.IsTrue(configured);
        }
    }
}
