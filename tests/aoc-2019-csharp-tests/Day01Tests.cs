using aoc_2019_csharp.Day01;

namespace aoc_2019_csharp_tests;

public class Day01Tests
{
    [TestCase(12, 2)]
    [TestCase(14, 2)]
    [TestCase(1969, 654)]
    [TestCase(100756, 33583)]
    public void Part1_GetFuelRequirement_ReturnCorrectAnswer(int mass, int fuel)
    {
        var expected = fuel;
        var actual = Day01.GetFuelRequirement(mass);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        var expected = 3087896;
        var actual = Day01.Part1();
        actual.Should().Be(expected);
    }

    [TestCase(2, 0)]
    [TestCase(1, 0)]
    [TestCase(0, 0)]
    [TestCase(-1, 0)]
    [TestCase(-2, 0)]
    public void Part2_GetFuelRequirement_DoesNotReturnNegativeNumbers(int mass, int fuel)
    {
        var expected = fuel;
        var actual = Day01.GetFuelRequirement(mass);
        actual.Should().Be(expected);
    }

    [TestCase(12, 2)]
    [TestCase(14, 2)]
    [TestCase(1969, 966)]
    [TestCase(100756, 50346)]
    public void Part2_GetFuelRequirementRecursive_ReturnCorrectAnswer(int mass, int fuel)
    {
        var expected = fuel;
        var actual = Day01.GetFuelRequirementRecursive(mass);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected = 4628989;
        var actual = Day01.Part2();
        actual.Should().Be(expected);
    }
}
