using System.Text;

namespace aoc_2019_csharp.Day18;

public static class Day18
{
    private static readonly string[] Part1Input = File.ReadAllLines("Day18/day18-part1.txt");
    private static readonly string[] Part2Input = File.ReadAllLines("Day18/day18-part2.txt");

    public static int Part1() => Solve1(Part1Input);

    public static int Part2() => Solve2(Part2Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var start = grid.First(x => x.Value == '@').Key;
        var keyCount = grid.Count(x => char.IsLower(x.Value));
        var allKeys = (1 << keyCount) - 1;
        var initialState = (start.Row, start.Col, 0);
        var visited = new Dictionary<(int Row, int Col, int Keys), int>();
        var queue = new Queue<(int Row, int Col, int Keys)>();

        queue.Enqueue(initialState);
        visited[initialState] = 0;

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, keys) = state;

            // if there is a key at my location, pick it up
            if (char.IsLower(grid[(row, col)]))
            {
                var key = grid[(row, col)];
                keys |= 1 << key - 'a';
            }

            // if we have all the keys, we're done
            if (keys == allKeys)
            {
                return visited[state];
            }

            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                // check if we would run into a wall
                if (grid[neighbor] == '#')
                {
                    continue;
                }

                // check if we would run into a door that we can't unlock
                if (char.IsUpper(grid[neighbor]))
                {
                    var door = grid[neighbor];

                    if ((keys & 1 << door - 'A') == 0)
                    {
                        continue;
                    }
                }

                var newState = (neighbor.Row, neighbor.Col, keys);

                if (visited.ContainsKey(newState))
                {
                    continue;
                }

                queue.Enqueue(newState);
                visited[newState] = visited[state] + 1;
            }
        }

        throw new Exception("No solution found");
    }

    public static int Solve2(string[] input)
    {
        var grid = BuildGrid(input);
        var startingRobotLocations = GetRobotLocations(grid);
        var keyCount = grid.Count(x => char.IsLower(x.Value));
        var allKeys = (1 << keyCount) - 1;

        var queue = new PriorityQueue<(
            int Row1, int Col1,
            int Row2, int Col2,
            int Row3, int Col3,
            int Row4, int Col4,
            int Keys, int Steps), int>();

        var seen = new HashSet<(
            int Row1, int Col1,
            int Row2, int Col2,
            int Row3, int Col3,
            int Row4, int Col4,
            int Keys)>();

        queue.Enqueue((
            startingRobotLocations[0].Row, startingRobotLocations[0].Col,
            startingRobotLocations[1].Row, startingRobotLocations[1].Col,
            startingRobotLocations[2].Row, startingRobotLocations[2].Col,
            startingRobotLocations[3].Row, startingRobotLocations[3].Col,
            0, 0), 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (row1, col1, row2, col2, row3, col3, row4, col4, keys, steps) = state;
            var stateKey = (row1, col1, row2, col2, row3, col3, row4, col4, keys);
            var robots = new (int Row, int Col)[]
            {
                (row1, col1),
                (row2, col2),
                (row3, col3),
                (row4, col4),
            };

            // check if we've already seen this state
            if (seen.Contains(stateKey))
            {
                continue;
            }

            seen.Add(stateKey);

            // if we have all the keys, we're done
            if (keys == allKeys)
            {
                return steps;
            }

            var reachableKeys = GetReachableKeys(grid, keys, robots).ToList();

            foreach (var reachableKey in reachableKeys)
            {
                var newKeys = keys | 1 << reachableKey.Key - 'a';

                var newRobots = robots.ToArray();
                newRobots[reachableKey.RobotIndex] = (reachableKey.Row, reachableKey.Col);

                var newSteps = steps + reachableKey.Distance;

                queue.Enqueue((
                    newRobots[0].Row, newRobots[0].Col,
                    newRobots[1].Row, newRobots[1].Col,
                    newRobots[2].Row, newRobots[2].Col,
                    newRobots[3].Row, newRobots[3].Col,
                    newKeys, newSteps), newSteps);
            }
        }

        throw new Exception("No solution found");
    }

    private static string DrawGrid(Dictionary<(int Row, int Col), char> grid, int keys, (int Row, int Col)[] robots)
    {
        var sb = new StringBuilder();
        var rows = grid.Keys.Select(k => k.Row).Distinct().OrderBy(r => r).ToList();
        var cols = grid.Keys.Select(k => k.Col).Distinct().OrderBy(c => c).ToList();

        sb.AppendLine($"Robot Positions: {string.Join(", ", robots)}");
        sb.AppendLine($"Owned Keys: {(Keys)keys}");

        foreach (var row in rows)
        {
            foreach (var col in cols)
            {
                var c = grid[(row, col)];

                if (robots.Any(r => r == (row, col)))
                {
                    sb.Append('@');
                    continue;
                }

                if (char.IsLower(c) && (keys & 1 << (c - 'a')) > 0)
                {
                    sb.Append('.');
                    continue;
                }

                if (char.IsUpper(c) && (keys & 1 << (c - 'A')) > 0)
                {
                    sb.Append('.');
                    continue;
                }

                if (c == '@')
                {
                    sb.Append('.');
                    continue;
                }

                sb.Append(grid[(row, col)]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static IEnumerable<(int Row, int Col, char Key, int RobotIndex, int Distance)> GetReachableKeys(
        IReadOnlyDictionary<(int Row, int Col), char> grid,
        int ownedKeys,
        IList<(int Row, int Col)> robotLocations)
    {
        var result = new List<(int Row, int Col, char Key, int RobotIndex, int Distance)>();
        var queue = new Queue<(int Row, int Col, int RobotIndex, int Distance)>();
        var seen = new HashSet<(int Row, int Col)>();

        for (var index = 0; index < robotLocations.Count; index++)
        {
            queue.Enqueue((robotLocations[index].Row, robotLocations[index].Col, index, 0));
        }

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, robotIndex, distance) = state;
            var position = (state.Row, state.Col);

            // check if we've already been here
            if (seen.Contains(position))
            {
                continue;
            }

            seen.Add(position);

            // check if we ran into a wall
            if (grid[position] == '#')
            {
                continue;
            }

            // check if we ran into a locked door
            if (char.IsUpper(grid[position]) && (ownedKeys & 1 << grid[position] - 'A') == 0)
            {
                continue;
            }

            // check if we found a key
            if (char.IsLower(grid[position]))
            {
                // check if this is a key that we already own
                if ((ownedKeys & 1 << grid[position] - 'a') == 0)
                {
                    result.Add((state.Row, state.Col, grid[position], robotIndex, state.Distance));
                }
            }

            // TODO: figure out if there is a more memory-efficient way of doing this, instead of allocating a new array every time
            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                queue.Enqueue((neighbor.Row, neighbor.Col, robotIndex, distance + 1));
            }
        }

        return result;
    }

    private static (int Row, int Col)[] GetRobotLocations(Dictionary<(int Row, int Col), char> grid)
    {
        return grid.Where(x => x.Value == '@')
            .OrderBy(x => x.Key.Row)
            .ThenBy(x => x.Key.Col)
            .Select(x => x.Key)
            .ToArray();
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }
}
