using aoc_2019_csharp.Day15;

namespace aoc_2019_csharp_tests;

public class Day15Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day15.Part1().Should().Be(244);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day15.Part2().Should().Be(278);
    }
}
