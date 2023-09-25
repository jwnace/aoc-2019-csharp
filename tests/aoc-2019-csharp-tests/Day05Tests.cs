using aoc_2019_csharp.Day05;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp_tests;

public class Day05Tests
{
    [Test]
    public void Part1_Example_ReturnsCorrectAnswer()
    {
        var inputs = new[] { 69 };
        var sut = new IntcodeComputer(inputs);

        var buffer = new[] { 3, 0, 4, 0, 99 };

        sut.RunProgram(buffer);

        var outputs = sut.GetOutputs();

        outputs.Should().ContainSingle();
        outputs[0].Should().Be(inputs[0]);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 4887191;
        var actual = Day05.Part1();
        actual.Should().Be(expected);
    }

    [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 1, 0)]
    [TestCase(new[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 1)]
    [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 2, 1)]
    [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 8, 0)]
    [TestCase(new[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 9, 0)]
    [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 3, 0)]
    [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 8, 1)]
    [TestCase(new[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 9, 0)]
    [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 4, 1)]
    [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 8, 0)]
    [TestCase(new[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 9, 0)]
    [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 0, 0)]
    [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 1, 1)]
    [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 2, 1)]
    [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 3, 1)]
    [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 0, 0)]
    [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 1, 1)]
    [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 2, 1)]
    [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 3, 1)]
    [TestCase(new[]
    {
        3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4,
        20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
    }, 1, 999)]
    [TestCase(new[]
    {
        3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4,
        20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
    }, 8, 1000)]
    [TestCase(new[]
    {
        3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4,
        20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99
    }, 9, 1001)]
    public void Part2_Examples_ReturnCorrectAnswer(int[] buffer, int input, int expected)
    {
        var inputs = new[] { input };
        var sut = new IntcodeComputer(inputs);

        sut.RunProgram(buffer);

        var outputs = sut.GetOutputs();
        outputs.Last().Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 3419022;
        var actual = Day05.Part2();
        actual.Should().Be(expected);
    }
}
