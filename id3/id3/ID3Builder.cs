using System;
using System.Collections.Generic;
using System.Linq;

namespace id3
{
	// ReSharper disable once InconsistentNaming
	class ID3Builder
	{
		private const string PositiveDecision = "Yes";
		private const string NegativeDecision = "No";
		private static readonly Dictionary<Result, string> Decisions = new Dictionary<Result, string>
		{
			{
				Result.Positive,
				PositiveDecision
			},
			{
				Result.Negative,
				NegativeDecision
			},
		};

		private readonly string _decisionFactor;
		private readonly Dictionary<string, List<string>> _valuesOfFactors;
		private readonly List<Case> _allCases;

		public ID3Builder(List<Case> cases, string decisionFactor)
		{
			_decisionFactor = decisionFactor;
			_allCases = cases;
			_valuesOfFactors = GetValuesOfFactors(cases);
		}

		public static Dictionary<string, List<string>> GetValuesOfFactors(IEnumerable<Case> cases) => cases
			.SelectMany(c => c.Factors)
			.GroupBy(pair => pair.Key)
			.ToDictionary(group => group.Key, group => group.Select(f => f.Value).Distinct().ToList());

		public Node Build()
		{
			var factors = _valuesOfFactors.Keys.Where(factor => factor != _decisionFactor).ToList();
			var targetFactor = GetBestFactor(_allCases, factors);
			Console.WriteLine($"BestFactor is {targetFactor}");
			return Build(_allCases, factors.Where(factor => factor != targetFactor).ToList(), targetFactor);
		}

		private Node Build(List<Case> cases, List<string> factors, string targetFactor)
		{
			var node = new Node
			{
				FactorName = targetFactor,
				Childrens = new List<(string factorValue, string factorName, Node child)>(),
				Result = Result.None,
			};

			if (SetSingleRootIfPossible(node, cases))
				return node;

			var values = _valuesOfFactors[targetFactor];

			foreach (var targetFactorValue in values)
			{
				var casesWithCurrentValue = cases.Where(c => c.Factors[targetFactor] == targetFactorValue).ToList();
				var appendingNode = (targetFactorValue, factorName: (string)null, node: new Node());
				if (SetSingleRootIfPossible(appendingNode.node, casesWithCurrentValue))
				{
					Console.WriteLine($"all cases have result {appendingNode.node.Result} for factorValue: {appendingNode.Item1}");
					node.Childrens.Add(
						appendingNode
					);
					continue;
				}
				if (casesWithCurrentValue.Any())
				{
					var targetFactorForValue = GetBestFactor(casesWithCurrentValue, factors);
					var factorsWithoutTarget = factors.Where(f => f != targetFactorForValue && f != targetFactor).ToList();
					appendingNode.node = Build(casesWithCurrentValue, factorsWithoutTarget, targetFactorForValue);
					appendingNode.factorName = targetFactorForValue;
				}
				else
				{
					var allCasesWithThisValue = _allCases.Where(c => c.Factors[targetFactor] == targetFactorValue).ToList();
					var successCount = allCasesWithThisValue.Count(c => c.Factors[_decisionFactor] == Decisions[Result.Positive]);
					var result = successCount * 2 >= allCasesWithThisValue.Count ? Result.Positive : Result.Negative;
					appendingNode.node.Result = result;
				}

				Console.WriteLine($"target factor {appendingNode.factorName} for factorValue: {appendingNode.Item1}");
				node.Childrens.Add(
					appendingNode
				);
			}

			return node;
		}

		private string GetBestFactor(List<Case> cases, IReadOnlyList<string> factors)
		{
			var bestFactor = factors.ElementWithMaxCost(otherProperty => CalculateGain(cases, otherProperty));
			return bestFactor;
		}

		private double CalculateGain(List<Case> cases, string calculatingProperty)
		{
			var summedEntropyForProperty = _valuesOfFactors[calculatingProperty].Sum(v => CalculateGainForPropertyValue(v));
			var entropy = CalculateEntropy(cases);
			var gain = entropy - summedEntropyForProperty;
			Console.WriteLine($"gain for property {calculatingProperty}: {gain:F4}");
			return gain;

			double CalculateGainForPropertyValue(string calculatingPropertyValue)
			{
				var casesWithValue = cases.Where(c => c.Factors[calculatingProperty] == calculatingPropertyValue).ToList();
				if (!casesWithValue.Any())
					return 0;
				var pValueOfAllCases = (double)casesWithValue.Count / cases.Count;
				return CalculateEntropy(casesWithValue) * pValueOfAllCases;
			}
		}

		private double CalculateEntropy(List<Case> cases)
		{
			var positive = cases.Count(c => c.Factors[_decisionFactor] == PositiveDecision);

			var pPositive = (double)positive / cases.Count;
			var pNegative = 1 - pPositive;
			if (Math.Abs(pPositive) < double.Epsilon || Math.Abs(pNegative) < double.Epsilon)
				return 0;
			return -pPositive * Math.Log(pPositive, 2) - pNegative * Math.Log(pNegative, 2);
		}


		private bool SetSingleRootIfPossible(Node node, List<Case> examples)
		{
			node.Result = CheckIfAll(Result.Positive, examples)
				? Result.Positive
				: CheckIfAll(Result.Negative, examples)
					? Result.Negative
					: Result.None;
			return node.Result != Result.None;
		}

		private bool CheckIfAll(Result expectedResult, List<Case> cases) =>
			cases.All(@case => @case.Factors[_decisionFactor] == Decisions[expectedResult]);

	}

	public enum Result
	{
		None,
		Negative,
		Positive,
	}
}
