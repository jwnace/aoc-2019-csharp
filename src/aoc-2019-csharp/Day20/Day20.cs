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
        var start = GetPositionOfLabel("AA", grid);
        var end = GetPositionOfLabel("ZZ", grid);
        var queue = new Queue<(int Row, int Col, int Steps)>();
        var seen = new HashSet<(int Row, int Col)>();
        queue.Enqueue((start.Row, start.Col, 0));

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, steps) = state;
            var position = (row, col);

            if (seen.Contains(position))
            {
                continue;
            }

            seen.Add(position);

            if (position == end)
            {
                return steps;
            }

            var neighbors = GetNeighbors(row, col);

            foreach (var neighbor in neighbors)
            {
                var value = grid.GetValueOrDefault(neighbor);

                if (value == '.')
                {
                    queue.Enqueue((neighbor.Row, neighbor.Col, steps + 1));
                    continue;
                }

                if (!char.IsUpper(value))
                {
                    continue;
                }

                var label = GetLabelAtPosition(neighbor, grid);

                if (label is "AA" or "ZZ")
                {
                    continue;
                }

                var matchingLabel = GetPositionOfMatchingLabel(label, position, grid);
                queue.Enqueue((matchingLabel.Row, matchingLabel.Col, steps + 1));
            }
        }

        throw new Exception("No path found");
    }

    private static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static (int Row, int Col)[] GetNeighbors(int row, int col) => new (int Row, int Col)[]
    {
        (row - 1, col),
        (row + 1, col),
        (row, col - 1),
        (row, col + 1),
    };

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

    private static (int Row, int Col) GetPositionOfLabel(string label, Dictionary<(int Row, int Col), char> grid) =>
        GetPositionsForLabel(label, grid).Single();

    private static (int Row, int Col)[] GetPositionsForLabel(string label, Dictionary<(int Row, int Col), char> grid) =>
        grid.Where(x => x.Value == '.')
            .Where(x => IsNextToLabel(label, grid, x.Key))
            .Select(x => x.Key)
            .ToArray();

    private static string GetLabelAtPosition((int Row, int Col) position, Dictionary<(int Row, int Col), char> grid)
    {
        var (row, col) = position;
        var neighbors = GetNeighbors(row, col);

        var letter1 = grid[position];
        var letter2 = neighbors.Select(grid.GetValueOrDefault).Single(char.IsUpper);

        var letters = new[] { letter1, letter2 };

        return string.Join("", letters.OrderBy(c => c));
    }

    private static (int Row, int Col) GetPositionOfMatchingLabel(
        string label,
        (int Row, int Col) position,
        Dictionary<(int Row, int Col), char> grid) => GetPositionsForLabel(label, grid).Single(x => x != position);

    private static bool IsNextToLabel(
        string label,
        Dictionary<(int Row, int Col), char> grid,
        (int Row, int Col) position)
    {
        var (row, col) = position;
        var neighbors = GetNeighbors(row, col);

        var values = label.ToArray();
        var (value1, value2) = values;

        return neighbors.Any(n => grid.GetValueOrDefault(n) == value1 && IsNextToValue(value2, n, grid)) ||
               neighbors.Any(n => grid.GetValueOrDefault(n) == value2 && IsNextToValue(value1, n, grid));
    }

    private static bool IsNextToValue(char value, (int, int) position, Dictionary<(int Row, int Col), char> grid)
    {
        var (row, col) = position;
        var neighbors = GetNeighbors(row, col);

        return neighbors.Any(n => grid.GetValueOrDefault(n) == value);
    }
}
