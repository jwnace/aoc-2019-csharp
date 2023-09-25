using aoc_2019_csharp.Day06;

namespace aoc_2019_csharp_tests;

public class Day06Tests
{
    [Test]
    public void Part1_Example_ReturnsCorrectAnswer()
    {
        var input = new[] { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L" };
        var expected = 42;

        var actual = Day06.CalculateChecksum(input);

        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 234446;
        var actual = Day06.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_Example_ReturnsCorrectAnswer()
    {
        var input = new[]
        {
            "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN",
        };

        var expected = 4;

        var actual = Day06.CalculateTransfers(input);

        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 385;
        var actual = Day06.Part2();
        actual.Should().Be(expected);
    }
}
