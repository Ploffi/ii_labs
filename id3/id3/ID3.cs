using System;
using System.Collections.Generic;
using System.Linq;

namespace id3
{
	// ReSharper disable once InconsistentNaming
	class ID3
	{
		private const string PositiveDecision = "yes";
		private const string NegativeDecision = "no";
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

		public ID3(List<Case> cases, string decisionFactor)
		{
			_decisionFactor = decisionFactor;
			_allCases = cases;
			_valuesOfFactors = GetValuesOfFactors(cases);
		}

		public static Dictionary<string, List<string>> GetValuesOfFactors(IEnumerable<Case> cases) => cases
			.SelectMany(c => c.Factors)
			.GroupBy(pair => pair.Key, pair => pair.Value)
			.ToDictionary(group => group.Key, group => group.ToList());

		public Node Build()
		{
			var factors = _valuesOfFactors.Keys;
			var targetFactor = GetBestFactor(_allCases, factors);
			return Build(_allCases, factors.ToList(), targetFactor);
		}

		private Node Build(List<Case> cases, List<string> factors, string targetFactor)
		{
			var node = new Node
			{
				FactorName = targetFactor,
				Childrens = new List<(string factorValue, Node child)>(),
				Result = Result.None,
			};

			if (SetSingleRootIfPossible(node, cases))
				return node;

			var values = _valuesOfFactors[targetFactor];

			foreach (var targetFactorValue in values)
			{
				var casesWithCurrentValue = cases.Where(c => c.Factors[targetFactor] == targetFactorValue).ToList();
				var targetFactorForValue = GetBestFactor(casesWithCurrentValue, factors);
				var factorsWithoutTarget = factors.Where(f => f != targetFactorForValue).ToList();

				node.Childrens.Add(
					(
						targetFactorForValue,
						Build(casesWithCurrentValue, factorsWithoutTarget, targetFactorForValue)
					)
				);
			}

			return node;
		}

		private string GetBestFactor(List<Case> cases, IEnumerable<string> factors)
		{
			return factors.ElementWithMaxCost(otherProperty => CalculateGain(cases, otherProperty));
		}

		private double CalculateGain(List<Case> cases, string calculatingProperty)
		{
			var summedEntropyForProperty = _valuesOfFactors[calculatingProperty].Sum(v => CalculateGainForProperyValue(v));
			var entropy = CalculateEntropy(cases);

			return entropy - summedEntropyForProperty;

			double CalculateGainForProperyValue(string calculatingPropertyValue)
			{
				var casesWithValue = cases.Where(c => c.Factors[calculatingProperty] == calculatingPropertyValue).ToList();
				var pValueOfAllCases = casesWithValue.Count / cases.Count;
				return CalculateEntropy(casesWithValue) * pValueOfAllCases;
			}
		}

		private double CalculateEntropy(List<Case> cases)
		{
			var positive = cases.Count(c => c.Factors[_decisionFactor] == PositiveDecision);

			var pPositive = positive / cases.Count;
			var pNegative = 1 - pPositive;

			return -pPositive * Math.Log(positive, 2) - pNegative * Math.Log(pNegative, 2);
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
