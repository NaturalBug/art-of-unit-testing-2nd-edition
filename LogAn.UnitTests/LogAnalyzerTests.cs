using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzerTests
    {
        [Fact]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithbadextension.foo");
            Assert.False(result);
        }
        
        [Fact]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithgoodextension.slf");
            Assert.True(result);
        }
        
        [Fact]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName("filewithgoodextension.SLF");
            Assert.True(result);
        }
    }
}
