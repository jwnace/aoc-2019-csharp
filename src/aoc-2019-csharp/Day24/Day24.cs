namespace aoc_2019_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input, 200);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var newGrid = new Dictionary<(int Row, int Col), bool>();
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

                    var adjacentBugs = neighbors.Count(n => grid.GetValueOrDefault(n));

                    newGrid[(row, col)] = adjacentBugs == 1 || !grid[(row, col)] && adjacentBugs == 2;
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
        var grid = BuildGrid(input);
        var newGrid = new Dictionary<(int Row, int Col), bool>();

        for (var i = 0; i < minutes; i++)
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

                    var adjacentBugs = neighbors.Count(n => grid.GetValueOrDefault(n));

                    newGrid[(row, col)] = adjacentBugs == 1 || !grid[(row, col)] && adjacentBugs == 2;
                }
            }

            (grid, newGrid) = (newGrid, grid);
        }

        return grid.Count(x => x.Value);
    }

    private static Dictionary<(int Row, int Col), bool> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int Row, int Col), bool>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col] == '#';
            }
        }

        return grid;
    }

    private static int ComputeBiodiversity(Dictionary<(int Row, int Col), bool> newGrid, string[] input) =>
        newGrid.Select(x => x.Value ? 1 << (x.Key.Row * input[x.Key.Row].Length + x.Key.Col) : 0).Sum();
}
