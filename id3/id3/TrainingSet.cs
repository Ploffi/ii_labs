using System.Collections.Generic;

namespace id3
{
	class Case
	{
		public readonly Dictionary<string, string> Factors;

		public Case(Dictionary<string, string> factors)
		{
			Factors = factors;
		}
	}
}
