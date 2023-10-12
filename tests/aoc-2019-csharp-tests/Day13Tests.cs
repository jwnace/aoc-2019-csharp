using aoc_2019_csharp.Day13;

namespace aoc_2019_csharp_tests;

public class Day13Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day13.Part1().Should().Be(265);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day13.Part2().Should().Be(0);
    }
}
