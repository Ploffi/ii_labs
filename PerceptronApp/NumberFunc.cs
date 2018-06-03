using System;
using System.Collections.Generic;
using System.Linq;

namespace PerceptronApp
{
	class NumberFunc
	{
		private readonly BoolNeuron[] _neurons;
		private static readonly double LearningRate = 0.01;
		public NumberFunc(int matrixLength)
		{
			Func<double, double> sigmoud = sum => 1 / (1 + Math.Pow(Math.E, -sum));
			_neurons = Enumerable.Range(0, 10).Select(num => new BoolNeuron(matrixLength, sigmoud)).ToArray();
		}

		public void Train(double[] input, int expectedNumber)
		{
			for (var i = 0; i < _neurons.Length; i++)
			{
				var result = _neurons[i].Calculate(input);
				var error = i == expectedNumber
					? 1 - result
					: 0 - result;
				_neurons[i].UpdateWeights(input, error, LearningRate);
			}
		}

		public int Calculate(double[] input)
		{
			var result = _neurons.Select(n => n.Calculate(input)).ToArray();
			var max = result.Max();
			var index = Array.IndexOf(result, max);
			return index;
		}
	}

	public static class Extensions
	{
		public static T ElementWithMaxCost<T>(this IEnumerable<T> collection, Func<T, double> costCallculator)
		{
			var maxCost = double.MinValue;
			var element = default(T);

			foreach (var item in collection)
			{
				var cost = costCallculator(item);
				if (cost <= maxCost) continue;

				maxCost = cost;
				element = item;
			}

			return element;
		}
	}


	class NumbersMatrix
	{
		public static List<double[]> Numbers = new List<double[]>()
		{
			new double[]
			{
				0, 1, 1, 0,
				1, 0, 0, 1,
				1, 0, 0, 1,
				0, 1, 1, 0
			},
			new double[]
			{
				0, 0, 1, 0,
				0, 1, 1, 0,
				0, 0, 1, 0,
				0, 0, 1, 0
			},
			new double[]
			{
				0, 1, 1, 1,
				0, 0, 0, 1,
				0, 0, 1, 0,
				0, 1, 1, 1,
			},
			new double[]
			{
				0, 0, 1, 0,
				0, 0, 0, 1,
				0, 0, 1, 0,
				0, 0, 0, 1,
			},
			new double[]
			{
				0, 0, 0, 1,
				0, 0, 1, 1,
				0, 1, 1, 1,
				0, 0, 0, 1,
			},
			new double[]
			{
				0, 1, 1, 1,
				0, 1, 0, 0,
				0, 0, 1, 0,
				0, 1, 0, 0,
			},
			new double[]
			{
				0, 0, 1, 1,
				0, 1, 0, 0,
				0, 1, 1, 1,
				0, 1, 1, 0,
			},
			new double[]
			{
				1, 1, 1, 1,
				0, 0, 0, 1,
				0, 0, 1, 0,
				0, 1, 0, 0,
			},
			new double[]
			{
				0, 1, 1, 0,
				1, 0, 0, 1,
				0, 1, 1, 0,
				0, 1, 1, 0,
			},
			new double[]
			{
				0, 0, 1, 1,
				0, 1, 0, 1,
				0, 0, 1, 1,
				0, 1, 1, 0,
			},
		};
	}
}
