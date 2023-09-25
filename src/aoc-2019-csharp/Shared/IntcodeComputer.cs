namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    public static int RunProgram(int[] buffer, int noun, int verb)
    {
        buffer[1] = noun;
        buffer[2] = verb;

        for (var i = 0; i < buffer.Length; i += 4)
        {
            var opcode = buffer[i];

            if (opcode == 99)
            {
                break;
            }

            var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

            buffer[c] = opcode switch
            {
                1 => buffer[a] + buffer[b],
                2 => buffer[a] * buffer[b],
                _ => throw new Exception($"Invalid opcode {opcode} at position {i}")
            };
        }

        return buffer[0];
    }
}
