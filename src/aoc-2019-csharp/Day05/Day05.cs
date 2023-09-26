using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day05;

public static class Day05
{
    private static readonly long[] Input = File.ReadAllText("Day05/day05.txt").Split(',').Select(long.Parse).ToArray();

    public static long Part1() => Run(1);

    public static long Part2() => Run(5);

    private static long Run(long input)
    {
        var inputs = new[] { input };
        var computer = new IntcodeComputer(Input, inputs);

        computer.Run();

        return computer.Output;
    }
}
