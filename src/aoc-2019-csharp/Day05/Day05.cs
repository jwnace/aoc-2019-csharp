using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day05;

public static class Day05
{
    private static readonly int[] Input = File.ReadAllText("Day05/day05.txt").Split(',').Select(int.Parse).ToArray();

    public static int Part1() => Run(1);

    public static int Part2() => Run(5);

    private static int Run(int input)
    {
        var inputs = new[] { input };
        var computer = new IntcodeComputer(Input.ToArray(), inputs);

        computer.Run();

        var outputs = computer.GetOutputs();
        return outputs.Last();
    }
}
