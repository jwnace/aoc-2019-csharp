using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day21;

public static class Day21
{
    private static readonly string Input = File.ReadAllText("Day21/day21.txt").Trim();

    public static long Part1()
    {
        var instructions = new[]
        {
            // if D is ground and there's a hole at B or C, we can jump to D
            "NOT B J\n",
            "NOT C T\n",
            "OR T J\n",
            "AND D J\n",

            // if next tile is a hole we have to jump
            "NOT A T\n",
            "OR T J\n",

            // walk
            "WALK\n",
        };

        return Solve(Input, instructions);
    }

    public static long Part2()
    {
        var instructions = new[]
        {
            // if D is ground and there's a hole at B or C, we can jump to D
            "NOT B J\n",
            "NOT C T\n",
            "OR T J\n",
            "AND D J\n",

            // but only if H is also ground
            "AND H J\n",

            // if next tile is a hole we have to jump
            "NOT A T\n",
            "OR T J\n",

            // run
            "RUN\n",
        };

        return Solve(Input, instructions);
    }

    private static long Solve(string input, string[] instructions)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 100);
        computer.Run();

        var program = instructions.SelectMany(c => c).Select(c => (long)c).ToArray();
        computer.AddInputs(program);
        computer.Run();

        return computer.Output;
    }
}
