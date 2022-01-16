using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace LogAn.UnitTests
{
    public class LogAnalyzerTests : BaseTestsClass
    {
        private readonly LogAnalyzer analyzer = new LogAnalyzer(new FileExtensionManager());
        private readonly Mock<IFileNameRules> fakeRules = new Mock<IFileNameRules>();

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
            ExtensionManagerFactory.SetManager(new FakeExtensionManager
            {
                WillBeValid = true
            });
            LogAnalyzer log = new LogAnalyzer();

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

        [Fact]
        public void OverrideTestWithoutStub()
        {
            TestableLogAnalyzer logan = new TestableLogAnalyzer
            {
                IsSupported = true
            };

            bool result = logan.IsValidLogFileName("file.ext");

            Assert.True(result, "...");
        }

        [Fact]
        public void Analyze_TooShortFileName_CallWebService()
        {
            FakeWebService mockService = new FakeWebService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            FakeTheLogger();

            log.Analyze(tooShortFileName);

            Assert.Equal("Filename too short:abc.ext", mockService.LastError);
        }

        [Fact]
        public void Analyze_TooShortFileName_CallLogger()
        {
            Mock<ILogger> logger = new Mock<ILogger>();
            LogAnalyzer analyzer = new LogAnalyzer(logger.Object)
            {
                MinNameLength = 6
            };

            analyzer.Analyze("a.txt");

            logger.Verify(x => x.LogError("Filename too short: a.txt"));
        }

        [Fact]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            fakeRules.Setup(x => x.IsVaildLogFileName(It.IsAny<string>())).Returns(true);

            Assert.True(fakeRules.Object.IsVaildLogFileName("anything.txt"));
        }

        [Fact]
        public void Returns_ItIsAny_Throws()
        {
            fakeRules.Setup(x => x.IsVaildLogFileName(It.IsAny<string>())).Throws(new Exception("fake exception"));

            Assert.Throws<Exception>(() => fakeRules.Object.IsVaildLogFileName("anything"));
        }

        [Fact]
        public void Analyze_EmptyFile_ThrowException()
        {
            FakeTheLogger();
            LogAnalyzer la = new LogAnalyzer();

            Assert.Throws<Exception>(() => la.Analyze("myemptyfile.txt"));
        }

        [Fact]
        public void IsValid_LengthNoBiggerThan3_ReturnsFalse()
        {
            LogAnalyzer logan = GetNewAnalyzer();

            bool isValid = logan.IsValid("abc");

            Assert.False(isValid);
        }

        [Fact]
        public void IsValid_LengthBiggerThan8_IsFalse()
        {
            LogAnalyzer logan = GetNewAnalyzer();

            bool valid = logan.IsValid("123456789");

            Assert.False(valid);
        }

        [Fact]
        public void IsValid_LengthSmallerThan8_IsTrue()
        {
            LogAnalyzer logan = GetNewAnalyzer();

            bool valid = logan.IsValid("1234567");

            Assert.True(valid);
        }

        private static LogAnalyzer GetNewAnalyzer()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.Initialize();
            return analyzer;
        }
    }

    public class FakeWebService : IWebService
    {
        public string LastError;

        public Exception ToThrow;

        public string MessageToWebService;

        public void LogError(string message)
        {
            if (ToThrow != null)
            {
                throw ToThrow;
            }
            LastError = message;
        }

        public void Write(string message)
        {
            MessageToWebService = message;
        }

        public void Write(ErrorInfo message)
        {
            throw new NotImplementedException();
        }
    }

    internal class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public bool IsSupported;

        public TestableLogAnalyzer()
        {
        }

        protected override bool IsValid(string fileName)
        {
            return IsSupported;
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
