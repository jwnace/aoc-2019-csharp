using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day23;

public static class Day23
{
    private static readonly string Input = File.ReadAllText("Day23/day23.txt").Trim();

    public static long Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static long Solve1(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computers = new List<IntcodeComputer>();

        for (var i = 0; i < 50; i++)
        {
            var computer = new IntcodeComputer(memory, 100);
            computer.AddInput(i);
            computers.Add(computer);
        }

        while (true)
        {
            for (var i = 0; i < 50; i++)
            {
                var computer = computers[i];

                if (!computer.HasInput)
                {
                    computer.AddInput(-1);
                }

                computer.Run();

                var outputs = computer.GetOutputs();

                foreach (var chunk in outputs.Chunk(3))
                {
                    var (destination, x, y) = chunk;

                    if (destination == 255)
                    {
                        return y;
                    }

                    computers[(int)destination].AddInput(x);
                    computers[(int)destination].AddInput(y);
                }
            }
        }
    }

    private static int Solve2(string input)
    {
        return -2;
    }
}
