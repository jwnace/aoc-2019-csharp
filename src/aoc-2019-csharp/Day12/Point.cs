namespace aoc_2019_csharp.Day12;

public class Point
{
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int GetMagnitude() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
}
