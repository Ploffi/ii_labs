using System;
using System.Linq;

namespace ConsoleApplication1.SerializerProblem
{
    public class SerializeSolver : IProblemResolver
    {
        public void Resolve(IMessageProvider messageProvider)
        {
            var type = messageProvider.Read().ToLower();
            var serializer = GetSerializer(type);
            var inputData = messageProvider.Read();
            var result = serializer.Deserialize<Input>(inputData);
            var calculated = Calculate(result);
            var serializedCalculated = serializer.Serialize(calculated);
            messageProvider.Write(serializedCalculated);
        }

        private Output Calculate(Input input)
        {
            return new Output()
            {
                MulResult = input.Muls.Aggregate(1, Mul),
                SumResult = input.Sums.Sum()*input.K,
                SortedInputs = input.Muls
                    .Select(a => (decimal)a)
                    .Concat(input.Sums)
                    .OrderBy(a => a)
                    .ToArray()
            };

            int Mul(int a, int b) => a * b;
        }

        private ISerializer GetSerializer(string type)
        {
            switch (type)
            {
                    case "json":
                        return new JsonSerializer();
                    case "xml":
                        return new XmlSerializer();
                    default:
                        throw new Exception($"{type} is not supported yet");
            }
        }
    }

    interface ISerializer
    {
        string Serialize<T>(T value);
        T Deserialize<T>(string value);
    }
}