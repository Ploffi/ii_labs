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
			var cases = new []
			{
				new Case(
					new Dictionary<string, string>()
					{
						{"Name", "Josh"},
						{"Name", "Josh"},
						{"Name", "Josh"},
					}
				)
			};
		}

	}
}
