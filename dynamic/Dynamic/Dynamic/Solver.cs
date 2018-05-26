using System;

namespace Dynamic_9
{
	class Solver
	{
		public int Run()
		{
			var template1 = Console.ReadLine();
			var template2 = Console.ReadLine();

			return Solve(template1, template2);
		}

		public int Solve(string first, string second)
		{
			var len1 = first.Length;
			var len2 = second.Length;
			var computed = new int[len1+1, len2+1];
			const int BigNumber = 99;
			FillArray(computed, BigNumber);
			computed[0, 0] = 0;

			for (int i = 0; i < len1; i++)
			{
				for (int j = 0; j < len2; j++)
				{
					char f = first[i], s = second[j];
					int current = computed[i,j];

					if ((f == s || f == '?' || s == '?') && computed[i + 1,j + 1] > current + 1)
					{
						computed[i + 1,j + 1] = current + 1;
					}

					if (f == '*')
					{
						var curlen = 0;
						for (var len = 0; j + len <= len2; ++len)
						{
							if (computed[i + 1,j + len] > current + curlen)
								computed[i + 1,j + len] = current + curlen;

							if (j + len < len2 && second[j + len] != '*')
								curlen ++;
						}
					}

					if (s == '*')
					{
						var curlen = 0;
						for (var len = 0; i + len <= len1; ++len)
						{
							if (computed[i + len,j + 1] > current + curlen)
								computed[i + len,j + 1] = current + curlen;

							if (i + len < len1 && first[i+len] != '*')
								curlen ++;
						}
					}
				}
			}

			var result = computed[len1, len2];
			var answer = result > BigNumber -1 ? -1 : result;
			return answer;
		}

		public static void FillArray(int[,] array, int val)
		{
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					array[i, j] = val;
				}
			}
		}
	}
}

