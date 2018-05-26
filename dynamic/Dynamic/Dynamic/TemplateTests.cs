using System.IO;
using NUnit.Framework;

namespace Dynamic_9
{
	[TestFixture]
	internal class TemplateTests
	{

		private const string path = @"C:\work\ii_labs\output.txt";
		[OneTimeSetUp]
	    public void Setup()
	    {
		    File.Delete(path);
	    }

	    [TestCase("AB", "AB", 2)]
	    [TestCase("AC", "AB", -1)]
	    [TestCase("A*", "*B", 2)]
	    [TestCase("*", "B", 1)]
	    [TestCase("?*?", "B?CDF", 5)]
	    [TestCase("A?C?D", "*", 5)]
	    [TestCase("A?C?D", "B*D", -1)]
	    [TestCase("A", "B", -1)]
	    [TestCase("BCD", "B*K*", -1)]
	    [TestCase("BCD", "B*C*", 3)]
	    [TestCase("He*lo", "Hell?", 5)]
	    [TestCase("He*lo", "Hel?o", 5)]
		[Test]
	    public void Test(string template1, string template2, int expected)
	    {
		    var solver = new Solver();
		    var actual = solver.Solve(template1, template2);
			WriteToFile(template1, template2, actual);
			Assert.AreEqual(expected, actual);
	    }

	    private void WriteToFile(string template1, string template2, int result)
	    {
		    File.AppendAllText(path, $"{template1} ; {template2} ; {result}\n");
	    }

    }
}
