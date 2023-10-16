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
        var initialState = new State(start.Row, start.Col, 0);
        var visited = new Dictionary<State, int>();
        var queue = new Queue<State>();

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

                var newState = new State(neighbor.Row, neighbor.Col, keys);

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

            // TODO: remove this once we have a working algorithm
            if (!reachableKeys.Any())
            {
                var render = DrawGrid(grid, keys, robots);
                Console.WriteLine(render);

                throw new Exception("We can't get to any of the remaining keys!");
            }

            foreach (var reachableKey in reachableKeys)
            {
                // Console.WriteLine($"Owned Keys: {(Keys)keys}");

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

        return -1;
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
        Dictionary<(int Row, int Col), char> grid,
        int ownedKeys,
        (int Row, int Col)[] robotLocations)
    {
        var result = new List<(int Row, int Col, char Key, int RobotIndex, int Distance)>();
        var queue = new Queue<(int Row, int Col, int RobotIndex, int Distance)>();
        var seen = new HashSet<(int Row, int Col)>();

        for (var index = 0; index < robotLocations.Length; index++)
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


    // private static object GetReachableKeys((int Row, int Col) robot)
    // {
    //     var keys = new Dictionary<(int Row, int Col), char>();
    //
    //     var (row, col) = robot;
    //
    //     var state = (Row: row, Col: col, Keys: 0, Distance: 0);
    //
    //     var queue = new Queue<(int Row, int Col, int Keys, int Distance)>();
    // }

    [Flags]
    private enum Keys
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
        D = 1 << 3,
        E = 1 << 4,
        F = 1 << 5,
        G = 1 << 6,
        H = 1 << 7,
        I = 1 << 8,
        J = 1 << 9,
        K = 1 << 10,
        L = 1 << 11,
        M = 1 << 12,
        N = 1 << 13,
        O = 1 << 14,
        P = 1 << 15,
        Q = 1 << 16,
        R = 1 << 17,
        S = 1 << 18,
        T = 1 << 19,
        U = 1 << 20,
        V = 1 << 21,
        W = 1 << 22,
        X = 1 << 23,
        Y = 1 << 24,
        Z = 1 << 25,
    }

    private static (int Row, int Col)[] GetRobotLocations(Dictionary<(int Row, int Col), char> grid)
    {
        return grid.Where(x => x.Value == '@')
            .OrderBy(x => x.Key.Row)
            .ThenBy(x => x.Key.Col)
            .Select(x => x.Key)
            .ToArray();
    }

    public static int Solve2_WorksButSlow(string[] input)
    {
        var grid = BuildGrid(input);

        // top left quadrant
        var quadrant1 = grid
            .Where(x => x.Key.Row < grid.Max(x => x.Key.Row) / 2 && x.Key.Col < grid.Max(x => x.Key.Col) / 2)
            .ToDictionary(x => x.Key, x => x.Value);

        var bot1Keys = quadrant1.Where(x => char.IsLower(x.Value)).Select(x => x.Value).ToArray();
        // var bot1Keys = new[] { 'a', 'c', 'e' };

        // top right quadrant
        var quadrant2 = grid
            .Where(x => x.Key.Row < grid.Max(x => x.Key.Row) / 2 && x.Key.Col > grid.Max(x => x.Key.Col) / 2)
            .ToDictionary(x => x.Key, x => x.Value);

        var bot2Keys = quadrant2.Where(x => char.IsLower(x.Value)).Select(x => x.Value).ToArray();
        // var bot2Keys = new[] { 'h', 'j', 'l' };

        // bottom left quadrant
        var quadrant3 = grid
            .Where(x => x.Key.Row > grid.Max(x => x.Key.Row) / 2 && x.Key.Col < grid.Max(x => x.Key.Col) / 2)
            .ToDictionary(x => x.Key, x => x.Value);

        var bot3Keys = quadrant3.Where(x => char.IsLower(x.Value)).Select(x => x.Value).ToArray();
        // var bot3Keys = new[] { 'b', 'd', 'f' };

        // bottom right quadrant
        var quadrant4 = grid
            .Where(x => x.Key.Row > grid.Max(x => x.Key.Row) / 2 && x.Key.Col > grid.Max(x => x.Key.Col) / 2)
            .ToDictionary(x => x.Key, x => x.Value);

        var bot4Keys = quadrant4.Where(x => char.IsLower(x.Value)).Select(x => x.Value).ToArray();
        // var bot4Keys = new[] { 'g', 'i', 'k' };

        var robotLocations = grid.Where(x => x.Value == '@')
            .OrderBy(x => x.Key.Row)
            .ThenBy(x => x.Key.Col)
            .Select(x => x.Key)
            .ToArray();

        var keyCount = grid.Count(x => char.IsLower(x.Value));
        var allKeys = (1 << keyCount) - 1;

        // the state is the positions of the robots and the keys we have
        var initialState = new State2(
            robotLocations[0].Row, robotLocations[0].Col,
            robotLocations[1].Row, robotLocations[1].Col,
            robotLocations[2].Row, robotLocations[2].Col,
            robotLocations[3].Row, robotLocations[3].Col,
            0);

        var visited = new Dictionary<State2, int>();
        var queue = new PriorityQueue<State2, int>();

        queue.Enqueue(initialState, 0);
        visited[initialState] = 0;

        while (queue.Count > 0)
        {
            // if (visited.Count % 10_000 == 0)
            // {
            // Console.WriteLine($"Visited {visited.Count} states");
            // }

            var state = queue.Dequeue();
            var (row1, col1, row2, col2, row3, col3, row4, col4, keys) = state;
            var robots = new (int Row, int Col)[]
            {
                (row1, col1),
                (row2, col2),
                (row3, col3),
                (row4, col4),
            };

            // loop through the robots and give them each a chance to move
            // TODO: just because a robot CAN move doesn't mean it SHOULD move
            // TODO: make each bot stop moving after it has collected all the keys it can
            for (var robotIndex = 0; robotIndex < 4; robotIndex++)
            {
                var (row, col) = robots[robotIndex];

                // if there is a key at my location, pick it up
                if (char.IsLower(grid[(row, col)]))
                {
                    var key = grid[(row, col)];

                    if ((keys & 1 << key - 'a') == 0)
                    {
                        keys |= 1 << key - 'a';

                        // TODO: if we picked up a key, give all the bots a chance to move again
                        // robotIndex = -1;
                        // continue;

                        var newState = new State2(row1, col1, row2, col2, row3, col3, row4, col4, keys);

                        if (visited.ContainsKey(newState))
                        {
                            continue;
                        }

                        queue.Enqueue(newState, 0);
                        visited[newState] = visited[state];
                    }
                }

                // if we have all the keys, we're done
                if (keys == allKeys)
                {
                    return visited[state];
                }

                // if this bot has no more keys to collect, skip it
                if (robotIndex == 0 && bot1Keys.All(x => (keys & 1 << x - 'a') != 0))
                {
                    continue;
                }

                if (robotIndex == 1 && bot2Keys.All(x => (keys & 1 << x - 'a') != 0))
                {
                    continue;
                }

                if (robotIndex == 2 && bot3Keys.All(x => (keys & 1 << x - 'a') != 0))
                {
                    continue;
                }

                if (robotIndex == 3 && bot4Keys.All(x => (keys & 1 << x - 'a') != 0))
                {
                    continue;
                }

                // switch (robotIndex)
                // {
                //     // if this bot has no more keys to collect, skip it
                //     // TODO: figure out how to reduce the duplication here
                //     case 0 when bot1Keys.All(x => (keys & 1 << x - 'a') != 0):
                //     case 1 when bot2Keys.All(x => (keys & 1 << x - 'a') != 0):
                //     case 2 when bot3Keys.All(x => (keys & 1 << x - 'a') != 0):
                //     case 3 when bot4Keys.All(x => (keys & 1 << x - 'a') != 0):
                //     {
                //         continue;
                //     }
                // }

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

                    var newRobots = robots.ToArray();
                    newRobots[robotIndex] = neighbor;

                    var newState = new State2(
                        newRobots[0].Row, newRobots[0].Col,
                        newRobots[1].Row, newRobots[1].Col,
                        newRobots[2].Row, newRobots[2].Col,
                        newRobots[3].Row, newRobots[3].Col,
                        keys);

                    if (visited.ContainsKey(newState))
                    {
                        continue;
                    }

                    queue.Enqueue(newState, visited[state] + 1);
                    visited[newState] = visited[state] + 1;
                }
            }
        }

        throw new Exception("No solution found");
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

    private record State(int Row, int Col, int Keys);

    private record State2(int Row1, int Col1, int Row2, int Col2, int Row3, int Col3, int Row4, int Col4, int Keys);

    private record State3(int Row1, int Col1, int Row2, int Col2, int Row3, int Col3, int Row4, int Col4, int Keys,
        int Steps);
}
