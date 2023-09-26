namespace aoc_2019_csharp.Shared;

public class IntcodeComputer
{
    private readonly long[] _memory;
    private long[] _inputs;
    private long[] _outputs;
    private long _currentInstruction;
    private long _relativeBase = 0;

    public IntcodeComputer(long[] memory, int extraMemory = 0) : this(memory, Array.Empty<long>(), extraMemory)
    {
        _inputs = Array.Empty<long>();
        _outputs = Array.Empty<long>();
    }

    public IntcodeComputer(long[] memory, long[] inputs, int extraMemory = 0)
    {
        if (extraMemory > 0)
        {
            var zeroes = new long[extraMemory];
            Array.Fill(zeroes, 0);

            _memory = memory.ToArray().Concat(zeroes).ToArray();
        }
        else
        {
            _memory = memory.ToArray();
        }

        _inputs = inputs;
        _outputs = Array.Empty<long>();
    }

    public bool HasHalted { get; private set; }

    public long Output => _outputs.Last();

    public long Run(long? noun = null, long? verb = null)
    {
        _memory[1] = noun ?? _memory[1];
        _memory[2] = verb ?? _memory[2];

        while (_currentInstruction < _memory.Length)
        {
            var instruction = _memory[_currentInstruction].ToString().PadLeft(5, '0');
            var opcode = long.Parse(instruction[^2..]);

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
                3 => ProcessInputInstruction(instruction, _currentInstruction),
                4 => ProcessOutputInstruction(instruction, _currentInstruction),
                5 => ProcessJumpIfTrueInstruction(instruction, _currentInstruction),
                6 => ProcessJumpIfFalseInstruction(instruction, _currentInstruction),
                7 => ProcessLessThanInstruction(instruction, _currentInstruction),
                8 => ProcessEqualsInstruction(instruction, _currentInstruction),
                9 => ProcessAdjustRelativeBaseInstruction(instruction, _currentInstruction),
                _ => throw new Exception($"Invalid opcode {opcode} at position {_currentInstruction}")
            };

            // if we didn't move the instruction polonger, we need to break out of the loop
            if (_currentInstruction == previousInstruction)
            {
                break;
            }
        }

        return _memory[0];
    }

    private long ProcessAdditionInstruction(string instruction, long i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode, cMode) = (instruction[2], instruction[1], instruction[0]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        var cValue = cMode switch
        {
            '2' => c + _relativeBase,
            '0' => c,
            _ => throw new ArgumentException($"Invalid cMode {cMode} at position {i}")
        };

        _memory[cValue] = aValue + bValue;

        return i + 4;
    }

    private long ProcessMultiplicationInstruction(string instruction, long i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode, cMode) = (instruction[2], instruction[1], instruction[0]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        var cValue = cMode switch
        {
            '2' => c + _relativeBase,
            '0' => c,
            _ => throw new ArgumentException($"Invalid cMode {cMode} at position {i}")
        };

        _memory[cValue] = aValue * bValue;

        return i + 4;
    }

    private long ProcessInputInstruction(string instruction, long i)
    {
        // if we don't have any inputs, wait until we do
        if (_inputs.Length == 0)
        {
            return i;
        }

        var a = _memory[i + 1];
        var aMode = instruction[2];

        var aValue = aMode switch
        {
            '2' => a + _relativeBase,
            '0' => a,
            _ => throw new ArgumentException($"Invalid aMode {aMode} at position {i}"),
        };

        _memory[aValue] = _inputs[0];
        _inputs = _inputs[1..];

        return i + 2;
    }

    private long ProcessOutputInstruction(string instruction, long i)
    {
        var a = _memory[i + 1];
        var aMode = instruction[2];

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        _outputs = _outputs.Append(aValue).ToArray();

        return i + 2;
    }

    private long ProcessJumpIfTrueInstruction(string instruction, long i)
    {
        var (a, b) = (_memory[i + 1], _memory[i + 2]);
        var (aMode, bMode) = (instruction[2], instruction[1]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        i = aValue == 0 ? i + 3 : bValue;

        return i;
    }

    private long ProcessJumpIfFalseInstruction(string instruction, long i)
    {
        var (a, b) = (_memory[i + 1], _memory[i + 2]);
        var (aMode, bMode) = (instruction[2], instruction[1]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        i = aValue == 0 ? bValue : i + 3;

        return i;
    }

    private long ProcessLessThanInstruction(string instruction, long i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode, cMode) = (instruction[2], instruction[1], instruction[0]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        var cValue = cMode switch
        {
            '2' => c + _relativeBase,
            '0' => c,
            _ => throw new ArgumentException($"Invalid cMode {cMode} at position {i}")
        };

        _memory[cValue] = aValue < bValue ? 1 : 0;

        return i + 4;
    }

    private long ProcessEqualsInstruction(string instruction, long i)
    {
        var (a, b, c) = (_memory[i + 1], _memory[i + 2], _memory[i + 3]);
        var (aMode, bMode, cMode) = (instruction[2], instruction[1], instruction[0]);

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        var bValue = bMode switch
        {
            '2' => _memory[b + _relativeBase],
            '1' => b,
            '0' => _memory[b],
        };

        var cValue = cMode switch
        {
            '2' => c + _relativeBase,
            '0' => c,
            _ => throw new ArgumentException($"Invalid cMode {cMode} at position {i}")
        };

        _memory[cValue] = aValue == bValue ? 1 : 0;

        return i + 4;
    }

    private long ProcessAdjustRelativeBaseInstruction(string instruction, long i)
    {
        var a = _memory[i + 1];
        var aMode = instruction[2];

        var aValue = aMode switch
        {
            '2' => _memory[a + _relativeBase],
            '1' => a,
            '0' => _memory[a],
        };

        _relativeBase += aValue;

        return i + 2;
    }

    public void AddInput(long value) =>
        _inputs = _inputs.Append(value).ToArray();
}
