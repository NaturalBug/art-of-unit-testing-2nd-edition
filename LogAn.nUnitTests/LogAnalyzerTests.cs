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
        [Ignore("there is a problem with this test")]
        public void IsValidLogFileName_ValidFile_ReturnsTrue()
        {

        }

        private LogAnalyzer MakeAnalyer()
        {
            return new LogAnalyzer();
        }
    }
}