using System;
using System.Linq;

namespace PerceptronApp
{
	class BoolNeuron
	{
		private readonly int size;
		private readonly Func<double, double> _activationFunc;

		private readonly double[] weights;
		private double _bias;

		public BoolNeuron(int size, Func<double, double> activationFunc)
		{
			this.size = size;
			_activationFunc = activationFunc;
			weights = new double[this.size];

			var rand = new Random();
			for (var i = 0; i < size; ++i)
				weights[i] = rand.NextDouble();

			_bias = rand.NextDouble();
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

			_bias += error * learningRate;
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
			return mult.Concat(new[] { _bias }).Sum();
		}

		private double Result(double sum)
		{
			return _activationFunc(sum);
		}
	}
}
