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
                    var neighbors = GetNeighbors(row, col);
                    var adjacentBugs = neighbors.Count(n => grid.Contains(n));
                    var exists = grid.Contains((row, col));

                    if (adjacentBugs == 1 || !exists && adjacentBugs == 2)
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
        var grid = BuildRecursiveGrid(input);
        var newGrid = new HashSet<(int Row, int Col, int Level)>();

        for (var i = 0; i < minutes; i++)
        {
            var minRow = grid.Min(x => x.Row);
            var maxRow = grid.Max(x => x.Row);
            var minCol = grid.Min(x => x.Col);
            var maxCol = grid.Max(x => x.Col);
            var minLevel = grid.Min(x => x.Level);
            var maxLevel = grid.Max(x => x.Level);

            for (var level = minLevel - 1; level <= maxLevel + 1; level++)
            {
                for (var row = minRow; row <= maxRow; row++)
                {
                    for (var col = minCol; col <= maxCol; col++)
                    {
                        var neighbors = GetNeighbors(row, col, level);
                        var adjacentBugs = neighbors.Count(n => grid.Contains(n));
                        var exists = grid.Contains((row, col, level));

                        if (adjacentBugs == 1 || !exists && adjacentBugs == 2)
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

        return grid.Count;
    }

    private static HashSet<(int Row, int Col)> BuildGrid(IReadOnlyList<string> input)
    {
        var grid = new HashSet<(int Row, int Col)>();

        for (var row = 0; row < input.Count; row++)
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

    private static HashSet<(int Row, int Col, int Level)> BuildRecursiveGrid(IReadOnlyList<string> input)
    {
        var grid = new HashSet<(int Row, int Col, int Level)>();

        for (var row = 0; row < input.Count; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    grid.Add((row, col, 0));
                }
            }
        }

        return grid;
    }

    private static IEnumerable<(int Row, int Col)> GetNeighbors(int row, int col) => new[]
    {
        (row - 1, col),
        (row + 1, col),
        (row, col - 1),
        (row, col + 1),
    };

    private static IEnumerable<(int Row, int Col, int Level)> GetNeighbors(int row, int col, int level)
    {
        if (row == 2 && col == 2)
        {
            return Array.Empty<(int Row, int Col, int Level)>();
        }

        var sameLevel = new[]
        {
            (row - 1, col, level),
            (row + 1, col, level),
            (row, col - 1, level),
            (row, col + 1, level),
        };

        var outerLevel = Array.Empty<(int Row, int Col, int Level)>();
        var innerLevel = Array.Empty<(int Row, int Col, int Level)>();

        switch (row)
        {
            case 0 when col == 0:
            {
                outerLevel = new[]
                {
                    (1, 2, level - 1),
                    (2, 1, level - 1),
                };
                break;
            }
            case 0 when col == 1:
            case 0 when col == 2:
            case 0 when col == 3:
            {
                outerLevel = new[]
                {
                    (1, 2, level - 1),
                };
                break;
            }
            case 0 when col == 4:
            {
                outerLevel = new[]
                {
                    (1, 2, level - 1),
                    (2, 3, level - 1),
                };
                break;
            }
            case 1 when col == 0:
            case 2 when col == 0:
            case 3 when col == 0:
            {
                outerLevel = new[]
                {
                    (2, 1, level - 1),
                };
                break;
            }
            case 1 when col == 2:
            {
                innerLevel = new[]
                {
                    (0, 0, level + 1),
                    (0, 1, level + 1),
                    (0, 2, level + 1),
                    (0, 3, level + 1),
                    (0, 4, level + 1),
                };
                break;
            }
            case 1 when col == 4:
            case 2 when col == 4:
            case 3 when col == 4:
            {
                outerLevel = new[]
                {
                    (2, 3, level - 1),
                };
                break;
            }
            case 2 when col == 1:
            {
                innerLevel = new[]
                {
                    (0, 0, level + 1),
                    (1, 0, level + 1),
                    (2, 0, level + 1),
                    (3, 0, level + 1),
                    (4, 0, level + 1),
                };
                break;
            }
            case 2 when col == 3:
            {
                innerLevel = new[]
                {
                    (0, 4, level + 1),
                    (1, 4, level + 1),
                    (2, 4, level + 1),
                    (3, 4, level + 1),
                    (4, 4, level + 1),
                };
                break;
            }
            case 3 when col == 2:
            {
                innerLevel = new[]
                {
                    (4, 0, level + 1),
                    (4, 1, level + 1),
                    (4, 2, level + 1),
                    (4, 3, level + 1),
                    (4, 4, level + 1),
                };
                break;
            }
            case 4 when col == 0:
            {
                outerLevel = new[]
                {
                    (3, 2, level - 1),
                    (2, 1, level - 1),
                };
                break;
            }
            case 4 when col == 1:
            case 4 when col == 2:
            case 4 when col == 3:
            {
                outerLevel = new[]
                {
                    (3, 2, level - 1),
                };
                break;
            }
            case 4 when col == 4:
            {
                outerLevel = new[]
                {
                    (3, 2, level - 1),
                    (2, 3, level - 1),
                };
                break;
            }
        }

        return outerLevel.Concat(sameLevel).Concat(innerLevel);
    }

    private static int ComputeBiodiversity(IEnumerable<(int Row, int Col)> grid, IReadOnlyList<string> input) =>
        grid.Select(x => 1 << (x.Row * input[x.Row].Length + x.Col)).Sum();
}
