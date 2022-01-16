using NSubstitute;
using NUnit.Framework;
using System;

namespace LogAn.nUnitTests
{
    [TestFixture]
    public class LogAnalyzerTests : BaseTestsClass
    {
        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtension_ChecksThem(string file, bool expected)
        {
            LogAnalyzer la = MakeAnalyer();

            bool result = la.IsVaildLogFileName(file);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsValidLogFileName_ValidFileLowerCased_ReturnsTrue()
        {
            LogAnalyzer la = MakeAnalyer();

            bool result = la.IsVaildLogFileName("whatever.slf");

            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        public void IsValidLogFileName_ValidFileUpperCased_ReturnsTrue()
        {
            LogAnalyzer la = MakeAnalyer();

            bool result = la.IsVaildLogFileName("whatever.SLF");

            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            LogAnalyzer la = MakeAnalyer();

            Assert.Throws<ArgumentException>(() => la.IsVaildLogFileName(string.Empty), "filename has to be provided");
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            LogAnalyzer la = MakeAnalyer();

            Assert.Catch<ArgumentException>(() => la.IsVaildLogFileName(string.Empty), "filename has to be provided");
        }

        [Test]
        [Category("Fast Tests")]
        public void IsValidLogFileName_ValidFile_ReturnsTrue()
        {

        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_ThrowsFluent()
        {
            LogAnalyzer la = MakeAnalyer();

            var ex = Assert.Catch<ArgumentException>(() => la.IsVaildLogFileName(string.Empty));

            Assert.That(ex.Message, Is.EqualTo("filename has to be provided"));
        }

        [TestCase("badname.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer la = MakeAnalyer();

            la.IsVaildLogFileName(file);

            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }

        [Test]
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

        [Test]
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

        [Test]
        public void OverrideTestWithoutStub()
        {
            TestableLogAnalyzer logan = new TestableLogAnalyzer();
            logan.IsSupported = true;

            bool result = logan.IsValidLogFileName("file.ext");

            Assert.True(result, "...");
        }

        [Test]
        public void Analyze_TooShortFileName_CallWebService()
        {
            FakeWebService mockService = new FakeWebService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            FakeTheLogger();

            log.Analyze(tooShortFileName);

            StringAssert.Contains("Filename too short:abc.ext", mockService.LastError);      
        }

        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            ILogger logger = Substitute.For<ILogger>();
            LogAnalyzer analyzer = new LogAnalyzer(logger)
            {
                MinNameLength = 6
            };

            analyzer.Analyze("a.txt");

            logger.Received().LogError("Filename too short: a.txt");
        }

        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();

            fakeRules.IsVaildLogFileName(Arg.Any<string>()).Returns(true);

            Assert.IsTrue(fakeRules.IsVaildLogFileName("anything.txt"));
        }

        [Test]
        public void Reutrns_ArgAny_Throws()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();

            fakeRules.When(x => x.IsVaildLogFileName(Arg.Any<string>())).Do(context => { throw new Exception("fake exception"); });

            Assert.Throws<Exception>(() => fakeRules.IsVaildLogFileName("anything"));
        }

        [Test]
        public void Analyze_EmptyFile_ThrowException()
        {
            FakeTheLogger();
            LogAnalyzer la = new LogAnalyzer();

            Assert.Throws<Exception>(() => la.Analyze("myemptyfile.txt"));
        }

        [Test]
        public void SemanticsChange()
        {
            LogAnalyzer logan = new LogAnalyzer();
            logan.Initialize();

            Assert.IsFalse(logan.IsValid("abc"));
        }

        private LogAnalyzer MakeAnalyer()
        {
            return new LogAnalyzer(new FileExtensionManager());
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