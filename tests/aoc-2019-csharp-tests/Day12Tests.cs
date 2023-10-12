using aoc_2019_csharp.Day12;

namespace aoc_2019_csharp_tests;

public class Day12Tests
{
    [TestCase(new[]
    {
        "<x=-1, y=0, z=2>",
        "<x=2, y=-10, z=-7>",
        "<x=4, y=-8, z=8>",
        "<x=3, y=5, z=-1>",
    }, 10, 179)]
    [TestCase(new[]
    {
        "<x=-8, y=-10, z=0>",
        "<x=5, y=5, z=10>",
        "<x=2, y=-7, z=3>",
        "<x=9, y=-8, z=-3>",
    }, 100, 1940)]
    public void Part1_Example_ReturnsCorrectAnswer(string[] input, int steps, int expected)
    {
        Day12.Solve1(input, steps).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day12.Part1().Should().Be(12053);
    }

    [TestCase(new[]
    {
        "<x=-1, y=0, z=2>",
        "<x=2, y=-10, z=-7>",
        "<x=4, y=-8, z=8>",
        "<x=3, y=5, z=-1>",
    }, 2772)]
    [TestCase(new[]
    {
        "<x=-8, y=-10, z=0>",
        "<x=5, y=5, z=10>",
        "<x=2, y=-7, z=3>",
        "<x=9, y=-8, z=-3>",
    }, 4686774924)]
    public void Part2_Example_ReturnsCorrectAnswer(string[] input, long expected)
    {
        Day12.Solve2(input).Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day12.Part2().Should().Be(320380285873116);
    }
}
