namespace aoc_2019_csharp.Day22;

public static class Day22
{
    private static readonly string[] Input = File.ReadAllLines("Day22/day22.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string[] input)
    {
        var deck = Enumerable.Range(0, 10007);

        foreach (var line in input)
        {
            if (line.StartsWith("deal with increment "))
            {
                var increment = int.Parse(line.Substring(20));
                deck = DealWithIncrement(deck, increment);
            }
            else if (line == "deal into new stack")
            {
                deck = DealIntoNewStack(deck);
            }
            else if (line.StartsWith("cut "))
            {
                var cut = int.Parse(line.Substring("cut ".Length));
                deck = Cut(deck, cut);
            }
            else
            {
                throw new Exception("Unknown line: " + line);
            }
        }

        var array = deck as int[] ?? deck.ToArray();
        return array.ToList().IndexOf(2019);
    }

    private static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<int> Cut(IEnumerable<int> deck, int cut)
    {
        var cards = deck as int[] ?? deck.ToArray();
        var result = new int[cards.Length];

        cut %= cards.Length;

        if (cut > 0)
        {
            Array.Copy(cards, cut, result, 0, cards.Length - cut);
            Array.Copy(cards, 0, result, cards.Length - cut, cut);
        }
        else
        {
            Array.Copy(cards, cards.Length + cut, result, 0, -cut);
            Array.Copy(cards, 0, result, -cut, cards.Length + cut);
        }

        return result;
    }

    private static IEnumerable<int> DealIntoNewStack(IEnumerable<int> deck)
    {
        return deck.Reverse();
    }

    private static IEnumerable<int> DealWithIncrement(IEnumerable<int> deck, int increment)
    {
        var cards = deck as int[] ?? deck.ToArray();
        var result = new int[cards.Length];
        var index = 0;

        foreach (var card in cards)
        {
            result[index] = card;
            index = (index + increment) % cards.Length;
        }

        return result;
    }
}
