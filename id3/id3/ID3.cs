using System.Collections.Generic;
using System.Linq;

namespace id3
{
	// ReSharper disable once InconsistentNaming
	class ID3
	{
		private readonly Node _root;

		public ID3(List<Case> cases, string decisionFactor)
		{
			_root = new ID3Builder(cases, decisionFactor).Build();
		}

		public Result GetResult(Case @case)
		{
			return GetResult(@case, _root);
		}

		private Result GetResult(Case @case, Node node)
		{
			var value = @case.Factors[node.FactorName];
			var child = node.Childrens.First(c => c.factorValue == value).child;
			if (child.Result != Result.None)
				return child.Result;
			return GetResult(@case, child);
		}
	}
}
