using System.Text.RegularExpressions;

namespace StringParser
{
    public class IISLogStringParser : BaseStringParser
    {
        public IISLogStringParser(string stringToParse) : base(stringToParse)
        {
        }

        public override string GetStringVersionFromHeader()
        {
            return Regex.Match(StringToParse, @"header;?version=(?<version>[^;]+);").Groups["version"].Value;
        }
    }
}