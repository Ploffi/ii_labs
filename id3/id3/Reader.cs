using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace id3
{
	class Reader
	{
		private static readonly char Divider = ';';
		public List<Case> Read(string fileName)
		{
			var lines = File.ReadAllLines(fileName);
			var factorNames = lines[0].Split(Divider);
			return lines
				.Skip(1)
				.Select(
					str => str.Split(Divider)
						.Select((factor, i) => Tuple.Create(factorNames[i], factor))
						.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2)
				)
				.Select(dict => new Case(dict))
				.ToList();
		}
	}
}
