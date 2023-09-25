namespace aoc_2019_csharp.Day03;

public static class Day03
{
    private static readonly string[] Input = File.ReadAllLines("Day03/day03.txt");

    public static int Part1() => GetShortestDistance(Input[0], Input[1]);

    public static int Part2() => GetFewestSteps(Input[0], Input[1]);

    public static int GetShortestDistance(string line1, string line2)
    {
        var wire1 = GetCoordinates(line1);
        var wire2 = GetCoordinates(line2);

        return wire1.Intersect(wire2).Min(GetManhattanDistanceFromOrigin);
    }

    public static int GetFewestSteps(string line1, string line2)
    {
        var wire1 = GetCoordinates(line1);
        var wire2 = GetCoordinates(line2);

        return wire1.Intersect(wire2).Min(intersection => GetStepsFromOrigin(intersection, wire1, wire2));
    }

    private static List<Coordinate> GetCoordinates(string input)
    {
        var coordinates = new List<Coordinate>();
        var instructions = input.Split(',');
        var temp = new Coordinate(0, 0);

        foreach (var instruction in instructions)
        {
            var direction = instruction[0];
            var distance = int.Parse(instruction[1..]);

            for (var i = 0; i < distance; i++)
            {
                temp = direction switch
                {
                    'U' => temp with { Y = temp.Y + 1 },
                    'D' => temp with { Y = temp.Y - 1 },
                    'L' => temp with { X = temp.X - 1 },
                    'R' => temp with { X = temp.X + 1 },
                    _ => throw new Exception($"Invalid direction `{direction}` in instruction `{instruction}`")
                };

                coordinates.Add(temp);
            }
        }

        return coordinates;
    }

    private static int GetManhattanDistanceFromOrigin(Coordinate coordinate) =>
        Math.Abs(coordinate.X) + Math.Abs(coordinate.Y);

    private static int GetStepsFromOrigin(Coordinate intersection, List<Coordinate> wire1, List<Coordinate> wire2)
    {
        var steps1 = wire1.IndexOf(intersection) + 1;
        var steps2 = wire2.IndexOf(intersection) + 1;

        return steps1 + steps2;
    }

    private record Coordinate(int X, int Y);
}
