namespace StringParser.nUnitTests
{
    class XmlStringParserTests : FillInTheBlankStringParserTests
    {
        protected override string HeaderVersion_SingleDigit { get { return $"<Header>{EXPECTED_SINGLE_DIGIT}</Header>"; } }

        protected override string HeaderVersion_WithMiniorVersion { get { return $"<Header>{EXPECTED_WITH_MINORVERSION}</Header>"; } }

        protected override string HeaderVersion_WithRevision { get { return $"<Header>{EXPECTED_WITH_REVISION}</Header>"; } }

        protected override IStringParser GetParser(string input)
        {
            return new XMLStringParser(input);
        }
    }
}
