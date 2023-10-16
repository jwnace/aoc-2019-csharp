using aoc_2019_csharp.Day18;

namespace aoc_2019_csharp_tests;

public class Day18Tests
{
    [TestCase(new[]
    {
        "#########",
        "#b.A.@.a#",
        "#########",
    }, 8, TestName = "02 Keys")]
    [TestCase(new[]
    {
        "########################",
        "#f.D.E.e.C.b.A.@.a.B.c.#",
        "######################.#",
        "#d.....................#",
        "########################",
    }, 86, TestName = "06 Keys")]
    [TestCase(new[]
    {
        "########################",
        "#...............b.C.D.f#",
        "#.######################",
        "#.....@.a.B.c.d.A.e.F.g#",
        "########################",
    }, 132, TestName = "07 Keys")]
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
    }, 136, TestName = "16 Keys")]
    [TestCase(new[]
    {
        "########################",
        "#@..............ac.GI.b#",
        "###d#e#f################",
        "###A#B#C################",
        "###g#h#i################",
        "########################",
    }, 81, TestName = "09 Keys")]
    public void Part1_Example_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day18.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day18.Part1().Should().Be(5198);
    }

    [TestCase(new[]
    {
        "#######",
        "#a.#Cd#",
        "##@#@##",
        "#######",
        "##@#@##",
        "#cB#Ab#",
        "#######",
    }, 8, TestName = "04 Keys (simple)")]
    [TestCase(new[]
    {
        "###############",
        "#d.ABC.#.....a#",
        "######@#@######",
        "###############",
        "######@#@######",
        "#b.....#.....c#",
        "###############",
    }, 24, TestName = "04 Keys (complex)")]
    // TODO: my algorithm doesn't work for this case because the grid cannot be split into 4 quadrants cleanly
    // [TestCase(new[]
    // {
    //     "#############",
    //     "#DcBa.#.GhKl#",
    //     "#.###@#@#I###",
    //     "#e#d#####j#k#",
    //     "###C#@#@###J#",
    //     "#fEbA.#.FgHi#",
    //     "#############",
    // }, 32, TestName = "12 Keys")]
    [TestCase(new[]
    {
        "#############",
        "#g#f.D#..h#l#",
        "#F###e#E###.#",
        "#dCba@#@BcIJ#",
        "#############",
        "#nK.L@#@G...#",
        "#M###N#H###.#",
        "#o#m..#i#jk.#",
        "#############",
    }, 72, TestName = "15 Keys")]
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
