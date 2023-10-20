﻿using aoc_2019_csharp.Day24;

namespace aoc_2019_csharp_tests;

public class Day24Tests
{
    [TestCase(new[]
    {
        "....#",
        "#..#.",
        "#..##",
        "..#..",
        "#....",
    }, 2129920)]
    public void Part1_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day24.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day24.Part1().Should().Be(32526865);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day24.Part2().Should().Be(0);
    }
}
