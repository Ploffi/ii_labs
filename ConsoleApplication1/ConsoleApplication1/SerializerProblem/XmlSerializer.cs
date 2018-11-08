using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ConsoleApplication1.SerializerProblem
{
    public class XmlSerializer : ISerializer
    {
        private readonly XmlSerializerHelper _serializer = new XmlSerializerHelper();

        public string Serialize<T>(T value)
        {
            var serialized = _serializer.SerializeToXml(value);
            var cleaned = Regex.Replace(serialized, @"<Output(.*?)>", "<Output>");
            return cleaned
                .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "")
                .Replace("\n", "")
                .Replace(" ", "");
        }

        public T Deserialize<T>(string value)
        {
            return _serializer.DeserializeFromXml<T>(value);
        }
    }
}