using Xunit;

namespace StringParser.UnitTests
{
    public abstract class FillInTheBlankStringParserTests
    {
        protected abstract IStringParser GetParser(string input);
        protected abstract string HeaderVersion_SingleDigit { get; }
        protected abstract string HeaderVersion_WithMiniorVersion { get; }
        protected abstract string HeaderVersion_WithRevision { get; }
        public const string EXPECTED_SINGLE_DIGIT = "1";
        public const string EXPECTED_WITH_REVISION = "1.1.1";
        public const string EXPECTED_WITH_MINORVERSION = "1.1";
        
        [Fact]
        public void GetStringVersionFromHeader_SingleDigit_Found()
        {
            string input = HeaderVersion_SingleDigit;
            IStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal(EXPECTED_SINGLE_DIGIT, versionFromHeader);
        }
        
        [Fact]
        public void GetStringVersionFromHeader_WithMinorVersion_Found()
        {
            string input = HeaderVersion_WithMiniorVersion; 
            IStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal(EXPECTED_WITH_MINORVERSION, versionFromHeader);
        }
        
        [Fact]
        public void GetStringVersionFromHeader_Revision_Found()
        {
            string input = HeaderVersion_WithRevision; 
            IStringParser parser = GetParser(input);

            string versionFromHeader = parser.GetStringVersionFromHeader();

            Assert.Equal(EXPECTED_WITH_REVISION, versionFromHeader);
        }
    }
}