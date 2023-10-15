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

    [TestCase("03036732577212944063491565474664", "84462026")]
    [TestCase("02935109699940807407585447034323", "78725270")]
    [TestCase("03081770884921959731165446850517", "53553731")]
    public void Part2_Example_ReturnsCorrectAnswer(string input, string expected)
    {
        Day16.Solve2(input).Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day16.Part2().Should().Be("99974970");
    }
}
