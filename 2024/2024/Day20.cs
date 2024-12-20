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
        var result = paths.Where(_ => shortest - _ == timeSaved);
        return new SolutionResult(result.Count().ToString());
    }

    [Solveable("2024/Puzzles/Day20.txt", "Day 20 part 2", 64)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
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

}