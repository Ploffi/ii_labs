using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConsoleApplication1.SerializerProblem.Tests
{
    [TestFixture]
    public class Unit
    {
        [TestCase(
            "{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4]}",
            "{\"SumResult\":30.30,\"MulResult\":4,\"SortedInputs\":[1.0,1.01,2.02,4.0]}"
        )]
        public void TestJson(string json, string expected)
        {
            Test("Json", json, expected);
        } 
        
        [TestCase(
            "<Input><K>10</K><Sums><decimal>1.01</decimal><decimal>2.02</decimal></Sums><Muls><int>1</int><int>4</int></Muls></Input>",
            "<Output><MulResult>4</MulResult><SortedInputs><element>1.0</element><element>1.01</element><element>2.02</element><element>4.0</element></SortedInputs><SumResult>30.3</SumResult></Output>"
        ), Explicit]
        public void TestXml(string xml, string expected)
        {
            Test("xml", xml, expected);
        }

        private void Test(string providerType, string input, string expectedOutput)
        {
            var serializeSolver = new SerializeSolver();
            var memoryProvider = new MemoryProvider(new[] {providerType, input});
            serializeSolver.Resolve(memoryProvider);
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