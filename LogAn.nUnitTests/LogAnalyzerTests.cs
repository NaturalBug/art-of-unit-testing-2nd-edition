using NUnit.Framework;
using System;

namespace LogAn.nUnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
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
        public void OverrideTest()
        {
            TestableLogAnalyzer logan = new TestableLogAnalyzer(new FakeExtensionManager
            {
                WillBeValid = true
            });

            bool result = logan.IsValidLogFileName("file.ext");

            Assert.True(result);
        }

        private LogAnalyzer MakeAnalyer()
        {
            return new LogAnalyzer(new FileExtensionManager());
        }
    }

    internal class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public IExtensionManager Manager;

        public TestableLogAnalyzer(IExtensionManager mgr)
        {
            Manager = mgr;
        }

        protected override IExtensionManager GetManager()
        {
            return Manager;
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