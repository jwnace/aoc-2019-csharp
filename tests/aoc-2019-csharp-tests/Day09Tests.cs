using aoc_2019_csharp.Day09;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp_tests;

public class Day09Tests
{
    [TestCase(new[] { 109L, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, 99)]
    [TestCase(new[] { 1102L, 34915192, 34915192, 7, 4, 7, 99, 0 }, 1219070632396864)]
    [TestCase(new[] { 104L, 1125899906842624, 99 }, 1125899906842624)]
    public void Part1_Examples_ReturnCorrectAnswer(long[] memory, long expected)
    {
        var computer = new IntcodeComputer(memory, extraMemory: 100);

        computer.Run();

        computer.Output.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 2377080455;
        var actual = Day09.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 74917;
        var actual = Day09.Part2();
        actual.Should().Be(expected);
    }
}
