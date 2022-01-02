namespace StringParser.UnitTests
{
    public class IISLogStringParserTests : FillInTheBlankStringParserTests
    {
        protected override string HeaderVersion_SingleDigit { get { return $"header;version={EXPECTED_SINGLE_DIGIT};"; } }

        protected override string HeaderVersion_WithMiniorVersion { get { return $"header;version={EXPECTED_WITH_MINORVERSION};"; } }

        protected override string HeaderVersion_WithRevision { get { return $"header;version={EXPECTED_WITH_REVISION};"; } }

        protected override IStringParser GetParser(string input)
        {
            return new IISLogStringParser(input);
        }
    }
}
