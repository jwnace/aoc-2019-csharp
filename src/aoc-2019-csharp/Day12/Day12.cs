using System.Diagnostics.CodeAnalysis;
using aoc_2019_csharp.Extensions;

namespace aoc_2019_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt");

    public static int Part1() => Solve1(Input, 1000);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input, int steps)
    {
        var moons = new List<Moon>();

        foreach (var line in input)
        {
            var parts = line.Split('=', ',', '>').Where(s => int.TryParse(s, out _)).Select(int.Parse).ToArray();
            var (x, y, z) = parts;
            var position = new Point(x, y, z);
            var velocity = new Point(0, 0, 0);
            var moon = new Moon(position, velocity);
            moons.Add(moon);
        }

        for (var t = 0; t < steps; t++)
        {
            foreach (var moon in moons)
            {
                foreach (var otherMoon in moons.Where(otherMoon => moon != otherMoon))
                {
                    if (moon.Position.X < otherMoon.Position.X)
                    {
                        moon.Velocity.X++;
                    }
                    else if (moon.Position.X > otherMoon.Position.X)
                    {
                        moon.Velocity.X--;
                    }

                    if (moon.Position.Y < otherMoon.Position.Y)
                    {
                        moon.Velocity.Y++;
                    }
                    else if (moon.Position.Y > otherMoon.Position.Y)
                    {
                        moon.Velocity.Y--;
                    }

                    if (moon.Position.Z < otherMoon.Position.Z)
                    {
                        moon.Velocity.Z++;
                    }
                    else if (moon.Position.Z > otherMoon.Position.Z)
                    {
                        moon.Velocity.Z--;
                    }
                }
            }

            foreach (var moon in moons)
            {
                moon.Position += moon.Velocity;
            }
        }

        return moons.Sum(x => x.GetTotalEnergy());
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private class Point
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

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }

    private class Moon
    {
        public Point Position { get; set; }
        public Point Velocity { get; set; }

        public Moon(Point position, Point velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public int GetTotalEnergy() => GetPotentialEnergy() * GetKineticEnergy();

        private int GetPotentialEnergy() => Position.GetMagnitude();

        private int GetKineticEnergy() => Velocity.GetMagnitude();

        public void Deconstruct(out Point position, out Point velocity)
        {
            position = Position;
            velocity = Velocity;
        }
    }
}
