using System;

namespace Neopixels
{
	public struct Point
	{
		public double X;
		public double Y;

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.X + b.X, a.Y + b.Y);
		}

		public static Point operator -(Point a, Point b)
		{
			return new Point(a.X - b.X, a.Y - b.Y);
		}

		public static bool operator !=(Point a, Point b)
		{
			return !(a == b);
		}

		public static bool operator ==(Point a, Point b)
		{
			return (Math.Abs(a.X - b.X) <= double.Epsilon && Math.Abs(a.Y - b.Y) <= double.Epsilon);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is Point)
			{
				Point p = (Point)obj;
				return this == p;
			}
			return false;
		}

		public override int GetHashCode()
		{
			unchecked 
			{
				int hash = 17;
				hash = hash * 23 + X.GetHashCode();
				hash = hash * 23 + Y.GetHashCode();
				return hash;
			}
		}

		public double Magnitude { get { return Math.Sqrt(X * X + Y * Y); }  }

		public double Angle(Point p)
		{
			var x = X - p.X;
			var y = Y - p.Y;
			if (Math.Abs(x) <= double.Epsilon)
			{
				if (y < -double.Epsilon)
					return Math.PI;
				return 0;
			}
			var a = Math.Atan(x / y) + Math.PI / 2;
			if (x > 0)
				a += Math.PI;
			return a;
		}

		public double Distance(Point p)
		{
			var a = X - p.X;
			var b = Y - p.Y;
			return Math.Sqrt(a * a + b * b);
		}

		public Point Rotate(Point p, double angle)
		{
			if (Math.Abs(angle) < double.Epsilon)
				return p;
			var s = Math.Sin(angle);
			var c = Math.Cos(angle);
			var x = Y * s - X * c + X;
			var y = -X * s - Y * c + Y;
			return new Point(p.X * c - p.Y * s + x, p.X * s + p.Y * c + y);
		}

		public override string ToString()
		{
			return string.Format("[Point: X={0},Y={1},Magnitude={2}]", X, Y, Magnitude);
		}
	}
}
