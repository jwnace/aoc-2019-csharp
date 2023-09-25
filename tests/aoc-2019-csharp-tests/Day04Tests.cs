using aoc_2019_csharp.Day04;

namespace aoc_2019_csharp_tests;

public class Day04Tests
{
    [TestCase("111111", true)]
    [TestCase("223450", false)]
    [TestCase("123789", false)]
    public void IsValidPasswordForPart1_ReturnsCorrectAnswer(string password, bool expected)
    {
        var actual = Day04.IsValidPasswordForPart1(password);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 1665;
        var actual = Day04.Part1();
        actual.Should().Be(expected);
    }

    [TestCase("112233", true)]
    [TestCase("123444", false)]
    [TestCase("111122", true)]
    public void IsValidPasswordForPart2_ReturnsCorrectAnswer(string password, bool expected)
    {
        var actual = Day04.IsValidPasswordForPart2(password);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 1131;
        var actual = Day04.Part2();
        actual.Should().Be(expected);
    }
}
