// #define ANIMATE
#undef ANIMATE

#if ANIMATE
using System.Text;
#endif

using aoc_2019_csharp.Extensions;
using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day13;

public static class Day13
{
    private static readonly string Input = File.ReadAllText("Day13/day13.txt").Trim();

    public static long Part1()
    {
        var memory = Input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 100);

        computer.Run();

        return computer.GetOutputs().Chunk(3).Count(x => x[2] == 2);
    }

    public static long Part2()
    {
#if ANIMATE
        var (left, top) = Console.GetCursorPosition();
#endif

        var memory = Input.Split(',').Select(long.Parse).ToArray();
        memory[0] = 2;

        var computer = new IntcodeComputer(memory, 100);

        var paddle = 0L;
        var ball = 0L;
        var score = 0L;

        while (!computer.HasHalted)
        {
            computer.Run();

            var chunks = computer.GetOutputs().Chunk(3);

#if ANIMATE
            var grid = new Dictionary<(long X, long Y), char>();
#endif

            foreach (var chunk in chunks)
            {
                var (x, y, type) = chunk;

                if (type == 3)
                {
                    paddle = x;
                }

                if (type == 4)
                {
                    ball = x;
                }

                if (x == -1 && y == 0)
                {
                    score = type;
                }

#if ANIMATE
                var c = type switch
                {
                    0 => ' ',
                    1 => '#',
                    2 => 'X',
                    3 => '-',
                    4 => 'O',
                    _ => '?',
                };

                if (c != '?')
                {
                    grid[(x, y)] = c;
                }
#endif
            }

#if ANIMATE
            Console.SetCursorPosition(left, top);
            Console.WriteLine(DrawGrid(grid, score));
            Thread.Sleep(17);
#endif

            var joystick = ball > paddle ? 1 : ball < paddle ? -1 : 0;

            computer.AddInput(joystick);
        }

        return score;
    }

#if ANIMATE
    private static string DrawGrid(Dictionary<(long X, long Y), char> grid, long score)
    {
        var minX = grid.Keys.Min(x => x.X);
        var maxX = grid.Keys.Max(x => x.X);
        var minY = grid.Keys.Min(x => x.Y);
        var maxY = grid.Keys.Max(x => x.Y);

        var sb = new StringBuilder();

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                sb.Append(grid.ContainsKey((x, y)) ? grid[(x, y)] : ' ');
            }

            sb.AppendLine();
        }

        sb.AppendLine($"Score: {score}");

        return sb.ToString();
    }
#endif
}
