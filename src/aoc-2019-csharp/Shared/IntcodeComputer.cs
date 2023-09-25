namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private readonly int[] _memory;
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
        _memory = memory.ToArray();
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

            i = opcode switch
            {
                1 => ProcessAdditionInstruction(instruction, i),
                2 => ProcessMultiplicationInstruction(instruction, i),
                3 => ProcessInputInstruction(i),
                4 => ProcessOutputInstruction(instruction, i),
                5 => ProcessJumpIfTrueInstruction(instruction, i),
                6 => ProcessJumpIfFalseInstruction(instruction, i),
                7 => ProcessLessThanInstruction(instruction, i),
                8 => ProcessEqualsInstruction(instruction, i),
                _ => throw new Exception($"Invalid opcode {opcode} at position {i}")
            };
        }

        return _memory[0];
    }

    private int ProcessAdditionInstruction(string instruction, int i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        _memory[c] = aValue + bValue;

        return i + 4;
    }

    private int ProcessMultiplicationInstruction(string instruction, int i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        _memory[c] = aValue * bValue;

        return i + 4;
    }

    private int ProcessInputInstruction(int i)
    {
        var a = _memory[i + 1];
        _memory[a] = _inputs[0];
        _inputs = _inputs[1..];

        return i + 2;
    }

    private int ProcessOutputInstruction(string instruction, int i)
    {
        var a = _memory[i + 1];
        var aMode = instruction[2];
        var aValue = aMode == '0' ? _memory[a] : a;

        _outputs = _outputs.Append(aValue).ToArray();

        return i + 2;
    }

    private int ProcessJumpIfTrueInstruction(string instruction, int i)
    {
        var (a, b) = (_memory[i + 1], _memory[i + 2]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        i = aValue == 0 ? i + 3 : bValue;

        return i;
    }

    private int ProcessJumpIfFalseInstruction(string instruction, int i)
    {
        var (a, b) = (_memory[i + 1], _memory[i + 2]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        i = aValue == 0 ? bValue : i + 3;

        return i;
    }

    private int ProcessLessThanInstruction(string instruction, int i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        _memory[c] = aValue < bValue ? 1 : 0;

        return i + 4;
    }

    private int ProcessEqualsInstruction(string instruction, int i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode) = (instruction[2], instruction[1]);
        var aValue = aMode == '0' ? _memory[a] : a;
        var bValue = bMode == '0' ? _memory[b] : b;

        _memory[c] = aValue == bValue ? 1 : 0;

        return i + 4;
    }
}
