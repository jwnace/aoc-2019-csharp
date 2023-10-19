using System.Numerics;

namespace aoc_2019_csharp.Day22;

public static class Day22
{
    private static readonly string[] Input = File.ReadAllLines("Day22/day22.txt");

    public static int Part1() => Solve1(Input);

    public static BigInteger Part2() => Solve2(Input);

    private static int Solve1(IEnumerable<string> input)
    {
        var deck = Enumerable.Range(0, 10007).ToArray();

        foreach (var line in input)
        {
            if (line == "deal into new stack")
            {
                deck = DealIntoNewStack(deck);
            }
            else if (line.StartsWith("cut"))
            {
                var cut = int.Parse(line[4..]);
                deck = Cut(deck, cut);
            }
            else if (line.StartsWith("deal with increment"))
            {
                var increment = int.Parse(line[20..]);
                deck = DealWithIncrement(deck, increment);
            }
        }

        return Array.IndexOf(deck, 2019);
    }

    private static BigInteger Solve2(IEnumerable<string> input)
    {
        BigInteger numberOfCards = 119315717514047;
        BigInteger timesToShuffle = 101741582076661;
        BigInteger position = 2020;
        BigInteger a = 1;
        BigInteger b = 0;

        foreach (var line in input.Reverse())
        {
            if (line == "deal into new stack")
            {
                a = -a;
                b = -b - 1;
            }
            else if (line.StartsWith("cut"))
            {
                b += BigInteger.Parse(line[4..]);
            }
            else if (line.StartsWith("deal with increment"))
            {
                var increment = BigInteger.Parse(line[20..]);
                var pow = ModInv(increment, numberOfCards);

                a *= pow;
                b *= pow;
            }
        }

        return CalculateAnswerForPart2(numberOfCards, timesToShuffle, position, a, b);
    }

    private static int[] DealIntoNewStack(IEnumerable<int> deck)
    {
        return deck.Reverse().ToArray();
    }

    private static int[] Cut(int[] deck, int cut)
    {
        cut += cut < 0 ? deck.Length : 0;
        cut %= deck.Length;

        return deck[cut..].Concat(deck[..cut]).ToArray();
    }

    private static int[] DealWithIncrement(IReadOnlyList<int> deck, int increment)
    {
        var result = new int[deck.Count];

        for (var i = 0; i < deck.Count; i++)
        {
            var index = i * increment % deck.Count;
            result[index] = deck[i];
        }

        return result;
    }

    private static BigInteger ModInv(BigInteger a, BigInteger m)
    {
        return BigInteger.ModPow(a, m - 2, m);
    }

    private static BigInteger CalculateAnswerForPart2(BigInteger n, BigInteger t, BigInteger p, BigInteger a, BigInteger b)
    {
        return (p * BigInteger.ModPow(a, t, n) + (BigInteger.ModPow(a, t, n) - 1) * b * ModInv(a - 1, n)) % n;
    }
}
