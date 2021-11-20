using NUnit.Framework;

namespace LogAn.nUnitTests
{
    public class LogAnalyzerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            Assert.Pass();
        }
    }
}