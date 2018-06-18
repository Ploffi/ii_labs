using System.Collections.Generic;
using System.Linq;

namespace QLearning
{
	public class Point
	{
		public Point(int x, int y)
		{
			_x = x;
			_y = y;
		}

		private readonly int _x;
		private readonly int _y;

		public static Point Create(int x, int y)
		{
			return new Point(x, y);
		}

		public override int GetHashCode()
		{
			return _x + _y * (int) 10e6;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Point p))
				return false;
			return _x == p._x && _y == p._y;
		}

		public override string ToString()
		{
			return $"({_x};{_y})";
		}

		public Point MoveTo(Move move)
		{
			switch (@move)
			{
				case Move.Bottom:
					return Point.Create(_x, _y + 1);
				case Move.Left:
					return Point.Create(_x - 1, _y);
				case Move.Right:
					return Point.Create(_x + 1, _y);
				case Move.Top:
					return Point.Create(_x, _y - 1);
			}

			return null;
		}

		public static readonly Move[] Moves = new[] {Move.Bottom, Move.Right, Move.Top, Move.Left,};
		public IEnumerable<Point> Neighboors => Moves.Select(MoveTo);
	}

	public enum Move
	{
		Left,
		Right,
		Top,
		Bottom
	}
}