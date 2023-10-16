using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

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

    private static long Solve2(string input)
    {
        throw new NotImplementedException();
    }
}
