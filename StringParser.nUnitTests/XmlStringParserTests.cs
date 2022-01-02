using NUnit.Framework;

namespace StringParser.nUnitTests
{
    class XmlStringParserTests : TemplateStringParserTests
    {
        [Test]
        public override void TestGetStringVersionFromHeader_SingleDigit_Found()
        {
            IStringParser parser = GetParser("<Header>1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithMinorVersion_Found()
        {
            IStringParser parser = GetParser("<Header>1.1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1", versionFromHeader);
        }

        [Test]
        public override void TestGetStringVersionFromHeader_WithRevision_Found()
        {
            IStringParser parser = GetParser("<Header>1.1.1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.AreEqual("1.1.1", versionFromHeader);
        }
        
        private static IStringParser GetParser(string input)
        {
            return new XMLStringParser(input);
        }
    }
}
