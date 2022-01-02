namespace StringParser
{
    public abstract class BaseStringParser : IStringParser
    {
        public string StringToParse { get; }

        public BaseStringParser(string stringToParse)
        {
            StringToParse = stringToParse;
        }

        public abstract string GetStringVersionFromHeader();

        public abstract bool HasCorrectHeader();
    }
}