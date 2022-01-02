using NUnit.Framework;
using System;

namespace StringParser.nUnitTests
{
    internal abstract class GenericParserTests<T> where T : IStringParser
    {
        protected abstract string GetInputHeaderSingleDigit();
        protected static T GetParser(string input)
        {
            return (T)Activator.CreateInstance(typeof(T), input);
        }

        [Test]
        public void HasCorrectHeader_No_ReturnsFalse()
        {
            string input = GetInputHeaderSingleDigit();
            T parser = GetParser(input);

            bool result = parser.HasCorrectHeader();

            Assert.IsFalse(result);
        }
    }
}