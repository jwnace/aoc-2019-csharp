namespace aoc_2019_csharp.Day22;

public static class Day22
{
    private static readonly string[] Input = File.ReadAllLines("Day22/day22.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string[] input)
    {
        var deck = Enumerable.Range(0, 10007).ToArray();

        foreach (var line in input)
        {
            if (line.StartsWith("deal with increment"))
            {
                var increment = int.Parse(line[20..]);
                deck = DealWithIncrement(deck, increment);
            }
            else if (line == "deal into new stack")
            {
                deck = DealIntoNewStack(deck);
            }
            else if (line.StartsWith("cut"))
            {
                var cut = int.Parse(line[4..]);
                deck = Cut(deck, cut);
            }
            else
            {
                throw new Exception("Unknown line: " + line);
            }
        }

        return Array.IndexOf(deck, 2019);
    }

    private static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static int[] DealIntoNewStack(int[] deck)
    {
        return deck.Reverse().ToArray();
    }

    private static int[] Cut(int[] deck, int cut)
    {
        cut += cut < 0 ? deck.Length : 0;
        cut %= deck.Length;

        return deck[cut..].Concat(deck[..cut]).ToArray();
    }

    private static int[] DealWithIncrement(int[] deck, int increment)
    {
        var result = new int[deck.Length];

        for (var i = 0; i < deck.Length; i++)
        {
            var index = i * increment % deck.Length;
            result[index] = deck[i];
        }

        return result;
    }
}
