namespace StringParser.UnitTests
{
    public class XmlStringParserGenericTests : GenericParserTests<XMLStringParser>
    {
        protected override string GetInputHeaderSingleDigit()
        {
            return "<header>1";
        }
    }
}
