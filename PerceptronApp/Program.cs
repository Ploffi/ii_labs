using System;
using System.Linq;

namespace PerceptronApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new NeuroWeb();
            var epoch = 10;

            for (var i = 0; i < epoch; ++i)
            {
                web.Train(new []{0.0, 0.0}, 0.0);
                web.Train(new []{0.0, 1.0}, 0.0);
                web.Train(new []{1.0, 0.0}, 1.0);
                web.Train(new []{1.0, 1.0}, 0.0);
                
                Console.Write(web.Calculate(new []{0.0, 0.0}) + " ");
                Console.Write(web.Calculate(new []{0.0, 1.0}) + " ");
                Console.Write(web.Calculate(new []{1.0, 0.0}) + " ");
                Console.Write(web.Calculate(new []{1.0, 1.0}) + " ");
                Console.WriteLine();
            }
        }
    }

    class NeuroWeb
    {
        private BoolNeuron neuron;
        private const double learningRate = 0.1;

        public NeuroWeb()
        {
            neuron = new BoolNeuron(2);
        }

        public void Train(double[] input, double expected)
        {
            var calcResult = neuron.Calculate(input);
            var error = expected - calcResult;

            neuron.UpdateWeights(input, error, learningRate);
        }

        public double Calculate(double[] input)
        {
            return neuron.Calculate(input);
        }
    }

    class BoolNeuron
    {
        private readonly int size;
        
        private double[] weights;
        private double bias;

        public BoolNeuron(int size)
        {
            this.size = size;
            weights = new double[this.size];
            
            var rand = new Random();
            for (var i = 0; i < size; ++i)
                weights[i] = rand.NextDouble();

            bias = rand.NextDouble();
        }

        public double Calculate(double[] input)
        {
            var mult = Multiplication(input);
            var sum = Sum(mult);
            return Result(sum);
        }

        public void UpdateWeights(double[] input, double error, double learningRate)
        {
            for (var i = 0; i < size; ++i)
            {
                weights[i] += error * input[i] * learningRate;
            }
            
            bias += error * learningRate;
        }

        private double[] Multiplication(double[] input)
        {
            var result = new double[size];
            
            for (var i = 0; i < size; ++i)
            {
                result[i] = input[i] * weights[i];
            }

            return result;
        }

        private double Sum(double[] mult)
        {
            return mult.Concat(new[]{bias}).Sum();
        }

        private static int Result(double sum)
        {
            return sum >= 0 ? 1 : 0;
        }
    }
}