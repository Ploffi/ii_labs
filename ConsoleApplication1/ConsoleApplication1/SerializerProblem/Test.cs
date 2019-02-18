using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConsoleApplication1.SerializerProblem
{
    [TestFixture]
    public class Unit
    {
        [Test]
        public void TestJsonSerializer()
        {
            var data = new Input()
            {
                Sums = new [] {1.0m, 4.0m},
                Muls = new [] {3,4},
                K = 5
            };
            var jsonSerializer = new JsonSerializer();
            var serialized = jsonSerializer.Serialize(data);
            var deserialized = jsonSerializer.Deserialize<Input>(serialized);
            
            Assert.AreEqual(data, deserialized);
        } 
        
        [Test]
        public void TestXmlSerializer()
        {
            var data = new Input()
            {
                Sums = new [] {1.0m, 4.0m},
                Muls = new [] {3,4},
                K = 5
            };
            var xmlSerializer = new XmlSerializer();
            var serialized = xmlSerializer.Serialize(data);
            var deserialized = xmlSerializer.Deserialize<Input>(serialized);
            
            Assert.AreEqual(data, deserialized);
        } 
        
        [TestCase(
            "{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4]}",
            "{\"SumResult\":30.30,\"MulResult\":4,\"SortedInputs\":[1.0,1.01,2.02,4.0]}"
        )]
        public void TestJsonResolver(string json, string expected)
        {
            TestSolver("Json", json, expected);
        } 
        
        private void TestSolver(string providerType, string input, string expectedOutput)
        {
            var serializeSolver = new SerializeSolver();
            var memoryProvider = new MemoryProvider(new[] {providerType, input});
            serializeSolver.Resolve(memoryProvider);
            Console.WriteLine(memoryProvider.Output);
            Assert.AreEqual(expectedOutput, memoryProvider.Output);
        }        
    }

    class MemoryProvider : IMessageProvider
    {
        private readonly Stack<string> _input;
        public string Output;

        public MemoryProvider(string[] input)
        {
            _input = new Stack<string>(input.Reverse());
        }

        public string Read()
        {
            return _input.Pop();
        }

        public void Write(string output)
        {
            this.Output = output;
        }
    }
}