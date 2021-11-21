using NUnit.Framework;

namespace LogAn.nUnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer m_analyzer = null;

        [SetUp]
        public void Setup()
        {
            m_analyzer = new LogAnalyzer();
        }

        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidLogFileName_VariousExtension_ChecksThem(string file, bool expected)
        {
            bool result = m_analyzer.IsVaildLogFileName(file);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IsValidLogFileName_ValidFileLowerCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsVaildLogFileName("whatever.slf");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [Test]
        public void IsValidLogFileName_ValidFileUpperCased_ReturnsTrue()
        {
            bool result = m_analyzer.IsVaildLogFileName("whatever.SLF");
            Assert.IsTrue(result, "filename should be valid!");
        }

        [TearDown]
        public void TearDown()
        {
            m_analyzer = null;
        }
    }
}