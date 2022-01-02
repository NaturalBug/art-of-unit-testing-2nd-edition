namespace StringParser.UnitTests
{
    public class IISLogStringParserGenericTests : GenericParserTests<IISLogStringParser>
    {
        protected override string GetInputHeaderSingleDigit()
        {
            return "Header\t1";
        }
    }
}
