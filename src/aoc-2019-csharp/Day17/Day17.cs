﻿using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day17;

public static class Day17
{
    private static readonly string Input = File.ReadAllText("Day17/day17.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static long Part2() => Solve2(Input);

    private static int Solve1(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 10_000);

        computer.Run();

        var outputs = computer.GetOutputs();
        var grid = BuildGrid(outputs);
        var intersections = GetIntersections(grid);

        return intersections.Sum(x => x.Row * x.Col);
    }

    private static long Solve2(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        memory[0] = 2;

        var computer = new IntcodeComputer(memory, 10_000);
        var mainMovementRoutine = "A,B,A,C,B,C,B,A,C,B\n".ToArray();
        var movementFunctionA = "L,10,L,6,R,10\n".ToArray();
        var movementFunctionB = "R,6,R,8,R,8,L,6,R,8\n".ToArray();
        var movementFunctionC = "L,10,R,8,R,8,L,10\n".ToArray();
        var videoFeed = "n\n".ToArray();

        var inputs = mainMovementRoutine
            .Concat(movementFunctionA)
            .Concat(movementFunctionB)
            .Concat(movementFunctionC)
            .Concat(videoFeed)
            .Select(c => (long)c)
            .ToArray();

        computer.AddInputs(inputs);
        computer.Run();

        return computer.Output;
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(IEnumerable<long> outputs)
    {
        var mapString = string.Join("", outputs.Select(c => (char)c));
        var map = mapString.Split("\n").Select(s => s.ToCharArray()).ToArray();
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < map.Length; row++)
        {
            for (var col = 0; col < map[row].Length; col++)
            {
                grid[(row, col)] = map[row][col];
            }
        }

        return grid;
    }

    private static IEnumerable<(int Row, int Col)> GetIntersections(Dictionary<(int Row, int Col), char> grid)
    {
        var intersections = new List<(int Row, int Col)>();

        foreach (var (row, col) in grid.Keys)
        {
            if (grid[(row, col)] != '#')
            {
                continue;
            }

            var neighbors = new[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            if (neighbors.All(n => grid.ContainsKey(n) && grid[n] == '#'))
            {
                intersections.Add((row, col));
            }
        }

        return intersections;
    }
}
