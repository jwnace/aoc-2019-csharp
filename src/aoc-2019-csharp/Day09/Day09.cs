using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day09;

public static class Day09
{
    private static readonly long[] Input = File.ReadAllText("Day09/day09.txt").Split(',').Select(long.Parse).ToArray();

    public static long Part1()
    {
        var computer = new IntcodeComputer(Input, new long[] { 1 }, 100);
        computer.Run();

        return computer.Output;
    }

    public static long Part2()
    {
        var computer = new IntcodeComputer(Input, new long[] { 2 }, 200);
        computer.Run();

        return computer.Output;
    }
}
