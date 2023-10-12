using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt");

    public static int Part1() => Solve1(Input, 1000);

    public static long Part2() => Solve2(Input);

    public static int Solve1(string[] input, int steps)
    {
        var moons = GetMoons(input);

        for (var t = 0; t < steps; t++)
        {
            SimulateMovement(moons);
        }

        return moons.Sum(x => x.GetTotalEnergy());
    }

    public static long Solve2(string[] input)
    {
        var moons = GetMoons(input);

        var xHashes = new HashSet<(int, int, int, int, int, int, int, int)> { HashX(moons) };
        var yHashes = new HashSet<(int, int, int, int, int, int, int, int)> { HashY(moons) };
        var zHashes = new HashSet<(int, int, int, int, int, int, int, int)> { HashZ(moons) };

        var xFound = false;
        var yFound = false;
        var zFound = false;

        var xCycle = 0L;
        var yCycle = 0L;
        var zCycle = 0L;

        var t = 0L;

        while (!xFound || !yFound || !zFound)
        {
            SimulateMovement(moons);
            t++;

            if (!xFound)
            {
                var xHash = HashX(moons);

                if (xHashes.Contains(xHash))
                {
                    xFound = true;
                    xCycle = t;
                }
                else
                {
                    xHashes.Add(xHash);
                }
            }

            if (!yFound)
            {
                var yHash = HashY(moons);

                if (yHashes.Contains(yHash))
                {
                    yFound = true;
                    yCycle = t;
                }
                else
                {
                    yHashes.Add(yHash);
                }
            }

            if (!zFound)
            {
                var zHash = HashZ(moons);

                if (zHashes.Contains(zHash))
                {
                    zFound = true;
                    zCycle = t;
                }
                else
                {
                    zHashes.Add(zHash);
                }
            }
        }

        return MathHelper.LeastCommonMultiple(xCycle, yCycle, zCycle);
    }

    private static List<Moon> GetMoons(string[] input)
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

        return moons;
    }

    private static void SimulateMovement(List<Moon> moons)
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

    private static (int, int, int, int, int, int, int, int) HashX(List<Moon> moons)
    {
        return (
            moons[0].Position.X, moons[1].Position.X, moons[2].Position.X, moons[3].Position.X,
            moons[0].Velocity.X, moons[1].Velocity.X, moons[2].Velocity.X, moons[3].Velocity.X);
    }

    private static (int, int, int, int, int, int, int, int) HashY(List<Moon> moons)
    {
        return (
            moons[0].Position.Y, moons[1].Position.Y, moons[2].Position.Y, moons[3].Position.Y,
            moons[0].Velocity.Y, moons[1].Velocity.Y, moons[2].Velocity.Y, moons[3].Velocity.Y);
    }

    private static (int, int, int, int, int, int, int, int) HashZ(List<Moon> moons)
    {
        return (
            moons[0].Position.Z, moons[1].Position.Z, moons[2].Position.Z, moons[3].Position.Z,
            moons[0].Velocity.Z, moons[1].Velocity.Z, moons[2].Velocity.Z, moons[3].Velocity.Z);
    }
}
