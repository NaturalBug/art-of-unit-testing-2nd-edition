using NUnit.Framework;

namespace LogAn.nUnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithbadextension.foo");
            Assert.False(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithgoodextension.slf");
            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithgoodextension.SLF");
            Assert.True(result);
        }
    }
}