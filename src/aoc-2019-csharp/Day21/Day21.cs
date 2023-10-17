using System.Runtime.InteropServices;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day21;

public static class Day21
{
    private static readonly string Input = File.ReadAllText("Day21/day21.txt").Trim();

    public static long Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static long Solve1(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 100);
        computer.Run();

        // var outputs = computer.GetOutputs();
        // computer.ClearOutput();

        // Console.WriteLine(string.Join("",outputs.Select(Convert.ToChar)));

        var instructions = new[]
        {
            "NOT A J\n",
            "NOT B T\n",
            "OR T J\n",
            "NOT C T\n",
            "OR T J\n",
            "AND D J\n",
            "WALK\n"
        };

        var program = instructions.SelectMany(c => c).Select(c => (long)c).ToArray();
        computer.AddInputs(program);
        computer.Run();

        // outputs = computer.GetOutputs();

        // Console.WriteLine(string.Join("",outputs[..^1].Select(Convert.ToChar)));

        // var damage = outputs.Last();

        return computer.Output;
    }

    private static int Solve2(string input)
    {
        throw new NotImplementedException();
    }
}
