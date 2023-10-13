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
            var neighbors = new[]
            {
                (x, y + 1),
                (x, y - 1),
                (x - 1, y),
                (x + 1, y)
            };

            if (neighbors.All(n => visited.ContainsKey(n)))
            {
                if (path.Count == 0)
                {
                    break;
                }

                var moveToUndo = path.Pop();

                var move = moveToUndo switch
                {
                    1 => 2,
                    2 => 1,
                    3 => 4,
                    4 => 3,
                    _ => throw new Exception("Invalid direction")
                };

                computer.AddInput(move);
                computer.Run();

                if (computer.Output != 1)
                {
                    throw new Exception("Something went wrong while backtracking!");
                }

                (x, y) = move switch
                {
                    1 => (x, y + 1),
                    2 => (x, y - 1),
                    3 => (x - 1, y),
                    4 => (x + 1, y),
                    _ => throw new Exception("Invalid direction")
                };

                continue;
            }

            var firstUnvisitedNeighbor = neighbors.First(n => !visited.ContainsKey(n));

            var direction = firstUnvisitedNeighbor switch
            {
                (_, _) N when N == (x, y + 1) => 1,
                (_, _) S when S == (x, y - 1) => 2,
                (_, _) W when W == (x - 1, y) => 3,
                (_, _) E when E == (x + 1, y) => 4,
                _ => throw new Exception("Invalid direction")
            };

            computer.AddInput(direction);
            computer.Run();
            var output = computer.Output;

            if (output == 0)
            {
                visited[(firstUnvisitedNeighbor)] = int.MaxValue;
                continue;
            }

            if (output == 1)
            {
                visited[(firstUnvisitedNeighbor)] = visited[(x, y)] + 1;
                path.Push(direction);
                (x, y) = (firstUnvisitedNeighbor);
                continue;
            }

            if (output == 2)
            {
                return visited[(x, y)] + 1;
            }
        }

        return 0;
    }

    private static int Solve2(string input)
    {
        var map = BuildMap(input);

        var oxygenSystem = (-12, -12, 0);
        var visited = new HashSet<(int X, int Y)>();
        var queue = new Queue<(int X, int Y, int Distance)>();
        queue.Enqueue(oxygenSystem);

        var maxDistance = 0;

        while (queue.Count > 0)
        {
            var (x, y, distance) = queue.Dequeue();
            maxDistance = Math.Max(maxDistance, distance);
            visited.Add((x, y));

            var neighbors = new (int X, int Y)[]
            {
                (x, y + 1),
                (x, y - 1),
                (x - 1, y),
                (x + 1, y)
            };

            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                if (map[neighbor] == '#')
                {
                    continue;
                }

                queue.Enqueue((neighbor.X, neighbor.Y, distance + 1));
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
            var neighbors = new[]
            {
                (x, y + 1),
                (x, y - 1),
                (x - 1, y),
                (x + 1, y)
            };

            if (neighbors.All(n => visited.ContainsKey(n)))
            {
                if (path.Count == 0)
                {
                    break;
                }

                var moveToUndo = path.Pop();

                var move = moveToUndo switch
                {
                    1 => 2,
                    2 => 1,
                    3 => 4,
                    4 => 3,
                    _ => throw new Exception("Invalid direction")
                };

                computer.AddInput(move);
                computer.Run();

                if (computer.Output != 1)
                {
                    throw new Exception("Something went wrong while backtracking!");
                }

                (x, y) = move switch
                {
                    1 => (x, y + 1),
                    2 => (x, y - 1),
                    3 => (x - 1, y),
                    4 => (x + 1, y),
                    _ => throw new Exception("Invalid direction")
                };

                continue;
            }

            var firstUnvisitedNeighbor = neighbors.First(n => !visited.ContainsKey(n));

            var direction = firstUnvisitedNeighbor switch
            {
                (_, _) N when N == (x, y + 1) => 1,
                (_, _) S when S == (x, y - 1) => 2,
                (_, _) W when W == (x - 1, y) => 3,
                (_, _) E when E == (x + 1, y) => 4,
                _ => throw new Exception("Invalid direction")
            };

            computer.AddInput(direction);
            computer.Run();
            var output = computer.Output;

            if (output == 0)
            {
                visited[(firstUnvisitedNeighbor)] = int.MaxValue;
                continue;
            }

            if (output == 1)
            {
                visited[(firstUnvisitedNeighbor)] = visited[(x, y)] + 1;
                path.Push(direction);
                (x, y) = (firstUnvisitedNeighbor);
                continue;
            }

            if (output == 2)
            {
                visited[(firstUnvisitedNeighbor)] = visited[(x, y)] + 1;
                path.Push(direction);
                (x, y) = (firstUnvisitedNeighbor);
            }
        }

        return visited.ToDictionary(c => c.Key, c => c.Value == int.MaxValue ? '#' : '.');
    }
}
