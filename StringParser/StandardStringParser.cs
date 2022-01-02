using System.Text.RegularExpressions;

namespace StringParser
{
    public class StandardStringParser : BaseStringParser
    {
        private readonly Regex versionRegex = new(@"header\tversion=(?<version>[^\t]+)");
        
        public StandardStringParser(string stringToParse): base(stringToParse)
        {
        }

        public override string GetStringVersionFromHeader()
        {
            return versionRegex.Match(StringToParse).Groups["version"].Value;
        }

        public override bool HasCorrectHeader()
        {
            return versionRegex.IsMatch(StringToParse);
        }
    }
}