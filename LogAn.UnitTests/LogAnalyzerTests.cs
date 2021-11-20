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
    }
}
