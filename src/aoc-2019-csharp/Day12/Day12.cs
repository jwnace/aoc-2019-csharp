using aoc_2019_csharp.Extensions;

namespace aoc_2019_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt");

    public static int Part1() => Solve1(Input, 1000);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input, int steps)
    {
        var moons = GetMoons(input);

        for (var t = 0; t < steps; t++)
        {
            SimulateMovement(moons);
        }

        return moons.Sum(x => x.GetTotalEnergy());
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
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
}
