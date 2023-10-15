namespace aoc_2019_csharp.Day16;

public static class Day16
{
    private static readonly string Input = File.ReadAllText("Day16/day16.txt").Trim();

    public static string Part1() => Solve1(Input);

    public static string Part2() => Solve2(Input);

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

    public static string Solve2(string input)
    {
        var numbers = Expand(input.Select(c => int.Parse(c.ToString())).ToArray());
        var offset = int.Parse(input[..7]);

        for (var phase = 0; phase < 100; phase++)
        {
            for (var i = numbers.Length - 2; i >= offset; i--)
            {
                numbers[i] = (numbers[i] + numbers[i + 1]) % 10;
            }
        }

        return string.Join("", numbers.Skip(offset).Take(8));
    }

    private static int[] Expand(IReadOnlyList<int> numbers)
    {
        var expanded = new int[numbers.Count * 10_000];

        for (var i = 0; i < expanded.Length; i++)
        {
            expanded[i] = numbers[i % numbers.Count];
        }

        return expanded;
    }
}
