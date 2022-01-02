using System;
using Xunit;

namespace StringParser.UnitTests
{
    public abstract class GenericParserTests<T> where T : IStringParser
    {
        protected abstract string GetInputHeaderSingleDigit();
        protected static T GetParser(string input)
        {
            return (T)Activator.CreateInstance(typeof(T), input);
        }

        [Fact]
        public void HasCorrectHeader_No_ReturnsFalse()
        {
            string input = GetInputHeaderSingleDigit();
            T parser = GetParser(input);

            bool result = parser.HasCorrectHeader();

            Assert.False(result);
        }
    }
}