namespace AoC2024;
public class Day20
{
    public static (char[,] grid, (int x, int y) start, (int x, int y) end) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var grid = new char[lines.Count(), lines.First().Length];
        var start = (0, 0);
        var end = (0, 0);
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (lines[row][col] == 'S')
                {
                    start = (col, row);
                    grid[row, col] = '.';
                }
                else if (lines[row][col] == 'E')
                {
                    end = (col, row);
                    grid[row, col] = '.';
                }
                else
                {
                    grid[row, col] = lines[row][col];
                }
            }
        }
        return (grid, start, end);
    }

    [Solveable("2024/Puzzles/Day20.txt", "Day 20 part 1", 20)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (grid, start, end) = ParseInput(filename);
        var shortest = FindShortestPath(grid, start, end);
        var paths = FindPathsRemovingOneWall(grid, start, end);
        var timeSaved = filename.Contains("test") ? 12 : 100;
        var result = paths.Where(_ => shortest - _ >= timeSaved);
        return new SolutionResult(result.Count().ToString());
    }

    [Solveable("2024/Puzzles/Day20.txt", "Day 20 part 2", 64)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (grid, start, end) = ParseInput(filename);
        var shortest = FindShortestPath(grid, start, end);
        //var paths = FindPathsRemovingUpToTwentyWalls(grid, start, end);
        //var timeSaved = filename.Contains("test") ? 50 : 100;
        //var res = paths.Where(_ => shortest - _.pathLength >= timeSaved);
        return new SolutionResult("");
        //return new SolutionResult(res.DistinctBy(_ => _.removedWalls).Count().ToString());
    }

    private static int FindShortestPath(char[,] grid, (int x, int y) start, (int x, int y) end)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var queue = new Queue<((int x, int y) pos, int dist)>();
        var visited = new bool[rows, cols];

        queue.Enqueue((start, 0));
        visited[start.y, start.x] = true;

        while (queue.Count > 0)
        {
            var (current, dist) = queue.Dequeue();

            if (current == end)
            {
                return dist;
            }

            foreach (var direction in directions)
            {
                var next = (x: current.x + direction.x, y: current.y + direction.y);

                if (next.x >= 0 && next.x < cols && next.y >= 0 && next.y < rows &&
                    grid[next.y, next.x] == '.' && !visited[next.y, next.x])
                {
                    queue.Enqueue((next, dist + 1));
                    visited[next.y, next.x] = true;
                }
            }
        }

        return -1;
    }

    private static List<int> FindPathsRemovingOneWall(char[,] grid, (int x, int y) start, (int x, int y) end)
    {
        var pathLengths = new List<int>();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col] == '#')
                {
                    grid[row, col] = '.';
                    int pathLength = FindShortestPath(grid, start, end);
                    if (pathLength != -1)
                    {
                        pathLengths.Add(pathLength);
                    }
                    grid[row, col] = '#';
                }
            }
        }

        return pathLengths;
    }

    private static List<(List<(int x, int y)> removedWalls, int pathLength)> FindPathsRemovingUpToTwentyWalls(char[,] grid, (int x, int y) start, (int x, int y) end)
    {
        var results = new List<(List<(int x, int y)> removedWalls, int pathLength)>();
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        var queue = new PriorityQueue<((int x, int y) pos, List<(int x, int y)> removedWalls, int dist), int>();
        var visited = new HashSet<string>();

        int Heuristic((int x, int y) a, (int x, int y) b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        string GetState((int x, int y) pos, List<(int x, int y)> removedWalls)
        {
            var state = $"{pos.x},{pos.y}:{string.Join(",", removedWalls.Select(w => $"{w.x},{w.y}"))}";
            return state;
        }

        queue.Enqueue((start, new List<(int x, int y)>(), 0), Heuristic(start, end));
        visited.Add(GetState(start, new List<(int x, int y)>()));

        while (queue.Count > 0)
        {
            var (current, removedWalls, dist) = queue.Dequeue();

            if (current == end)
            {
                results.Add((new List<(int x, int y)>(removedWalls), dist));
                continue;
            }

            foreach (var direction in directions)
            {
                var next = (x: current.x + direction.x, y: current.y + direction.y);

                if (next.x >= 0 && next.x < cols && next.y >= 0 && next.y < rows)
                {
                    var newRemovedWalls = new List<(int x, int y)>(removedWalls);
                    if (grid[next.y, next.x] == '#')
                    {
                        if (removedWalls.Count < 20)
                        {
                            newRemovedWalls.Add((next.x, next.y));
                            grid[next.y, next.x] = '.';
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var state = GetState(next, newRemovedWalls);
                    if (!visited.Contains(state))
                    {
                        queue.Enqueue((next, newRemovedWalls, dist + 1), dist + 1 + Heuristic(next, end));
                        visited.Add(state);
                    }

                    if (grid[next.y, next.x] == '.')
                    {
                        grid[next.y, next.x] = '#';
                    }
                }
            }
        }

        return results;
    }


}