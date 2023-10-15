using System.Text;

namespace aoc_2019_csharp.Day18;

public static class Day18
{
    private static readonly string[] Input = File.ReadAllLines("Day18/day18.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);

        var entrance = grid.First(x => x.Value == '@').Key;
        var keys = grid.Where(x => char.IsLower(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        var doors = grid.Where(x => char.IsUpper(x.Value)).ToDictionary(x => x.Key, x => x.Value);

        Console.WriteLine(DrawGrid(grid));

        return -1;
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

    private static string DrawGrid(Dictionary<(int Row, int Col),char> grid)
    {
        var sb = new StringBuilder();
        var rows = grid.Keys.Select(k => k.Row).Distinct().OrderBy(r => r).ToList();
        var cols = grid.Keys.Select(k => k.Col).Distinct().OrderBy(c => c).ToList();

        foreach (var row in rows)
        {
            foreach (var col in cols)
            {
                sb.Append(grid[(row, col)]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }
}
