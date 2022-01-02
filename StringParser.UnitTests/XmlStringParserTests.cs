using System;
using Xunit;

namespace StringParser.UnitTests
{
    public class XmlStringParserTests : TemplateStringParserTests
    {
        [Fact]
        public override void TestGetStringVersionFromHeader_SingleDigit_Found()
        {
            IStringParser parser = GetParser("<Header>1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1", versionFromHeader);
        }

        [Fact]
        public override void TestGetStringVersionFromHeader_WithMinorVersion_Found()
        {
            IStringParser parser = GetParser("<Header>1.1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1.1", versionFromHeader);
        }

        [Fact]
        public override void TestGetStringVersionFromHeader_WithRevision_Found()
        {
            IStringParser parser = GetParser("<Header>1.1.1</Header>");

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1.1.1", versionFromHeader);
        }
        
        private static IStringParser GetParser(string input)
        {
            return new XMLStringParser(input);
        }
    }
}
