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
        var path = FindShortestPath(grid, start, end);
        var timeSaved = filename.Contains("test") ? 12 : 100;
        var result = FindPathsRemovingOneWall(grid, start, end, path - timeSaved);
        return new SolutionResult(result.Count().ToString());
    }

    [Solveable("2024/Puzzles/Day20.txt", "Day 20 part 2", 64)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (grid, start, end) = ParseInput(filename);
        var path = FindShortestPath(grid, start, end);
        var timeSaved = filename.Contains("test") ? 50 : 100;
        var result = FindPathsRemovingUpTo20Walls(grid, start, end, path - timeSaved, printer);
        return new SolutionResult(result.Count().ToString());
    }

    private static List<int> FindPathsRemovingOneWall(char[,] grid, (int x, int y) start, (int x, int y) end, int targetPathLength)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var paths = new List<int>();
        var memo = new Dictionary<(int, int), int>();
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col] == '#')
                {
                    bool isValidRemoval = false;
                    foreach (var direction in directions)
                    {
                        var next = (x: col + direction.x, y: row + direction.y);
                        if (next.x >= 0 && next.x < cols && next.y >= 0 && next.y < rows && grid[next.y, next.x] == '.')
                        {
                            isValidRemoval = true;
                            break;
                        }
                    }

                    if (!isValidRemoval)
                    {
                        continue;
                    }

                    grid[row, col] = '.';
                    var key = (row, col);
                    if (!memo.TryGetValue(key, out var pathLength))
                    {
                        pathLength = FindShortestPath(grid, start, end);
                        memo[key] = pathLength;
                    }
                    if (pathLength <= targetPathLength)
                    {
                        paths.Add(pathLength);
                    }
                    grid[row, col] = '#';
                }
            }
        }

        return paths;
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

    private static List<List<(int x, int y)>> FindPathsRemovingUpTo20Walls(char[,] grid, (int x, int y) start, (int x, int y) end, int targetPathLength, IPrinter printer)
    {
        var paths = new List<List<(int x, int y)>>();
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };


        for (int walls = 0; walls <= 20; walls++)
        {
            BFSWithWallRemoval(grid, start, end, walls);
        }

        return paths;

        List<(int x, int y)> BFSWithWallRemoval(char[,] grid, (int x, int y) start, (int x, int y) end, int maxWalls)
        {
            var queue = new Queue<((int x, int y) pos, int dist, int walls, List<(int x, int y)> removedWalls)>();
            var visited = new Dictionary<((int x, int y) pos, int walls), int>();

            queue.Enqueue((start, 0, 0, new List<(int x, int y)>()));
            visited[(start, 0)] = 0;

            while (queue.Count > 0)
            {
                var (current, dist, walls, removedWalls) = queue.Dequeue();

                if (current == end)
                {
                    if (dist <= targetPathLength)
                    {
                        paths.Add(new (removedWalls));
                    }
                    continue;
                }

                foreach (var direction in directions)
                {
                    var next = (x: current.x + direction.x, y: current.y + direction.y);
                    if (next.x >= 0 && next.x < grid.GetLength(1) && next.y >= 0 && next.y < grid.GetLength(0))
                    {
                        int newWalls = walls + (grid[next.y, next.x] == '#' ? 1 : 0);
                        if (newWalls > maxWalls) continue;

                        int newDist = dist + 1;
                        var nextState = (next, newWalls);
                        if (!visited.ContainsKey(nextState) || visited[nextState] > newDist)
                        {
                            visited[nextState] = newDist;
                            var newRemovedWalls = new List<(int x, int y)>(removedWalls);
                            if (grid[next.y, next.x] == '#')
                            {
                                newRemovedWalls.Add(next);
                            }
                            queue.Enqueue((next, newDist, newWalls, newRemovedWalls));
                        }
                    }
                }
            }

            return new List<(int x, int y)>();
        }

    }

}
