using NUnit.Framework;

namespace StringParser.nUnitTests
{
    public class StandardStrinParserTests
    {
        private static StandardStringParser GetParser(string input)
        {
            return new StandardStringParser(input);
        }

        [Test]
        public void GetStringVersionFromHeader_SingleDigit_Found()
        {
            string input = "header;version=1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithMinorVersion_Found()
        {
            string input = "header;version=1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public void GetStringVersionFromHeader_WithRevision_Found()
        {
            string input = "header;version=1.1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1.1", versionFromHeader);
        }
    }
}