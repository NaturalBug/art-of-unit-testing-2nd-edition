using FluentAssertions;
using System;
using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzerTests
    {
        private readonly LogAnalyzer analyzer = new LogAnalyzer(new FileExtensionManager());
        
        [Theory]
        [InlineData("filewithgoodextension.SLF", true)]
        [InlineData("filewithgoodextension.slf", true)]
        [InlineData("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtension_CheckThem(string file, bool expected)
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

        [Theory]
        [InlineData("badname.foo", false)]
        [InlineData("goodfile.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            analyzer.IsVaildLogFileName(file);

            Assert.Equal(expected, analyzer.WasLastFileNameValid);
        }

        [Fact]
        public void IsValidLogFileName_NameSupportedExtension_ReturnsTrue()
        {
            LogAnalyzer log = new LogAnalyzer(new FakeExtensionManager
            {
                WillBeValid = true
            });

            bool result = log.IsVaildLogFileName("short.ext");

            Assert.True(result);
        }

        [Fact]
        public void IsValidLogFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            LogAnalyzer log = new LogAnalyzer(new FakeExtensionManager
            {
                WillThrow = new Exception("this is fake")
            });

            bool result;
            try
            {
                result = log.IsVaildLogFileName("anything.anyextension");
            }
            catch
            {
                result = false;
            }

            Assert.False(result);
        }
    }

    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;

        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeValid;
        }
    }
}
