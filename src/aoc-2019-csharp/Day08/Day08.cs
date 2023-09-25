using System.Text;

namespace aoc_2019_csharp.Day08;

public static class Day08
{
    private static readonly string Input = File.ReadAllText("Day08/day08.txt").Trim();

    public static int Part1()
    {
        var layer = Input.Chunk(25 * 6).MinBy(layer => layer.Count(c => c == '0'))!;
        return layer.Count(c => c == '1') * layer.Count(c => c == '2');
    }

    public static string Part2()
    {
        var image = new Dictionary<(int, int), char>();
        var layers = Input.Chunk(25 * 6);

        foreach (var layer in layers)
        {
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 25; col++)
                {
                    if (!image.ContainsKey((row, col)) && layer[row * 25 + col] != '2')
                    {
                        image[(row, col)] = layer[row * 25 + col];
                    }
                }
            }
        }

        return DrawImage(image);
    }

    private static string DrawImage(Dictionary<(int, int), char> image)
    {
        var builder = new StringBuilder();

        for (var row = 0; row < 6; row++)
        {
            builder.Append(Environment.NewLine);

            for (var col = 0; col < 25; col++)
            {
                builder.Append(image[(row, col)] == '1' ? '#' : ' ');
            }
        }

        return builder.ToString();
    }
}
