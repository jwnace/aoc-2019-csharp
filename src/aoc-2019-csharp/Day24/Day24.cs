namespace aoc_2019_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input, 200);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var newGrid = new HashSet<(int Row, int Col)>();
        var seen = new HashSet<int>();

        while (true)
        {
            for (var row = 0; row < input.Length; row++)
            {
                for (var col = 0; col < input[row].Length; col++)
                {
                    var neighbors = new[]
                    {
                        (row - 1, col),
                        (row + 1, col),
                        (row, col - 1),
                        (row, col + 1),
                    };

                    var adjacentBugs = neighbors.Count(n => grid.Contains(n));
                    var value = grid.Contains((row, col));

                    if (adjacentBugs == 1 || !value && adjacentBugs == 2)
                    {
                        newGrid.Add((row, col));
                    }
                    else
                    {
                        newGrid.Remove((row, col));
                    }
                }
            }

            var biodiversity = ComputeBiodiversity(newGrid, input);

            if (!seen.Add(biodiversity))
            {
                return biodiversity;
            }

            (grid, newGrid) = (newGrid, grid);
        }
    }

    public static int Solve2(string[] input, int minutes)
    {
        var grid = new HashSet<(int Row, int Col, int Level)>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    grid.Add((row, col, 0));
                }
            }
        }

        var newGrid = new HashSet<(int Row, int Col, int Level)>();

        for (var i = 0; i < minutes; i++)
        {
            var minRow = grid.Min(x => x.Row);
            var maxRow = grid.Max(x => x.Row);
            var minCol = grid.Min(x => x.Col);
            var maxCol = grid.Max(x => x.Col);
            var minLevel = grid.Min(x => x.Level);
            var maxLevel = grid.Max(x => x.Level);

            for (var row = minRow; row <= maxRow; row++)
            {
                for (var col = minCol; col <= maxCol; col++)
                {
                    for (var level = minLevel - 1; level <= maxLevel + 1; level++)
                    {
                        var neighbors = GetNeighbors(row, col, level);
                        var adjacentBugs = neighbors.Count(n => grid.Contains(n));
                        var value = grid.Contains((row, col, level));

                        if (adjacentBugs == 1 || !value && adjacentBugs == 2)
                        {
                            newGrid.Add((row, col, level));
                        }
                        else
                        {
                            newGrid.Remove((row, col, level));
                        }
                    }
                }
            }

            (grid, newGrid) = (newGrid, grid);
        }

        DrawGrid(grid);
        return grid.Count;
    }

    private static void DrawGrid(HashSet<(int Row, int Col, int Level)> grid)
    {
        var minRow = grid.Min(x => x.Row);
        var maxRow = grid.Max(x => x.Row);
        var minCol = grid.Min(x => x.Col);
        var maxCol = grid.Max(x => x.Col);
        var minLevel = grid.Min(x => x.Level);
        var maxLevel = grid.Max(x => x.Level);

        for (var level = minLevel; level <= maxLevel; level++)
        {
            Console.WriteLine($"Depth {level}:");

            for (var row = minRow; row <= maxRow; row++)
            {
                for (var col = minCol; col <= maxCol; col++)
                {
                    if ((row, col) == (2, 2))
                    {
                        Console.Write('?');
                        continue;
                    }

                    Console.Write(grid.Contains((row, col, level)) ? '#' : '.');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }

    private static (int Row, int Col, int Level)[] GetNeighbors(int row, int col, int level)
    {
        var sameLevel = new[]
        {
            (row - 1, col, level),
            (row + 1, col, level),
            (row, col - 1, level),
            (row, col + 1, level),
        };

        if (row == 0 && col == 0)
        {
            var outerLevel = new[]
            {
                (1, 2, level - 1),
                (2, 1, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 0 && col == 1)
        {
            var outerLevel = new[]
            {
                (1, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 0 && col == 2)
        {
            var outerLevel = new[]
            {
                (1, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 0 && col == 3)
        {
            var outerLevel = new[]
            {
                (1, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 0 && col == 4)
        {
            var outerLevel = new[]
            {
                (1, 2, level - 1),
                (2, 3, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 1 && col == 0)
        {
            var outerLevel = new[]
            {
                (2, 1, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 1 && col == 1)
        {
            return sameLevel.ToArray();
        }

        if (row == 1 && col == 2)
        {
            var innerLevel = new[]
            {
                (0, 0, level + 1),
                (0, 1, level + 1),
                (0, 2, level + 1),
                (0, 3, level + 1),
                (0, 4, level + 1),
            };

            return sameLevel.Concat(innerLevel).ToArray();
        }

        if (row == 1 && col == 3)
        {
            return sameLevel.ToArray();
        }

        if (row == 1 && col == 4)
        {
            var outerLevel = new[]
            {
                (2, 3, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 2 && col == 0)
        {
            var outerLevel = new[]
            {
                (2, 1, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 2 && col == 1)
        {
            var innerLevel = new[]
            {
                (0, 0, level + 1),
                (1, 0, level + 1),
                (2, 0, level + 1),
                (3, 0, level + 1),
                (4, 0, level + 1),
            };

            return sameLevel.Concat(innerLevel).ToArray();
        }

        if (row == 2 && col == 2)
        {
            // TODO: does this work?
            return Array.Empty<(int Row, int Col, int Level)>();
        }

        if (row == 2 && col == 3)
        {
            var innerLevel = new[]
            {
                (0, 4, level + 1),
                (1, 4, level + 1),
                (2, 4, level + 1),
                (3, 4, level + 1),
                (4, 4, level + 1),
            };

            return sameLevel.Concat(innerLevel).ToArray();
        }

        if (row == 2 && col == 4)
        {
            var outerLevel = new[]
            {
                (2, 3, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 3 && col == 0)
        {
            var outerLevel = new[]
            {
                (2, 1, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 3 && col == 1)
        {
            return sameLevel.ToArray();
        }

        if (row == 3 && col == 2)
        {
            var innerLevel = new[]
            {
                (4, 0, level + 1),
                (4, 1, level + 1),
                (4, 2, level + 1),
                (4, 3, level + 1),
                (4, 4, level + 1),
            };

            return sameLevel.Concat(innerLevel).ToArray();
        }

        if (row == 3 && col == 3)
        {
            return sameLevel.ToArray();
        }

        if (row == 3 && col == 4)
        {
            var outerLevel = new[]
            {
                (2, 3, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 4 && col == 0)
        {
            var outerLevel = new[]
            {
                (3, 2, level - 1),
                (2, 1, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 4 && col == 1)
        {
            var outerLevel = new[]
            {
                (3, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 4 && col == 2)
        {
            var outerLevel = new[]
            {
                (3, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 4 && col == 3)
        {
            var outerLevel = new[]
            {
                (3, 2, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        if (row == 4 && col == 4)
        {
            var outerLevel = new[]
            {
                (3, 2, level - 1),
                (2, 3, level - 1),
            };

            return sameLevel.Concat(outerLevel).ToArray();
        }

        throw new Exception("Couldn't figure out neighbors!");
    }

    private static HashSet<(int Row, int Col)> BuildGrid(string[] input)
    {
        var grid = new HashSet<(int Row, int Col)>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    grid.Add((row, col));
                }
            }
        }

        return grid;
    }

    private static int ComputeBiodiversity(HashSet<(int Row, int Col)> newGrid, string[] input) =>
        newGrid.Select(x => 1 << (x.Row * input[x.Row].Length + x.Col)).Sum();
}
