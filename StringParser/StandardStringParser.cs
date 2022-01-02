using System.Text.RegularExpressions;

namespace StringParser
{
    public class StandardStringParser
    {
        private readonly string input;

        public StandardStringParser(string input)
        {
            this.input = input;
        }

        public string GetStringVersionFromHeader()
        {
            return Regex.Match(input, @"header;version=(?<version>[^;]+)").Groups["version"].Value;
        }
    }
}