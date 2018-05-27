using System;
using System.Collections.Generic;

namespace id3
{
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
}
