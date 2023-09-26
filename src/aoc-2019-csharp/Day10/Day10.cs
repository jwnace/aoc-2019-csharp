namespace aoc_2019_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt");

    public static int Part1()
    {
        var max = 0;
        var maxRow = 0;
        var maxCol = 0;
        var maxAngles = new List<double>();

        for (int row = 0; row < Input.Length; row++)
        {
            for (int col = 0; col < Input[row].Length; col++)
            {
                if (Input[row][col] != '#')
                {
                    continue;
                }

                var angles = new List<double>();

                for (int r = 0; r < Input.Length; r++)
                {
                    for (int c = 0; c < Input[r].Length; c++)
                    {
                        if ((r == row && c == col) || Input[r][c] != '#')
                        {
                            continue;
                        }

                        var rise = row - r;
                        var run = c - col;
                        var angle = Math.Atan2(rise, run) * 180 / Math.PI;
                        angle = angle < 0 ? angle + 360 : angle;
                        angles.Add(angle);

                        var count = angles.Distinct().Count();

                        if (count > max)
                        {
                            max = count;
                            maxRow = row;
                            maxCol = col;
                            maxAngles = angles;
                        }
                    }
                }
            }
        }

        return max;
    }

    public static int Part2()
    {
        var max = 0;
        var maxRow = 0;
        var maxCol = 0;
        var maxAngles = new Dictionary<double, List<(int, int)>>();

        for (int row = 0; row < Input.Length; row++)
        {
            for (int col = 0; col < Input[row].Length; col++)
            {
                if (Input[row][col] != '#')
                {
                    continue;
                }

                var angles = new Dictionary<double, List<(int, int)>>();

                for (int r = 0; r < Input.Length; r++)
                {
                    for (int c = 0; c < Input[r].Length; c++)
                    {
                        if ((r == row && c == col) || Input[r][c] != '#')
                        {
                            continue;
                        }

                        var rise = row - r;
                        var run = c - col;
                        var angle = Math.Atan2(rise, run) * 180 /
                                    Math.PI; // this is the actual angle from asteroid A to asteroid B
                        angle -= 90;         // HACK: subtract 90 to rotate the entire field 90 degrees
                        angle = angle <= 0
                            ? angle + 360
                            : angle; // HACK: get rid of negative angles so we can loop from 360 to 0

                        if (angles.ContainsKey(angle))
                        {
                            angles[angle].Add((r, c));
                        }
                        else
                        {
                            angles.Add(angle, new List<(int, int)> { (r, c) });
                        }

                        var count = angles.Count();

                        if (count > max)
                        {
                            max = count;
                            maxRow = row;
                            maxCol = col;
                            maxAngles = angles;
                        }
                    }
                }
            }
        }

        var counter = 0;
        var query = maxAngles.OrderByDescending(a => a.Key).ToList();

        while (query.Any(a => a.Value.Count > 0))
        {
            foreach (var a in query)
            {
                if (a.Value.Count == 0)
                {
                    continue;
                }

                var temp = a.Value.OrderBy(x => Math.Abs(x.Item1 - maxRow) + Math.Abs(x.Item2 - maxCol)).First();
                a.Value.Remove(temp);
                counter++;

                // Console.WriteLine($"Vaporized asteroid #{counter}: ({temp.Item2}, {temp.Item1})");

                if (counter == 200)
                {
                    return temp.Item2 * 100 + temp.Item1;
                }
            }
        }

        return 0;
    }
}
