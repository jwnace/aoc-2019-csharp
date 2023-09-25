using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day05;

public static class Day05
{
    private static readonly int[] Input = File.ReadAllText("Day05/day05.txt").Split(',').Select(int.Parse).ToArray();

    public static int Part1()
    {
        var inputs = new[] { 1 };
        var computer = new IntcodeComputer(inputs);

        computer.RunProgram(Input.ToArray());

        var outputs = computer.GetOutputs();
        return outputs.Last();
    }

    public static int Part2()
    {
        var inputs = new[] { 5 };
        var computer = new IntcodeComputer(inputs);

        computer.RunProgram(Input.ToArray());

        var outputs = computer.GetOutputs();
        return outputs.Last();
    }
}
