namespace aoc_2019_csharp.Day02;

public static class Day02
{
    private static readonly List<int> Input = File.ReadAllText("Day02/day02.txt").Split(',').Select(int.Parse).ToList();

    public static int Part1()
    {
        var buffer = Input.ToList();

        buffer[1] = 12;
        buffer[2] = 2;

        RunProgram(buffer);

        return buffer[0];
    }

    public static int Part2()
    {
        for (var i = 0; i < 100; i++)
        {
            for (var j = 0; j < 100; j++)
            {
                var buffer = Input.ToList();

                buffer[1] = i;
                buffer[2] = j;

                RunProgram(buffer);

                if (buffer[0] == 19690720)
                {
                    return 100 * i + j;
                }
            }
        }

        return 0;
    }

    private static void RunProgram(List<int> buffer)
    {
        for (var i = 0; i < buffer.Count; i++)
        {
            if (buffer[i] == 1)
            {
                var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);
                buffer[c] = buffer[a] + buffer[b];
                i += 3;
            }
            else if (buffer[i] == 2)
            {
                var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);
                buffer[c] = buffer[a] * buffer[b];
                i += 3;
            }
            else if (buffer[i] == 99)
            {
                break;
            }
            else
            {
                throw new Exception($"Invalid opcode {buffer[i]} at position {i}");
            }
        }
    }
}
