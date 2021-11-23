using FluentAssertions;
using System;
using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzerTests
    {
        private readonly LogAnalyzer analyzer = new LogAnalyzer();
        
        [Theory]
        [InlineData("filewithgoodextension.SLF", true)]
        [InlineData("filewithgoodextension.slf", true)]
        [InlineData("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtension_ReturnsTrue(string file, bool expected)
        {
            bool result = analyzer.IsVaildLogFileName(file);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValidLogFileName_ValidFileLowerCased_ReturnsTrue()
        {
            bool result = analyzer.IsVaildLogFileName("whatever.slf");

            Assert.True(result);
        }
        
        [Fact]
        public void IsValidLogFileName_ValidFileUpperCased_ReturnsTrue()
        {
            bool result = analyzer.IsVaildLogFileName("whatever.SLF");

            Assert.True(result);
        }

        [Fact]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => analyzer.IsVaildLogFileName(string.Empty));

            Assert.Equal("filename has to be provided", exception.Message);
        }

        [Fact]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            Action action = () => analyzer.IsVaildLogFileName(string.Empty);

            action.Should().Throw<ArgumentException>().WithMessage("filename has to be provided");
        }

        [Fact]
        [Trait("Category", "Fast Tests")]
        public void IsValidLogFileName_ValidFile_ReturnsTrue()
        {

        }

        [Fact]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid()
        {
            analyzer.IsVaildLogFileName("badname.foo");

            Assert.False(analyzer.WasLastFileNameValid);
        }
    }
}
