namespace aoc_2019_csharp.Day06;

public static class Day06
{
    private static readonly string[] Input = File.ReadAllLines("Day06/day06.txt");

    public static int Part1() => CalculateChecksum(Input);

    public static int Part2() => CalculateTransfers(Input);

    public static int CalculateChecksum(string[] input)
    {
        var checksum = 0;
        var orbits = input.Select(x => x.Split(")")).ToDictionary(x => x[1], x => x[0]);

        foreach (var orbit in orbits)
        {
            var current = orbit.Key;

            while (orbits.ContainsKey(current))
            {
                checksum++;
                current = orbits[current];
            }
        }

        return checksum;
    }

    public static int CalculateTransfers(string[] input)
    {
        var directOrbits = input.Select(x => x.Split(")")).ToDictionary(x => x[1], x => x[0]);
        var yourOrbits = GetOrbits(directOrbits, "YOU");
        var santasOrbits = GetOrbits(directOrbits, "SAN");

        var closestCommonOrbit = yourOrbits.Intersect(santasOrbits).First();

        return yourOrbits.IndexOf(closestCommonOrbit) + santasOrbits.IndexOf(closestCommonOrbit) - 2;
    }

    private static List<string> GetOrbits(Dictionary<string, string> orbits, string start)
    {
        var you = new List<string>();
        var current = start;

        while (orbits.ContainsKey(current))
        {
            you.Add(current);
            current = orbits[current];
        }

        return you;
    }
}
