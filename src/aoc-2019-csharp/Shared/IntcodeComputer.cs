namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private int[] _memory;
    private int[] _inputs;
    private int[] _outputs;

    public IntcodeComputer(int[] memory)
    {
        _memory = memory.ToArray();
        _inputs = Array.Empty<int>();
        _outputs = Array.Empty<int>();
    }

    public IntcodeComputer(int[] memory, int[] inputs)
    {
        _memory = memory;
        _inputs = inputs;
        _outputs = Array.Empty<int>();
    }

    public int[] GetOutputs() => _outputs.ToArray();

    public int Run(int? noun = null, int? verb = null)
    {
        _memory[1] = noun ?? _memory[1];
        _memory[2] = verb ?? _memory[2];

        for (var i = 0; i < _memory.Length;)
        {
            var instruction = _memory[i].ToString().PadLeft(5, '0');
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
                    var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

                    _memory[c] = aValue + bValue;

                    i += 4;
                    break;
                }
                case 2:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

                    _memory[c] = aValue * bValue;

                    i += 4;
                    break;
                }
                case 3:
                {
                    var parameter = _memory[i + 1];
                    _memory[parameter] = _inputs[0];
                    _inputs = _inputs[1..];
                    i += 2;
                    break;
                }
                case 4:
                {
                    var aMode = instruction[2];

                    var a = _memory[i + 1];

                    var aValue = aMode == '0' ? _memory[a] : a;

                    _outputs = _outputs.Append(aValue).ToArray();
                    i += 2;
                    break;
                }
                case 5:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b) = (_memory[i + 1], _memory[i + 2]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

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
                    var (a, b) = (_memory[i + 1], _memory[i + 2]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

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
                    var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

                    _memory[c] = aValue < bValue ? 1 : 0;

                    i += 4;
                    break;
                }
                case 8:
                {
                    var (aMode, bMode) = (instruction[2], instruction[1]);
                    var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);

                    var aValue = aMode == '0' ? _memory[a] : a;
                    var bValue = bMode == '0' ? _memory[b] : b;

                    _memory[c] = aValue == bValue ? 1 : 0;

                    i += 4;
                    break;
                }
                default:
                {
                    throw new Exception($"Invalid opcode {opcode} at position {i}");
                }
            }
        }

        return _memory[0];
    }
}
