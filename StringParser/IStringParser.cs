namespace StringParser
{
    public interface IStringParser
    {
        string StringToParse { get; }
        string GetStringVersionFromHeader();
    }
}