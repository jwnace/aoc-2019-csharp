using aoc_2019_csharp.Day08;

namespace aoc_2019_csharp_tests;

public class Day08Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 1677;
        var actual = Day08.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected =
            Environment.NewLine + "#  # ###  #  # #### ###  " +
            Environment.NewLine + "#  # #  # #  # #    #  # " +
            Environment.NewLine + "#  # ###  #  # ###  #  # " +
            Environment.NewLine + "#  # #  # #  # #    ###  " +
            Environment.NewLine + "#  # #  # #  # #    #    " +
            Environment.NewLine + " ##  ###   ##  #    #    ";

        var actual = Day08.Part2();
        actual.Should().Be(expected);
    }
}
