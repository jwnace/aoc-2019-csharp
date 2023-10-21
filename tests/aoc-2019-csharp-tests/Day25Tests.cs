using aoc_2019_csharp.Day25;

namespace aoc_2019_csharp_tests;

public class Day25Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day25.Part1().Should().Be(134227456);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day25.Part2().Should().Be("Merry Christmas!");
    }
}
