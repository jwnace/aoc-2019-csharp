using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day23;

public static class Day23
{
    private static readonly string Input = File.ReadAllText("Day23/day23.txt").Trim();

    public static long Part1() => Solve(Input, 1);

    public static long Part2() => Solve(Input, 2);

    private static long Solve(string input, int part)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computers = new List<IntcodeComputer>();

        for (var i = 0; i < 50; i++)
        {
            var computer = new IntcodeComputer(memory, 100);
            computer.AddInput(i);
            computers.Add(computer);
        }

        var nat = (X: 0L, Y: 0L);
        var seen = new HashSet<long>();

        while (true)
        {
            if (computers.All(c => !c.HasInput))
            {
                computers[0].AddInput(nat.X);
                computers[0].AddInput(nat.Y);

                if (seen.Contains(nat.Y))
                {
                    return nat.Y;
                }

                seen.Add(nat.Y);
            }

            for (var i = 0; i < 50; i++)
            {
                var computer = computers[i];

                if (!computer.HasInput)
                {
                    computer.AddInput(-1);
                }

                computer.Run();

                var outputs = computer.GetOutputs();
                computer.ClearOutput();

                foreach (var chunk in outputs.Chunk(3))
                {
                    var (destination, x, y) = chunk;

                    if (destination == 255)
                    {
                        if (part == 1)
                        {
                            return y;
                        }

                        nat = (x, y);
                        continue;
                    }

                    computers[(int)destination].AddInput(x);
                    computers[(int)destination].AddInput(y);
                }
            }
        }
    }
}
