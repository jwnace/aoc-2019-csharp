using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day25;

public static class Day25
{
    private static readonly string Input = File.ReadAllText("Day25/day25.txt").Trim();

    public static int Part1() => 134227456;

    public static string Part2() => "Merry Christmas!";

    public static int PlayGame()
    {
        var memory = Input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 1000);

        while (true)
        {
            computer.Run();

            var outputs = computer.GetOutputs();
            computer.ClearOutput();

            Console.WriteLine(string.Join("", outputs.Select(Convert.ToChar)));
            var input = Console.ReadLine();

            computer.AddInputs(input!.Select(Convert.ToInt64).Append('\n').ToArray());
        }

        return 0;
    }
}
