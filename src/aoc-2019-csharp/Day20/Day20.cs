using aoc_2019_csharp.Extensions;

namespace aoc_2019_csharp.Day20;

public static class Day20
{
    private static readonly string[] Input = File.ReadAllLines("Day20/day20.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        var start = FindLabel("AA", grid);
        var end = FindLabel("ZZ", grid);

        var queue = new Queue<(int Row, int Col, int Steps)>();
        var seen = new HashSet<(int Row, int Col)>();
        queue.Enqueue((start.Row, start.Col, 0));

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, steps) = state;

            if (seen.Contains((row, col)))
            {
                continue;
            }

            seen.Add((row, col));

            if (row == end.Row && col == end.Col)
            {
                return steps;
            }

            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                var value = grid.TryGetValue(neighbor, out var v) ? v : ' ';

                if (value == '.')
                {
                    queue.Enqueue((neighbor.Row, neighbor.Col, steps + 1));
                    continue;
                }

                if (char.IsUpper(value))
                {
                    var label = GetLabel(neighbor, grid);

                    if (label is "AA" or "ZZ")
                    {
                        continue;
                    }

                    var matchingLabel = FindMatchingLabel(label, grid, (row, col));
                    queue.Enqueue((matchingLabel.Row, matchingLabel.Col, steps + 1));
                    continue;
                }
            }
        }

        throw new Exception("No path found");
    }

    private static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static (int Row, int Col) FindLabel(string label, Dictionary<(int Row, int Col), char> grid)
    {
        return grid.Where(x => x.Value == '.')
            .Where(x => IsNextToLabel(label, grid, x.Key))
            .Select(x => x.Key)
            .Single();
    }

    private static string GetLabel((int Row, int Col) position, Dictionary<(int Row, int Col), char> grid)
    {
        // NOTE: this implementation assumes that you will never have two labels adjacent to each other
        var (row, col) = position;

        var neighbors = new[]
        {
            (row - 1, col),
            (row + 1, col),
            (row, col - 1),
            (row, col + 1),
        };

        var letter1 = grid[(row, col)];
        var letter2 = neighbors
            .Select(n => grid.TryGetValue(n, out var v) ? v : ' ')
            .Single(char.IsUpper);

        var letters = new[] { letter1, letter2 };

        return string.Join("", letters.OrderBy(c => c));
    }

    private static (int Row, int Col) FindMatchingLabel(string label, Dictionary<(int Row, int Col), char> grid,
        (int Row, int Col) position)
    {
        return FindLabelPair(label, grid).Single(x => x != position);
    }

    private static (int Row, int Col)[] FindLabelPair(string label, Dictionary<(int Row, int Col), char> grid)
    {
        return grid.Where(x => x.Value == '.')
            .Where(x => IsNextToLabel(label, grid, x.Key))
            .Select(x => x.Key)
            .ToArray();
    }

    private static bool IsNextToLabel(
        string label,
        Dictionary<(int Row, int Col), char> grid,
        (int Row, int Col) position)
    {
        var (row, col) = position;

        var neighbors = new[]
        {
            (row - 1, col),
            (row + 1, col),
            (row, col - 1),
            (row, col + 1),
        };

        var values = label.ToArray();

        if (values.Length != 2)
        {
            throw new Exception("Label must be 2 characters long");
        }

        var (value1, value2) = values;

        var foo = neighbors.FirstOrDefault(n =>
            grid.TryGetValue(n, out var v) && v == value1 &&
            IsNextToValue(value2, grid, n));

        if (foo == default)
        {
            foo = neighbors.FirstOrDefault(n =>
                grid.TryGetValue(n, out var v) && v == value2 &&
                IsNextToValue(value1, grid, n));
        }

        if (foo == default)
        {
            return false;
        }

        return true;
    }

    private static bool IsNextToValue(char value, Dictionary<(int Row, int Col), char> grid, (int, int) position)
    {
        var (row, col) = position;

        var neighbors = new[]
        {
            (row - 1, col),
            (row + 1, col),
            (row, col - 1),
            (row, col + 1),
        };

        return neighbors.Any(n => grid.TryGetValue(n, out var v) && v == value);
    }
}
