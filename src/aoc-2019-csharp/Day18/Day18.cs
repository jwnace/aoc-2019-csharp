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
