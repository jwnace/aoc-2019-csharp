namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private readonly int[] _memory;
    private int[] _inputs;
    private int[] _outputs;
    private int _currentInstruction;

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

    public int Output => _outputs.Last();

    public int Run(int? noun = null, int? verb = null)
    {
        _memory[1] = noun ?? _memory[1];
        _memory[2] = verb ?? _memory[2];

        while (_currentInstruction < _memory.Length)
        {
            var instruction = _memory[_currentInstruction].ToString().PadLeft(5, '0');
            var opcode = int.Parse(instruction[^2..]);

            if (opcode == 99)
            {
                HasHalted = true;
                break;
            }

            var previousInstruction = _currentInstruction;

            _currentInstruction = opcode switch
            {
                1 => ProcessAdditionInstruction(instruction, _currentInstruction),
                2 => ProcessMultiplicationInstruction(instruction, _currentInstruction),
                3 => ProcessInputInstruction(_currentInstruction),
                4 => ProcessOutputInstruction(instruction, _currentInstruction),
                5 => ProcessJumpIfTrueInstruction(instruction, _currentInstruction),
                6 => ProcessJumpIfFalseInstruction(instruction, _currentInstruction),
                7 => ProcessLessThanInstruction(instruction, _currentInstruction),
                8 => ProcessEqualsInstruction(instruction, _currentInstruction),
                _ => throw new Exception($"Invalid opcode {opcode} at position {_currentInstruction}")
            };

            // if we didn't move the instruction pointer, we need to break out of the loop
            if (_currentInstruction == previousInstruction)
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
