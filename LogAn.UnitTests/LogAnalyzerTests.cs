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
        
        [Theory]
        [InlineData("filewithgoodextension.SLF")]
        [InlineData("filewithgoodextension.slf")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string file)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName(file);
            Assert.True(result);
        }
    }
}
