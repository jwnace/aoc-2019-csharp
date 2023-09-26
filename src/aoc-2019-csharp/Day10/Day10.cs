namespace aoc_2019_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt");

    public static int Part1()
    {
        var max = 0;

        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[row].Length; col++)
            {
                if (Input[row][col] != '#')
                {
                    continue;
                }

                var angles = new List<double>();

                for (var r = 0; r < Input.Length; r++)
                {
                    for (var c = 0; c < Input[r].Length; c++)
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

        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[row].Length; col++)
            {
                if (Input[row][col] != '#')
                {
                    continue;
                }

                var angles = new Dictionary<double, List<(int, int)>>();

                for (var r = 0; r < Input.Length; r++)
                {
                    for (var c = 0; c < Input[r].Length; c++)
                    {
                        if ((r == row && c == col) || Input[r][c] != '#')
                        {
                            continue;
                        }

                        var rise = row - r;
                        var run = c - col;

                        // this is the actual angle from asteroid A to asteroid B
                        var angle = Math.Atan2(rise, run) * 180 / Math.PI;

                        // HACK: subtract 90 to rotate the entire field 90 degrees
                        angle -= 90;

                        // HACK: get rid of negative angles so we can loop from 360 to 0
                        angle = angle <= 0 ? angle + 360 : angle;

                        if (angles.ContainsKey(angle))
                        {
                            angles[angle].Add((r, c));
                        }
                        else
                        {
                            angles.Add(angle, new() { (r, c) });
                        }

                        var count = angles.Count;

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

                var temp = a.Value.MinBy(x => Math.Abs(x.Item1 - maxRow) + Math.Abs(x.Item2 - maxCol));
                a.Value.Remove(temp);
                counter++;

                if (counter == 200)
                {
                    return temp.Item2 * 100 + temp.Item1;
                }
            }
        }

        return 0;
    }
}
