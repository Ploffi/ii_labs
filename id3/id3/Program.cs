using System;

namespace id3
{
	class Program
	{
		static void Main(string[] args)
		{
			var reader = new Reader();
			var trainData = reader.Read("train.csv");
			var decisionsTree = new ID3(trainData, "Decision");
			var checkData = reader.Read("check.csv");

			Console.WriteLine();
			Console.WriteLine();

			foreach (var checkCase in checkData)
			{
				Console.WriteLine(checkCase);
				Console.WriteLine(decisionsTree.GetResult(checkCase));
			}
		}
	}
}
