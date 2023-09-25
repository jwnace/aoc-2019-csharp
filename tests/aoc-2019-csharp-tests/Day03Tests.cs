using aoc_2019_csharp.Day03;

namespace aoc_2019_csharp_tests;

public class Day03Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 2050;
        var actual = Day03.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 21666;
        var actual = Day03.Part2();
        actual.Should().Be(expected);
    }
}
