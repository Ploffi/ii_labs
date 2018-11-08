using System;
using ConsoleApplication1.SerializerProblem;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var consoleProvider = new ConsoleProvider();
            var serializeSolver = new SerializeSolver();
            serializeSolver.Resolve(consoleProvider);
        } 
        
        class ConsoleProvider: IMessageProvider
        {
            public string Read()
            {
                return Console.ReadLine();
            }

            public void Write(string output)
            {
                Console.WriteLine(output);
            }
        }
    }
}