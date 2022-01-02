using System.Text.RegularExpressions;

namespace StringParser
{
    public class StandardStringParser : BaseStringParser
    {
        public StandardStringParser(string stringToParse): base(stringToParse)
        {
        }

        public override string GetStringVersionFromHeader()
        {
            return Regex.Match(StringToParse, @"header;version=(?<version>[^;]+)").Groups["version"].Value;
        }
    }
}