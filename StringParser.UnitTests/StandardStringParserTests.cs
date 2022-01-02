namespace StringParser.UnitTests
{
    public class StandardStringParserTests : FillInTheBlankStringParserTests
    {
        protected override string HeaderVersion_SingleDigit { get { return $"header\tversion={EXPECTED_SINGLE_DIGIT}\t\n"; } }

        protected override string HeaderVersion_WithMiniorVersion { get { return $"header\tversion={EXPECTED_WITH_MINORVERSION}\t\n"; } }

        protected override string HeaderVersion_WithRevision { get { return $"header\tversion={EXPECTED_WITH_REVISION}\t\n"; } }

        protected override IStringParser GetParser(string input)
        {
            return new StandardStringParser(input);
        }
    }
}
