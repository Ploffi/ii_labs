using System;
using System.Collections.Generic;

namespace QLearning
{
	
	public class Data
	{
		public readonly Dictionary<Point, Rewards> Field;
		
		public Data(int n): this(n,n){}
		
		public Data(int n, int m)
		{
			Field = Generate(n,m);
		}

		private Dictionary<Point, Rewards> Generate(int n, int m)
		{
			var field = new Dictionary<Point, Rewards>(n*m);

			var isFinish = false;
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					var randReward = GetRandReward();
					while (isFinish &&  randReward == Rewards.Finish)
						randReward = GetRandReward();
					
					if (randReward == Rewards.Finish)
						isFinish = true;
					field[Point.Create(i, j)] = randReward;
				}
			}

			return field;
		}

		private static readonly Random Random = new Random();
		private static Rewards GetRandReward()
		{
			var values = Enum.GetValues(typeof(Rewards));
			return (Rewards) values.GetValue(Random.Next(values.Length));
		}
	}

	public enum Rewards
	{
		Good = 50,
		Normal = 10,
		Neutral = -1,
		Bad = -30,
		Finish = 1000,
	}
}