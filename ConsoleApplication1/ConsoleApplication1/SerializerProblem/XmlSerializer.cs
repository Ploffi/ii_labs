using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ConsoleApplication1.SerializerProblem
{
    public class XmlSerializer : ISerializer
    {
        private readonly XmlSerializerHelper _serializer = new XmlSerializerHelper();

        public string Serialize<T>(T value)
        {
            return _serializer.SerializeToXml(value)
                .Replace("\n", "")
                .Trim();
        }

        public T Deserialize<T>(string value)
        {
            return _serializer.DeserializeFromXml<T>(value);
        }
    }
}