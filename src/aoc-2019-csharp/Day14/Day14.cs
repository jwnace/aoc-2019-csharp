using aoc_2019_csharp.Extensions;

namespace aoc_2019_csharp.Day14;

public static class Day14
{
    private static readonly string[] Input = File.ReadAllLines("Day14/day14.txt");

    public static long Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static long Solve1(string[] input)
    {
        var reactions = GetReactions(input);
        var factory = new Factory(reactions);
        var ore = factory.GetOreRequirementToProduce(1, "FUEL");

        return ore;
    }

    public static int Solve2(string[] input)
    {
        const long ore = 1_000_000_000_000;
        var reactions = GetReactions(input);
        var start = 0;
        var end = int.MaxValue;

        while (end - start > 1)
        {
            var fuel = (start + end) / 2;
            var factory = new Factory(reactions);
            var oreRequirement = factory.GetOreRequirementToProduce(fuel, "FUEL");

            if (oreRequirement > ore)
            {
                end = fuel;
            }
            else
            {
                start = fuel;
            }
        }

        return start;
    }

    private static List<Reaction> GetReactions(string[] input)
    {
        var reactions = new List<Reaction>();

        foreach (var line in input)
        {
            var (left, right) = line.Split(" => ");
            var inputs = left.Split(", ").Select(x => x.Split(" ")).Select(x => new Chemical(int.Parse(x[0]), x[1]));
            var output = new Chemical(int.Parse(right.Split(" ")[0]), right.Split(" ")[1]);
            var reaction = new Reaction(inputs.ToArray(), output);
            reactions.Add(reaction);
        }

        return reactions;
    }
}
