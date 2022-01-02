using Xunit;

namespace StringParser.UnitTests
{
    public class StandardStringParserTests
    {
        [Fact]
        public void GetStringVersionFromHeader_SingleDigit_Found()
        {
            string input = "header;version=1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1", versionFromHeader);
        }
        
        [Fact]
        public void GetStringVersionFromHeader_WithMinorVersion_Found()
        {
            string input = "header;version=1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1.1", versionFromHeader);
        }
        
        [Fact]
        public void GetStringVersionFromHeader_Revision_Found()
        {
            string input = "header;version=1.1.1;\n";
            StandardStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal("1.1.1", versionFromHeader);
        }

        private static StandardStringParser GetParser(string input)
        {
            return new StandardStringParser(input);
        }
    }
}
