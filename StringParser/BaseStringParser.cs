namespace StringParser
{
    public abstract class BaseStringParser : IStringParser
    {
        protected string StringToParse { get; }

        public BaseStringParser(string stringToParse)
        {
            StringToParse = stringToParse;
        }

        public abstract string GetStringVersionFromHeader();
    }
}