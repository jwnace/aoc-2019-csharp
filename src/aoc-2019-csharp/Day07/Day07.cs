using System.ComponentModel.DataAnnotations;
using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day07;

public static class Day07
{
    private static readonly int[] Input = File.ReadAllText("Day07/day07.txt").Split(',').Select(int.Parse).ToArray();

    public static int Part1() => GetMaxOutput(Input);

    public static int Part2() => FeedbackLoop(Input);

    public static int GetMaxOutput(int[] input)
    {
        var max = 0;
        var phaseSettings = new[] { 0, 1, 2, 3, 4 };

        var permutations = phaseSettings.GetPermutations(5).Select(x => x.ToArray());

        foreach (var phases in permutations)
        {
            var amplifierA = new IntcodeComputer(input, new[] { phases[0], 0 });
            amplifierA.Run();

            var amplifierB = new IntcodeComputer(input, new[] { phases[1], amplifierA.Output });
            amplifierB.Run();

            var amplifierC = new IntcodeComputer(input, new[] { phases[2], amplifierB.Output });
            amplifierC.Run();

            var amplifierD = new IntcodeComputer(input, new[] { phases[3], amplifierC.Output });
            amplifierD.Run();

            var amplifierE = new IntcodeComputer(input, new[] { phases[4], amplifierD.Output });
            amplifierE.Run();

            var output = amplifierE.Output;

            max = Math.Max(output, max);
        }

        return max;
    }

    public static int FeedbackLoop(int[] input)
    {
        var max = 0;
        var phaseSettings = new[] { 5, 6, 7, 8, 9 };

        var permutations = phaseSettings.GetPermutations(5).Select(x => x.ToArray());

        foreach (var phases in permutations)
        {
            var output = 0;

            var amplifierA = new IntcodeComputer(input, new[] { phases[0] });
            var amplifierB = new IntcodeComputer(input, new[] { phases[1] });
            var amplifierC = new IntcodeComputer(input, new[] { phases[2] });
            var amplifierD = new IntcodeComputer(input, new[] { phases[3] });
            var amplifierE = new IntcodeComputer(input, new[] { phases[4] });

            while (!amplifierE.HasHalted)
            {
                amplifierA.AddInput(output);
                amplifierA.Run();
                output = amplifierA.Output;

                amplifierB.AddInput(output);
                amplifierB.Run();
                output = amplifierB.Output;

                amplifierC.AddInput(output);
                amplifierC.Run();
                output = amplifierC.Output;

                amplifierD.AddInput(output);
                amplifierD.Run();
                output = amplifierD.Output;

                amplifierE.AddInput(output);
                amplifierE.Run();
                output = amplifierE.Output;
            }

            max = Math.Max(output, max);
        }

        return max;
    }
}
