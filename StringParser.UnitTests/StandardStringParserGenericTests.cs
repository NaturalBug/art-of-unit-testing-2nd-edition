namespace StringParser.UnitTests
{
    public class StandardStringParserGenericTests : GenericParserTests<StandardStringParser>
    {
        protected override string GetInputHeaderSingleDigit()
        {
            return "Header;1";
        }
    }
}
