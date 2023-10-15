namespace aoc_2019_csharp.Day16;

public static class Day16
{
    private static readonly string Input = File.ReadAllText("Day16/day16.txt").Trim();

    public static string Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static string Solve1(string input)
    {
        var numbers = input.Select(c => int.Parse(c.ToString())).ToArray();
        var basePattern = new[] { 0, 1, 0, -1 };

        for (var phase = 0; phase < 100; phase++)
        {
            for (var i = 0; i < numbers.Length; i++)
            {
                var pattern = basePattern.SelectMany(x => Enumerable.Repeat(x, i + 1)).ToArray();
                var sum = numbers.Select((number, index) => number * pattern[(index + 1) % pattern.Length]).Sum();

                numbers[i] = Math.Abs(sum) % 10;
            }
        }

        return string.Join("", numbers.Take(8));
    }

    public static int Solve2(string input) => throw new NotImplementedException();
}
