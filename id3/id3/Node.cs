using System.Collections.Generic;
using id3;

class Node
{
	public string FactorName;
	public Result Result;
	public List<(string factorValue, string factorName, Node child)> Childrens;
}