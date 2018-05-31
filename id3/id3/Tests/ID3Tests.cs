using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace id3.Tests
{
	[TestFixture]
	// ReSharper disable once InconsistentNaming
	class ID3Tests
	{
		public void ShouldRightInitialize()
		{
			var p = new[]
			{
				("Josh", "3", "10"), ("Andrey", "5", "8"), ("Filip","2", "8")
			};
			var cases = p.Select(item => new Case(		
				new Dictionary<string, string>
				{
					{"Name", item.Item1},
					{"Strength", item.Item2},
					{"Weight", item.Item3},
				}
			));

			var dict = ID3Builder.GetValuesOfFactors(cases);

			Assert.Contains(dict["Name"], new [] { "Josh", "Andrey", "Filip" });
			Assert.Contains(dict["Strength"], new [] { "3", "5", "2" });
			Assert.Contains(dict["Weight"], new [] { "10", "8" });
		}

	}
}
