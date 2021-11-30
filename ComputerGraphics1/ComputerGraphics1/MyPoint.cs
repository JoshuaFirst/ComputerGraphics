using System;

struct MyPoint
{
    public MyPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    public readonly double X;
    public readonly double Y;

    public static bool operator ==(MyPoint one, MyPoint other) =>
        Math.Abs(one.X - other.X) < 0.0001 && Math.Abs(one.Y - other.Y) < 0.0001;

    public static bool operator !=(MyPoint one, MyPoint other) =>
        !(one == other);
}