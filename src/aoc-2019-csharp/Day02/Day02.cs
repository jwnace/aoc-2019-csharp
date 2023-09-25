namespace aoc_2019_csharp.Day02;

public static class Day02
{
    private static readonly int[] Input = File.ReadAllText("Day02/day02.txt").Split(',').Select(int.Parse).ToArray();

    public static int Part1() => RunProgram(Input.ToArray(), 12, 2);

    public static int Part2()
    {
        for (var i = 0; i < 100; i++)
        {
            for (var j = 0; j < 100; j++)
            {
                if (RunProgram(Input.ToArray(), i, j) == 19690720)
                {
                    return 100 * i + j;
                }
            }
        }

        return 0;
    }

    private static int RunProgram(int[] buffer, int noun, int verb)
    {
        buffer[1] = noun;
        buffer[2] = verb;

        for (var i = 0; i < buffer.Length; i += 4)
        {
            if (buffer[i] == 99)
            {
                break;
            }

            var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

            buffer[c] = buffer[i] switch
            {
                1 => buffer[a] + buffer[b],
                2 => buffer[a] * buffer[b],
                _ => throw new Exception($"Invalid opcode {buffer[i]} at position {i}")
            };
        }

        return buffer[0];
    }
}
