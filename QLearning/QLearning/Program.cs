using System;
using System.Linq;

namespace QLearning
{
	class Program
	{
		static void Main(string[] args)
		{
			const int size = 3;
			var data = new Data(3);
			var qlearn = new QLearning(data);
			qlearn.Run(100);
			PrintData(data, size, size);
			PrintQTable(data, qlearn);
		}

		private static void PrintData(Data data, int n, int m)
		{			
			Console.WriteLine(" " + string.Concat(Enumerable.Range(0, n).Select(num => num+" ")));		
			for (var i = 0; i < m; i++)
			{
				Console.Write(i + " ");
				for (var j = 0; j < n; j++)
				{
					Console.Write(data.Field[Point.Create(j,i)] + " ");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		private static void PrintQTable(Data data, QLearning qlearn)
		{
			Console.Write("      ");
			foreach (var move in Point.Moves)
			{
				Console.Write(move + " ");
			}
			Console.WriteLine();
			foreach (var point in data.Field.Keys)
			{
				Console.Write(point + " ");
				foreach (var move in Point.Moves)
				{
					Console.Write(qlearn.QTable[(point, move)].ToString("F3") + " ");
				}
				Console.WriteLine();
			}
		}
	}
}