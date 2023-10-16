namespace aoc_2019_csharp.Day18;

public static class Day18
{
    private static readonly string[] Input = File.ReadAllLines("Day18/day18.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var entranceLocation = grid.First(x => x.Value == '@').Key;
        var keyLocations = grid.Where(x => char.IsLower(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        var doorLocations = grid.Where(x => char.IsUpper(x.Value)).ToDictionary(x => x.Key, x => x.Value);
        var initialState = new State(entranceLocation.Row, entranceLocation.Col, 0b00000000000000000000000000);
        var allKeys = (1 << keyLocations.Count) - 1;

        var queue = new Queue<State>();
        queue.Enqueue(initialState);

        var visited = new Dictionary<State, int>();
        visited[initialState] = 0;

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, keys) = state;
            var distance = visited[state];

            // if there is a key at my location, pick it up
            if (keyLocations.TryGetValue((row, col), out var key))
            {
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
                if (grid[neighbor] == '#')
                {
                    continue;
                }

                // check we are at a door that we can open
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
                visited[newState] = distance + 1;
            }
        }

        throw new Exception("No solution found");
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
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
}
