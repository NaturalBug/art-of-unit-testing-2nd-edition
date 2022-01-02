using System.Xml;

namespace StringParser
{
    public class XMLStringParser : BaseStringParser
    {
        public XMLStringParser(string stringToParse) : base(stringToParse)
        {
        }

        public override string GetStringVersionFromHeader()
        {
            var XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(StringToParse);
            return XmlDoc.SelectSingleNode("Header").InnerText;
        }
    }
}