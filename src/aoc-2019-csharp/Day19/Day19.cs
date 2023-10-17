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
        var count = 0;

        for (var row = 0; row < 50; row++)
        {
            for (var col = 0; col < 50; col++)
            {
                var computer = new IntcodeComputer(memory, 100);
                computer.AddInputs(col, row);
                computer.Run();

                count += (int)computer.Output;
            }
        }

        return count;
    }

    private static int Solve2(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var tractorBeam = new HashSet<(int Row, int Col)>();

        for (var row = 400; row < 1000; row++)
        {
            for (var col = 400; col < 1500; col++)
            {
                var computer = new IntcodeComputer(memory, 100);
                computer.AddInputs(col, row);
                computer.Run();

                if (computer.Output == 1)
                {
                    tractorBeam.Add((row, col));
                }
            }

            var row1 = row - 99;
            var row2 = row;
            var col1 = tractorBeam.Where(x => x.Row == row2).Min(x => x.Col);
            var col2 = col1 + 99;

            var topLeft = (row1, col1);
            var topRight = (row1, col2);
            var bottomLeft = (row2, col1);
            var bottomRight = (row2, col2);

            if (tractorBeam.Contains(topLeft) &&
                tractorBeam.Contains(topRight) &&
                tractorBeam.Contains(bottomLeft) &&
                tractorBeam.Contains(bottomRight))
            {
                return col1 * 10_000 + row1;
            }
        }

        throw new Exception("No solution found");
    }
}
