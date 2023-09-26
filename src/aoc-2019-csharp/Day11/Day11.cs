using System.Text;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day11;

public static class Day11
{
    private static readonly long[] Input = File.ReadAllText("Day11/day11.txt")
        .Trim().Split(',').Select(long.Parse).ToArray();

    public static int Part1()
    {
        var grid = new Dictionary<(int, int), Color>();
        var robot = new Robot();

        while (robot.Brain.HasHalted == false)
        {
            var found = grid.TryGetValue((robot.Position.X, robot.Position.Y), out var value);

            if (!found)
            {
                value = 0;
                grid[(robot.Position.X, robot.Position.Y)] = value;
            }

            robot.Brain.AddInput((long)value);
            robot.Brain.Run();

            var output = robot.Brain.GetOutputs();
            robot.Brain.ClearOutput();

            var color = (Color)output[0];
            var turn = output[1] == 0 ? 90 : -90;
            var direction = (int)robot.Direction + turn;
            direction = direction < 0 ? direction + 360 : direction >= 360 ? direction - 360 : direction;

            grid[(robot.Position.X, robot.Position.Y)] = color;
            robot.Direction = (Direction)direction;
            robot.MoveForward();
        }

        return grid.Count;
    }

    public static string Part2()
    {
        var grid = new Dictionary<(int, int), Color>();
        var robot = new Robot();
        var firstCell = true;

        while (robot.Brain.HasHalted == false)
        {
            var found = grid.TryGetValue((robot.Position.X, robot.Position.Y), out var value);

            if (!found)
            {
                if (firstCell)
                {
                    value = Color.White;
                    firstCell = false;
                }
                else
                {
                    value = 0;
                }

                grid[(robot.Position.X, robot.Position.Y)] = value;
            }

            robot.Brain.AddInput((long)value);
            robot.Brain.Run();

            var output = robot.Brain.GetOutputs();
            robot.Brain.ClearOutput();

            var color = (Color)output[0];
            var turn = output[1] == 0 ? 90 : -90;
            var direction = (int)robot.Direction + turn;
            direction = direction < 0 ? direction + 360 : direction >= 360 ? direction - 360 : direction;

            grid[(robot.Position.X, robot.Position.Y)] = color;
            robot.Direction = (Direction)direction;
            robot.MoveForward();
        }

        var builder = new StringBuilder();

        var minX = grid.Keys.Min(x => x.Item1);
        var maxX = grid.Keys.Max(x => x.Item1);
        var minY = grid.Keys.Min(x => x.Item2);
        var maxY = grid.Keys.Max(x => x.Item2);

        for(var row = maxY; row >= minY; row--)
        {
            builder.Append(Environment.NewLine);

            for(var col = minX; col <= maxX; col++)
            {
                var found = grid.TryGetValue((col, row), out var value);

                if (!found)
                {
                    value = 0;
                }

                builder.Append(value == Color.White ? '#' : ' ');
            }
        }

        return builder.ToString();
    }

    private class Robot
    {
        public Coordinate Position { get; set; } = new Coordinate { X = 0, Y = 0 };
        public Direction Direction { get; set; } = Direction.North;
        public IntcodeComputer Brain { get; set; } = new IntcodeComputer(Input, extraMemory: 10_000);

        internal void MoveForward()
        {
            switch (this.Direction)
            {
                case Direction.North:
                    this.Position.Y++;
                    break;
                case Direction.South:
                    this.Position.Y--;
                    break;
                case Direction.East:
                    this.Position.X++;
                    break;
                case Direction.West:
                    this.Position.X--;
                    break;
            }
        }
    }

    private class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    private enum Direction
    {
        North = 90,
        South = 270,
        East = 0,
        West = 180
    }

    private enum Color
    {
        Black = 0,
        White = 1
    }
}
