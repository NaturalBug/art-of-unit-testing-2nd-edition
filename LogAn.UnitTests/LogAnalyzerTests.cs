using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzerTests
    {
        [Theory]
        [InlineData("filewithgoodextension.SLF", true)]
        [InlineData("filewithgoodextension.slf", true)]
        [InlineData("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtension_ReturnsTrue(string file, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsVaildLogFileName(file);
            Assert.Equal(expected, result);
        }
    }
}
