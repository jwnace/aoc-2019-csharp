using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day13;

public static class Day13
{
    private static readonly long[] Input =
        File.ReadAllText("Day13/day13.txt").Trim().Split(',').Select(long.Parse).ToArray();

    public static long Part1()
    {
        var computer = new IntcodeComputer(Input, 1000);
        computer.Run();
        var outputs = computer.GetOutputs();

        return outputs.Chunk(3).Count(x => x[2] == 2);
    }

    public static int Part2() => throw new NotImplementedException();
}
