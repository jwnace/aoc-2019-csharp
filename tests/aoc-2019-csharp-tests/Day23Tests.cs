﻿using aoc_2019_csharp.Day23;

namespace aoc_2019_csharp_tests;

public class Day23Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day23.Part1().Should().Be(20225);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day23.Part2().Should().Be(0);
    }
}
