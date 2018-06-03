using System;

namespace PerceptronApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Определяемая каждым нейроном цифра (на каждой 10 эпохе)");
			Console.WriteLine();
			TrainNumberDetecting();
		}

		private static void TrainNumberDetecting()
		{
			var web = new NumberFunc(NumbersMatrix.Numbers[0].Length);
			var epoch = 100;

			for (var i = 0; i < epoch; ++i)
			{
				for (var j = 0; j < NumbersMatrix.Numbers.Count; j++)
				{
					web.Train(NumbersMatrix.Numbers[j], j);
				}

				if (i % 10 != 0) continue;
				foreach (var number in NumbersMatrix.Numbers)
				{
					Console.Write(web.Calculate(number) + " ");
				}

				Console.WriteLine();
				Console.WriteLine();
			}
		}

		private static void TrainLogicFunc()
		{
			var web = new LogicFunc();
			var epoch = 10;

			for (var i = 0; i < epoch; ++i)
			{
				web.Train(new[] { 0.0, 0.0 }, 0.0);
				web.Train(new[] { 0.0, 1.0 }, 0.0);
				web.Train(new[] { 1.0, 0.0 }, 1.0);
				web.Train(new[] { 1.0, 1.0 }, 0.0);

				Console.Write(web.Calculate(new[] { 0.0, 0.0 }) + " ");
				Console.Write(web.Calculate(new[] { 0.0, 1.0 }) + " ");
				Console.Write(web.Calculate(new[] { 1.0, 0.0 }) + " ");
				Console.Write(web.Calculate(new[] { 1.0, 1.0 }) + " ");
				Console.WriteLine();
			}
		}
	}
}