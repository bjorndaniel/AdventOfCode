namespace AoC2023;
public class Day23
{
    private static Dictionary<((int x, int y) pos, int dist), int> _memo = new();
    private static int _maxDist = 0;
    private static List<char> _slopes = ['>', '<', '^', 'v'];
    private static Dictionary<Direction, (int x, int y)> _directions = new Dictionary<Direction, (int x, int y)>
     {
        { Direction.Down, ( 0, 1 ) },
        { Direction.Up, ( 0, -1 ) },
        { Direction.Right, (1, 0) },
        { Direction.Left, ( -1, 0 ) }
     };
    
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var chars = new char[lines[0].Length, lines.Length];
        foreach (var (line, row) in lines.WithIndex())
        {
            foreach (var (c, col) in line.WithIndex())
            {
                chars[col, row] = c;
            }
        }
        return chars;
    }

    [Solveable("2023/Puzzles/Day23.txt", "Day23 part 1", 23)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var map = ParseInput(filename);
        var visited = new bool[map.GetLength(0), map.GetLength(1)];
        var start = (1, 0);
        var end = (map.GetLength(0) - 2, map.GetLength(1) - 1);
        _maxDist = 0;
        _memo = new();
        Hamiltonian(map, start, end, visited, start, 0);
        return new SolutionResult(_maxDist.ToString());
    }

    [Solveable("2023/Puzzles/Day23.txt", "Day23 part 2", 23)]
    //This part was learnt from https://www.youtube.com/watch?v=NTLYL7Mg2jU
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var map = ParseInput(filename);
        var start = (1, 0);
        var end = (map.GetLength(0) - 2, map.GetLength(1) - 1);
        var nodes = CreateNodes(map, start, end);
        var graph = new Dictionary<(int x, int y), Dictionary<(int x, int y), int>>();
        nodes.ForEach(_ => graph.Add(_, new()));
        CreateGraph(map, nodes, graph);

        return new SolutionResult(DFS(start, end, graph, new()).ToString());

        static int DFS((int x, int y) current, (int x, int y) end, Dictionary<(int x, int y), Dictionary<(int x, int y), int>> graph, List<(int x, int y)> seen)
        {
            if (current == end)
            {
                return 0;
            }
            var max = int.MinValue;
            seen.Add(current);
            foreach (var n in graph[current].Keys)
            {
                if (!seen.Contains(n))
                {
                    max = Math.Max(max, graph[current][n] + DFS(n, end, graph, seen));
                }
            }
            seen.Remove(current);
            return max;
        }

        static List<(int x, int y)> CreateNodes(char[,] map, (int, int) start, (int, int) end)
        {
            var nodes = new List<(int x, int y)> { start, end };
            for (int row = 0; row < map.GetLength(1); row++)
            {
                for (int col = 0; col < map.GetLength(0); col++)
                {
                    if (map[col, row] == '#')
                    {
                        continue;
                    }
                    var neighbors = 0;
                    foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                    {
                        var (x, y) = (col + _directions[dir].x, row + _directions[dir].y);
                        if (x >= 0 && x < map.GetLength(0) &&
                                                   y >= 0 && y < map.GetLength(1) &&
                                                                          map[x, y] != '#')
                        {
                            neighbors++;
                        }
                    }
                    if (neighbors >= 3)
                    {
                        nodes.Add((col, row));
                    }
                }
            }

            return nodes;
        }

        static void CreateGraph(char[,] map, List<(int x, int y)> nodes, Dictionary<(int x, int y), Dictionary<(int x, int y), int>> graph)
        {
            foreach (var node in nodes)
            {
                var stack = new Stack<(int c, (int x, int y))>();
                stack.Push((0, node));
                var seen = new List<(int x, int y)> { node };
                while (stack.Any())
                {
                    var (c, pos) = stack.Pop();
                    if (c != 0 && nodes.Contains(pos))
                    {
                        if (!graph[node].ContainsKey(pos))
                        {
                            graph[node].Add(pos, c);
                        }
                        graph[node][pos] = c;
                        continue;
                    }
                    foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                    {
                        var (x, y) = (pos.x + _directions[dir].x, pos.y + _directions[dir].y);
                        if (x >= 0 && x < map.GetLength(0) &&
                            y >= 0 && y < map.GetLength(1) &&
                            map[x, y] != '#' && !seen.Contains((x, y))
                        )
                        {
                            seen.Add((x, y));
                            stack.Push((c + 1, (x, y)));
                        }
                    }
                }
            }
        }
    }

    private static void Hamiltonian(char[,] map, (int x, int y) start, (int x, int y) end, bool[,] visited, (int x, int y) pos, int dist)
    {
        if (pos == end || AllVisited(visited))
        {
            _maxDist = Math.Max(dist, _maxDist);
            return;
        }

        if (_memo.TryGetValue((pos, dist), out int cachedDist))
        {
            _maxDist = Math.Max(cachedDist, _maxDist);
            return;
        }

        visited[pos.x, pos.y] = true;
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            var (x, y) = (pos.x + _directions[dir].x, pos.y + _directions[dir].y);
            if (IsValid(map, visited, (x, y)))
            {
                if (_slopes.Contains(map[x, y]))
                {
                    switch (map[x, y])
                    {
                        case '>':
                            if (dir == Direction.Right)
                            {
                                Hamiltonian(map, start, end, visited, (x, y), dist + 1);
                            }
                            break;
                        case '<':
                            if (dir == Direction.Left)
                            {
                                Hamiltonian(map, start, end, visited, (x, y), dist + 1);
                            }
                            break;
                        case '^':
                            if (dir == Direction.Up)
                            {
                                Hamiltonian(map, start, end, visited, (x, y), dist + 1);
                            }
                            break;
                        case 'v':
                            if (dir == Direction.Down)
                            {
                                Hamiltonian(map, start, end, visited, (x, y), dist + 1);
                            }
                            break;
                    }
                }
                else
                {
                    Hamiltonian(map, start, end, visited, (x, y), dist + 1);
                }
            }
        }
        visited[pos.x, pos.y] = false;
        _memo[(pos, dist)] = _maxDist;

        static bool IsValid(char[,] map, bool[,] visited, (int x, int y) pos)
        {
            return pos.x >= 0 && pos.x < map.GetLength(0) &&
                   pos.y >= 0 && pos.y < map.GetLength(1) &&
                   map[pos.x, pos.y] != '#' && !visited[pos.x, pos.y];
        }
    }

    private static bool AllVisited(bool[,] visited)
    {
        var allVisited = true;
        for (int row = 0; row < visited.GetLength(1); row++)
        {
            for (int col = 0; col < visited.GetLength(0); col++)
            {
                if (!visited[col, row])
                {
                    allVisited = false;
                }
            }
        }
        return allVisited;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}