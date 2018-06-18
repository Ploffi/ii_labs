using System;
using System.Collections.Generic;
using System.Linq;

namespace QLearning
{
	public class QLearning
	{
		private readonly Data data;
		public readonly Dictionary<(Point, Move), double> QTable;
		private int Reward(Point p) => (int) data.Field[p];
		private bool CanMoveTo(Point p) => data.Field.ContainsKey(p);
		private readonly Random _random;
		private Point _currentState;
		private readonly double _learningRate;
		private readonly double _discountRate;

		public QLearning(Data data)
		{
			this.data = data;
			_random = new Random();
			_currentState = Point.Create(0, 0);
			_discountRate = 0.5;
			_learningRate = 0.01;
			QTable = data.Field.Keys
				.SelectMany(p => new[] {(p, Move.Top), (p, Move.Left), (p, Move.Right), (p, Move.Bottom)})
				.ToDictionary(pair => pair, pair => 0.0);
		}

		public void Run(int learnTimes = 2000)
		{
			for (int i = 0; i < learnTimes; i++)
			{
				Evaluate();
			}
		}

		private Move ChooseAction()
		{
			var possible = Point.Moves.Where(m => CanMoveTo(_currentState.MoveTo(m))).ToArray();
			return possible[_random.Next(possible.Length)];
		}

		private void Evaluate()
		{
			var action = ChooseAction();
			var newState = _currentState.MoveTo(action);
			var maxNewStateReward = MaxRewardFrom(newState);
			var currentValue = QTable[(_currentState, action)];
			QTable[(_currentState, action)] = currentValue +
			                                 _learningRate *
			                                 (Reward(newState) + _discountRate * maxNewStateReward - currentValue);
			_currentState = newState;
		}

		private double MaxRewardFrom(Point newState)
		{
			return Point.Moves.Select(newState.MoveTo).Where(CanMoveTo).Max(Reward);
		}
	}
}