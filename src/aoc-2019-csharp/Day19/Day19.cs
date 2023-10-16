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

        for (row = 0; row < 1000; row++)
        {
            var rowCount = 0;

            for (col = 0; col < 2000; col++)
            {
                // TODO: why do I need to create a new computer for each coordinate?
                var computer = new IntcodeComputer(memory, 100);
                computer.AddInputs(col, row);
                computer.Run();

                var output = computer.Output;

                // if (rowCount >= 100)
                // {
                //    Console.Write(output == 0 ? '.' : '#');
                // }

                if (output == 0)
                {
                    grid[(row, col)] = '.';
                }

                if (output == 1)
                {
                    grid[(row, col)] = '#';
                    rowCount++;
                }
            }

            // if (rowCount >= 100)
            // {
            //    Console.WriteLine();
            // }

            if (rowCount < 100)
            {
                continue;
            }

            // Console.WriteLine($"RowCount: {rowCount}");

            var row1 = row - 99;
            var row2 = row;

            var rowValues = grid.Where(x => x.Key.Row == row2).Select(x => x).ToArray();

            var col1 = rowValues.First(x => x.Value == '#').Key.Col;
            var col2 = col1 + 99;

            var topLeft = (row1, col1);
            var topRight = (row1, col2);
            var bottomLeft = (row2, col1);
            var bottomRight = (row2, col2);

            // Console.WriteLine($"Top Left: {topLeft}, Top Right: {topRight}, Bottom Left: {bottomLeft}, Bottom Right: {bottomRight}");

            var topLeftValue = grid[topLeft];
            var topRightValue = grid[topRight];
            var bottomLeftValue = grid[bottomLeft];
            var bottomRightValue = grid[bottomRight];

            if (topLeftValue == '#' &&
                topRightValue == '#' &&
                bottomLeftValue == '#' &&
                bottomRightValue == '#')
            {
                // Console.WriteLine($"Top Left: {topLeft}, Top Right: {topRight}, Bottom Left: {bottomLeft}, Bottom Right: {bottomRight}");

                for (var r = row1 - 10 ; r <= row2; r++)
                {
                    for (var c = col1 - 10; c <= col2; c++)
                    {
                        // Console.Write(grid[(r, c)]);
                    }

                    // Console.WriteLine();
                }

                return (col1 * 10_000) + row1;
            }
        }

        return -1;
    }
}
