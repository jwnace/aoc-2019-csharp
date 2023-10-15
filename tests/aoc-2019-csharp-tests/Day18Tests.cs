using aoc_2019_csharp.Day18;

namespace aoc_2019_csharp_tests;

public class Day18Tests
{
    [TestCase(new[]
    {
        "#########",
        "#b.A.@.a#",
        "#########",
    }, 8)]
    [TestCase(new[]
    {
        "########################",
        "#f.D.E.e.C.b.A.@.a.B.c.#",
        "######################.#",
        "#d.....................#",
        "########################",
    }, 86)]
    [TestCase(new[]
    {
        "########################",
        "#...............b.C.D.f#",
        "#.######################",
        "#.....@.a.B.c.d.A.e.F.g#",
        "########################",
    }, 132)]
    [TestCase(new[]
    {
        "#################",
        "#i.G..c...e..H.p#",
        "########.########",
        "#j.A..b...f..D.o#",
        "########@########",
        "#k.E..a...g..B.n#",
        "########.########",
        "#l.F..d...h..C.m#",
        "#################",
    }, 136)]
    [TestCase(new[]
    {
        "########################",
        "#@..............ac.GI.b#",
        "###d#e#f################",
        "###A#B#C################",
        "###g#h#i################",
        "########################",
    }, 81)]
    public void Part1_Example_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day18.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day18.Part1().Should().Be(0);
    }

    [TestCase(new[] { "" }, 0)]
    public void Part2_Example_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day18.Solve2(input).Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day18.Part2().Should().Be(0);
    }
}
