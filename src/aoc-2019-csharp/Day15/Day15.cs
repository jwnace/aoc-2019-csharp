using aoc_2019_csharp.Shared;

namespace aoc_2019_csharp.Day15;

public static class Day15
{
    private static readonly string Input = File.ReadAllText("Day15/day15.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 100);

        var visited = new Dictionary<(int X, int Y), int>();
        var (x, y) = (0, 0);
        visited[(x, y)] = 0;

        var path = new Stack<int>();

        while (true)
        {
            var neighbors = GetNeighbors(x, y);

            if (AllNeighborsHaveBeenVisited(neighbors, visited))
            {
                if (path.Count == 0)
                {
                    break;
                }

                (x, y) = BackTrack(path, computer, x, y);
                continue;
            }

            var firstUnvisitedNeighbor = neighbors.First(n => !visited.ContainsKey(n.Position));

            (x, y) = VisitNeighbor(computer, firstUnvisitedNeighbor, visited, path, x, y, out var oxygenSystem);

            if (oxygenSystem != (0, 0))
            {
                return visited[oxygenSystem];
            }
        }

        throw new Exception("No solution found!");
    }

    private static int Solve2(string input)
    {
        var map = BuildMap(input);
        var visited = new HashSet<(int X, int Y)>();
        var queue = new Queue<(int X, int Y, int Distance)>();
        // TODO: This probably shouldn't be hardcoded, but it works for now
        var oxygenSystem = (-12, -12, 0);
        queue.Enqueue(oxygenSystem);

        var maxDistance = 0;

        while (queue.Count > 0)
        {
            var (x, y, distance) = queue.Dequeue();
            maxDistance = Math.Max(maxDistance, distance);
            visited.Add((x, y));

            var neighbors = GetNeighbors(x, y);

            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor.Position))
                {
                    continue;
                }

                if (map[neighbor.Position] == '#')
                {
                    continue;
                }

                queue.Enqueue((neighbor.Position.X, neighbor.Position.Y, distance + 1));
            }
        }

        return maxDistance;
    }

    private static Dictionary<(int X, int Y), char> BuildMap(string input)
    {
        var memory = input.Split(',').Select(long.Parse).ToArray();
        var computer = new IntcodeComputer(memory, 100);

        var visited = new Dictionary<(int X, int Y), int>();
        var (x, y) = (0, 0);
        visited[(x, y)] = 0;

        var path = new Stack<int>();

        while (true)
        {
            var neighbors = GetNeighbors(x, y);

            if (AllNeighborsHaveBeenVisited(neighbors, visited))
            {
                if (path.Count == 0)
                {
                    break;
                }

                (x, y) = BackTrack(path, computer, x, y);
                continue;
            }

            var firstUnvisitedNeighbor = neighbors.First(n => !visited.ContainsKey(n.Position));

            (x, y) = VisitNeighbor(computer, firstUnvisitedNeighbor, visited, path, x, y, out _);
        }

        return visited.ToDictionary(c => c.Key, c => c.Value == int.MaxValue ? '#' : '.');
    }

    private static ((int X, int Y) Position, int Direction)[] GetNeighbors(int x, int y) =>
        new ((int X, int Y) Position, int Direction)[]
        {
            ((x, y + 1), 1),
            ((x, y - 1), 2),
            ((x - 1, y), 3),
            ((x + 1, y), 4),
        };

    private static (int x, int y) VisitNeighbor(
        IntcodeComputer computer,
        ((int X, int Y) Position, int Direction) neighbor,
        IDictionary<(int X, int Y), int> visited,
        Stack<int> path,
        int x,
        int y,
        out (int X, int Y) oxygenSystem)
    {
        oxygenSystem = (0, 0);

        computer.AddInput(neighbor.Direction);
        computer.Run();
        var output = computer.Output;

        if (output == 0)
        {
            visited[neighbor.Position] = int.MaxValue;
        }

        if (output == 1)
        {
            visited[neighbor.Position] = visited[(x, y)] + 1;
            path.Push(neighbor.Direction);
            (x, y) = neighbor.Position;
        }

        if (output == 2)
        {
            visited[neighbor.Position] = visited[(x, y)] + 1;
            path.Push(neighbor.Direction);
            (x, y) = neighbor.Position;
            oxygenSystem = (x, y);
        }

        return (x, y);
    }

    private static (int X, int Y) BackTrack(Stack<int> path, IntcodeComputer computer, int x, int y)
    {
        var move = ReverseMove(path.Pop(), x, y);

        computer.AddInput(move.Direction);
        computer.Run();

        return move.Position;
    }

    private static ((int X, int Y) Position, int Direction) ReverseMove(int moveToUndo, int x, int y)
    {
        return moveToUndo switch
        {
            1 => ((x, y - 1), 2),
            2 => ((x, y + 1), 1),
            3 => ((x + 1, y), 4),
            4 => ((x - 1, y), 3),
            _ => throw new Exception("Invalid direction")
        };
    }

    private static bool AllNeighborsHaveBeenVisited(
        IEnumerable<((int X, int Y) Position, int Direction)> neighbors,
        IReadOnlyDictionary<(int X, int Y), int> visited)
    {
        return neighbors.All(n => visited.ContainsKey(n.Position));
    }
}
