﻿using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day02;

public static class Day02
{
    private static readonly long[] Input = File.ReadAllText("Day02/day02.txt").Split(',').Select(long.Parse).ToArray();

    public static long Part1() => new IntcodeComputer(Input).Run(12, 2);

    public static long Part2()
    {
        for (var i = 0; i < 100; i++)
        {
            for (var j = 0; j < 100; j++)
            {
                if (new IntcodeComputer(Input).Run(i, j) == 19690720)
                {
                    return 100 * i + j;
                }
            }
        }

        return 0;
    }
}
