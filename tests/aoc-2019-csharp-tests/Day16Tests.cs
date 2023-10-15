using aoc_2019_csharp.Day16;

namespace aoc_2019_csharp_tests;

public class Day16Tests
{
    [TestCase("80871224585914546619083218645595", "24176176")]
    [TestCase("19617804207202209144916044189917", "73745418")]
    [TestCase("69317163492948606335995924319873", "52432133")]
    public void Part1_Example_ReturnsCorrectAnswer(string input, string expected)
    {
        Day16.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day16.Part1().Should().Be("42945143");
    }

    [TestCase("80871224585914546619083218645595", 0)]
    [TestCase("19617804207202209144916044189917", 0)]
    [TestCase("69317163492948606335995924319873", 0)]
    public void Part2_Example_ReturnsCorrectAnswer(string input, int expected)
    {
        Day16.Solve2(input).Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day16.Part2().Should().Be(0);
    }
}
