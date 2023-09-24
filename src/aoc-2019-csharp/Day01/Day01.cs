namespace aoc_2019_csharp.Day01;

public static class Day01
{
    private static readonly IEnumerable<int> Input = File.ReadAllLines("Day01/day01.txt").Select(int.Parse);

    public static int Part1() => Input.Sum(GetFuelRequirement);

    public static int Part2() => Input.Sum(GetFuelRequirementRecursive);

    public static int GetFuelRequirement(int mass) => Math.Max(mass / 3 - 2, 0);

    public static int GetFuelRequirementRecursive(int mass)
    {
        var fuel = GetFuelRequirement(mass);
        return fuel <= 0 ? 0 : fuel + GetFuelRequirementRecursive(fuel);
    }
}
