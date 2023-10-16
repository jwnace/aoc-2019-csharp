using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < 50; row++)
        {
            for (var col = 0; col < 50; col++)
            {
                // TODO: why do I need to create a new computer for each coordinate?
                var computer = new IntcodeComputer(memory, 100);
                computer.AddInputs(col, row);
                computer.Run();

                var output = computer.Output;

                grid[(row, col)] = output == 0 ? '.' : '#';
            }
        }

        return grid.Count(x => x.Value == '#');
    }

    private static int Solve2(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var grid = new Dictionary<(int Row, int Col), char>();

        var row = 0;
        var col = 0;

        for (row = 400; row < 1000; row++)
        {
            var rowCount = 0;

            for (col = 400; col < 1500; col++)
            {
                // TODO: why do I need to create a new computer for each coordinate?
                var computer = new IntcodeComputer(memory, 100);
                computer.AddInputs(col, row);
                computer.Run();

                var output = computer.Output;

                if (output == 1)
                {
                    grid[(row, col)] = '#';
                    rowCount++;
                }
            }

            if (rowCount < 100)
            {
                continue;
            }

            var row1 = row - 99;
            var row2 = row;
            var col1 = grid.Where(x => x.Key.Row == row2).Min(x => x.Key.Col);
            var col2 = col1 + 99;

            var topLeft = (row1, col1);
            var topRight = (row1, col2);
            var bottomLeft = (row2, col1);
            var bottomRight = (row2, col2);

            var topLeftValue = grid.TryGetValue(topLeft, out _);
            var topRightValue = grid.TryGetValue(topRight, out _);
            var bottomLeftValue = grid.TryGetValue(bottomLeft, out _);
            var bottomRightValue = grid.TryGetValue(bottomRight, out _);

            if (topLeftValue &&
                topRightValue &&
                bottomLeftValue &&
                bottomRightValue)
            {
                return col1 * 10_000 + row1;
            }
        }

        throw new Exception("No solution found");
    }
}
