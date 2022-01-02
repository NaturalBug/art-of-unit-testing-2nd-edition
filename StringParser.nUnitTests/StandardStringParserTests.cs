using NUnit.Framework;

namespace StringParser.nUnitTests
{
    public class StandardStrinParserTests : TemplateStringParserTests
    {
        private static StandardStringParser GetParser(string input)
        {
            return new StandardStringParser(input);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_SingleDigit_Found()
        {
            string input = "header;version=1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithMinorVersion_Found()
        {
            string input = "header;version=1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithRevision_Found()
        {
            string input = "header;version=1.1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1.1", versionFromHeader);
        }
    }
}