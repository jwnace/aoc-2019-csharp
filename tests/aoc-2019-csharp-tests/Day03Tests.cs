using aoc_2019_csharp.Day03;

namespace aoc_2019_csharp_tests;

public class Day03Tests
{
    [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 6)]
    [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)]
    [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
    public void Part1_Examples_ReturnCorrectAnswer(string line1, string line2, int expected)
    {
        var actual = Day03.GetShortestDistance(line1, line2);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 2050;
        var actual = Day03.Part1();
        actual.Should().Be(expected);
    }

    [TestCase("R8,U5,L5,D3", "U7,R6,D4,L4", 30)]
    [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)]
    [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
    public void Part2_Examples_ReturnCorrectAnswer(string line1, string line2, int expected)
    {
        var actual = Day03.GetFewestSteps(line1, line2);
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
