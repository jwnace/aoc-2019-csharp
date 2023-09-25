using aoc_2019_csharp.Extensions;

namespace aoc_2019_csharp.Day04;

public static class Day04
{
    private static readonly int[] Input = File.ReadAllText("Day04/day04.txt").Split('-').Select(int.Parse).ToArray();

    public static int Part1()
    {
        var (min, max) = (Input[0], Input[1]);
        var range = Enumerable.Range(min, max - min + 1).ToArray();

        return range.Select(x => x.ToString()).Count(IsValidPasswordForPart1);
    }

    public static int Part2()
    {
        var (min, max) = (Input[0], Input[1]);
        var range = Enumerable.Range(min, max - min + 1).ToArray();

        return range.Select(x => x.ToString()).Count(IsValidPasswordForPart2);
    }

    public static bool IsValidPasswordForPart1(string password) =>
        IsNumber(password) &&
        HasExactlySixDigits(password) &&
        HasPairOfMatchingDigits(password) &&
        DigitsNeverDecrease(password);

    public static bool IsValidPasswordForPart2(string password) =>
        IsValidPasswordForPart1(password) &&
        HasPairOfMatchingDigitsThatAreNotPartOfLargerGroup(password);

    private static bool IsNumber(string password) =>
        password.All(char.IsDigit);

    private static bool HasExactlySixDigits(string password) =>
        password.Length == 6;

    private static bool HasPairOfMatchingDigits(string password) =>
        password.Windowed(2).Any(x => x[0] == x[1]);

    private static bool DigitsNeverDecrease(string password) =>
        password.Windowed(2).All(x => x[0] <= x[1]);

    private static bool HasPairOfMatchingDigitsThatAreNotPartOfLargerGroup(string password) =>
        password.Any(digit => password.Count(d => d == digit) == 2);
}
