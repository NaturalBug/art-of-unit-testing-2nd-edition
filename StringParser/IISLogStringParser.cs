using System.Text.RegularExpressions;

namespace StringParser
{
    public class IISLogStringParser : BaseStringParser
    {
        private readonly Regex versionRegex = new(@"header;version=(?<version>[^;]+);", RegexOptions.Compiled);

        public IISLogStringParser(string stringToParse) : base(stringToParse)
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