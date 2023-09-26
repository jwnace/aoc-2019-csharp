using aoc_2019_csharp.Day10;

namespace aoc_2019_csharp_tests;

public class Day10Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 296;
        var actual = Day10.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 204;
        var actual = Day10.Part2();
        actual.Should().Be(expected);
    }
}
