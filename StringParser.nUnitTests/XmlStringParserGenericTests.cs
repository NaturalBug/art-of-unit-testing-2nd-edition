namespace StringParser.nUnitTests
{
    class XmlStringParserGenericTests : GenericParserTests<XMLStringParser>
    {
        protected override string GetInputHeaderSingleDigit()
        {
            return "<header>1";
        }
    }
}
