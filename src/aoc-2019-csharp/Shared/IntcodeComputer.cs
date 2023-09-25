namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private readonly int[] _memory;
    private int[] _inputs;
    private int[] _outputs;
    private int _instructionPointer = 0;

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

    public bool HasHalted { get; private set; }

    public int[] GetOutputs() => _outputs.ToArray();

    public int Run(int? noun = null, int? verb = null)
    {
        _memory[1] = noun ?? _memory[1];
        _memory[2] = verb ?? _memory[2];

        while (_instructionPointer < _memory.Length)
        {
            var instruction = _memory[_instructionPointer].ToString().PadLeft(5, '0');
            var opcode = int.Parse(instruction[^2..]);

            if (opcode == 99)
            {
                HasHalted = true;
                break;
            }

            var previousInstruction = _instructionPointer;

            _instructionPointer = opcode switch
            {
                1 => ProcessAdditionInstruction(instruction, _instructionPointer),
                2 => ProcessMultiplicationInstruction(instruction, _instructionPointer),
                3 => ProcessInputInstruction(_instructionPointer),
                4 => ProcessOutputInstruction(instruction, _instructionPointer),
                5 => ProcessJumpIfTrueInstruction(instruction, _instructionPointer),
                6 => ProcessJumpIfFalseInstruction(instruction, _instructionPointer),
                7 => ProcessLessThanInstruction(instruction, _instructionPointer),
                8 => ProcessEqualsInstruction(instruction, _instructionPointer),
                _ => throw new Exception($"Invalid opcode {opcode} at position {_instructionPointer}")
            };

            // if we didn't move the instruction pointer, we need to break out of the loop
            if (_instructionPointer == previousInstruction)
            {
                break;
            }
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
        // if we don't have any inputs, wait until we do
        if (_inputs.Length == 0)
        {
            return i;
        }

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

    public void AddInput(int value) =>
        _inputs = _inputs.Append(value).ToArray();
}
