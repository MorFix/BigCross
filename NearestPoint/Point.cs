using System;

namespace NearestPoint
{
    public class Point : IComparable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public int CompareTo(Point other)
        {
            return X.CompareTo(other.X);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
