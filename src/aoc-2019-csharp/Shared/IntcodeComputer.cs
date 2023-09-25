namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private int[] _inputs;
    private int[] _outputs;

    public IntcodeComputer()
    {
        _inputs = Array.Empty<int>();
        _outputs = Array.Empty<int>();
    }

    public IntcodeComputer(int[] inputs)
    {
        _inputs = inputs;
        _outputs = Array.Empty<int>();
    }

    public IntcodeComputer(int[] inputs, int[] outputs)
    {
        _inputs = inputs;
        _outputs = outputs;
    }

    public int[] GetOutputs() => _outputs;

    public int RunProgram(int[] buffer, int? noun = null, int? verb = null)
    {
        buffer[1] = noun ?? buffer[1];
        buffer[2] = verb ?? buffer[2];

        for (var i = 0; i < buffer.Length;)
        {
            // HACK: fill in any missing parameter modes with 0
            var instruction = buffer[i].ToString().PadLeft(5, '0');
            var opcode = int.Parse(instruction[^2..]);

            if (opcode == 99)
            {
                break;
            }

            switch (opcode)
            {
                case 1:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    buffer[c] = aValue + bValue;

                    i += 4;
                    break;
                }
                case 2:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    buffer[c] = aValue * bValue;

                    i += 4;
                    break;
                }
                case 3:
                {
                    var parameter = buffer[i + 1];
                    buffer[parameter] = _inputs[0];
                    _inputs = _inputs[1..];
                    i += 2;
                    break;
                }
                case 4:
                {
                    var aMode = instruction[2];

                    var a = buffer[i + 1];

                    var aValue = aMode == '0' ? buffer[a] : a;

                    _outputs = _outputs.Append(aValue).ToArray();
                    i += 2;
                    break;
                }
                case 5:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b) = (buffer[i + 1], buffer[i + 2]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    if (aValue != 0)
                    {
                        i = bValue;
                    }
                    else
                    {
                        i += 3;
                    }

                    break;
                }
                case 6:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b) = (buffer[i + 1], buffer[i + 2]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    if (aValue == 0)
                    {
                        i = bValue;
                    }
                    else
                    {
                        i += 3;
                    }

                    break;
                }
                case 7:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    buffer[c] = aValue < bValue ? 1 : 0;

                    i += 4;
                    break;
                }
                case 8:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (buffer[i + 1], buffer[i + 2], buffer[i + 3]);

                    var aValue = aMode == '0' ? buffer[a] : a;
                    var bValue = bMode == '0' ? buffer[b] : b;

                    buffer[c] = aValue == bValue ? 1 : 0;

                    i += 4;
                    break;
                }
                default:
                {
                    throw new Exception($"Invalid opcode {opcode} at position {i}");
                }
            }
        }

        return buffer[0];
    }
}
