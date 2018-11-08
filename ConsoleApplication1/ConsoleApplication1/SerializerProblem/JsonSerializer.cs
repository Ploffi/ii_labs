using Newtonsoft.Json;

namespace ConsoleApplication1.SerializerProblem
{
    public class JsonSerializer: ISerializer
    {
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}