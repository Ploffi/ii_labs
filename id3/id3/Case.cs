using System.Collections.Generic;
using System.Linq;

namespace id3
{
	class Case
	{
		public readonly Dictionary<string, string> Factors;

		public Case(Dictionary<string, string> factors)
		{
			Factors = factors;
		}

		public override string ToString()
		{
			return string.Join(",", Factors.Select(k => $"{k.Key}={k.Value}"));
		}
	}
}
