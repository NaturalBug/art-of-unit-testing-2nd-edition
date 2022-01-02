namespace StringParser.nUnitTests
{
    class IISLogStringParserGenericTests : GenericParserTests<IISLogStringParser>
    {
        protected override string GetInputHeaderSingleDigit()
        {
            return "Header\t1";
        }
    }
}
