namespace AoC2024;
public class Day18
{
    public static List<(int x, int y)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(int x, int y)>();
        foreach (var l in lines)
        {
            var coords = l.Split(',');
            result.Add((int.Parse(coords[0]), int.Parse(coords[1])));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day18.txt", "Day 18 part 1", 18)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var bytes = ParseInput(filename);
        var isTest = filename.Contains("test");
        var grid = isTest ? new char[7, 7] : new char[71, 71];
        var result = 0;
        var obstacles = isTest ? bytes.Take(12) : bytes.Take(1024);
        for (int row = 0; row < grid.GetLength(1); row++)
        {
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                grid[col, row] = '.';
            }
        }
        foreach (var (x, y) in obstacles)
        {
            grid[x, y] = '#';
        }

        return new SolutionResult(BFS(grid, result).ToString());
    }

    [Solveable("2024/Puzzles/Day18.txt", "Day 18 part 2", 18)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var bytes = ParseInput(filename);
        var isTest = filename.Contains("test");
        var grid = isTest ? new char[7, 7] : new char[71, 71];
        var obstacles = isTest ? bytes.Take(12).ToList() : bytes.Take(1024).ToList();
        for (int row = 0; row < grid.GetLength(1); row++)
        {
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                grid[col, row] = '.';
            }
        }

        foreach (var (x, y) in obstacles)
        {
            grid[x, y] = '#';
        }

        foreach (var (x, y) in bytes.Skip(obstacles.Count))
        {
            grid[x, y] = '#';
            if (BFS(grid, 0) == 0)
            {
                return new SolutionResult($"{x},{y}");
            }
        }

        return new SolutionResult("No such coordinate found");
    }

    private static int BFS(char[,] grid, int result)
    {
        var directions = new (int x, int y)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var queue = new Queue<((int x, int y) pos, int dist)>();
        var visited = new HashSet<(int x, int y)>();
        queue.Enqueue(((0, 0), 0));
        visited.Add((0, 0));

        while (queue.Count > 0)
        {
            var ((x, y), dist) = queue.Dequeue();
            if (x == grid.GetLength(0) - 1 && y == grid.GetLength(1) - 1)
            {
                result = dist;
                break;
            }

            foreach (var (dx, dy) in directions)
            {
                var newX = x + dx;
                var newY = y + dy;
                if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1) && grid[newX, newY] == '.' && !visited.Contains((newX, newY)))
                {
                    queue.Enqueue(((newX, newY), dist + 1));
                    visited.Add((newX, newY));
                }
            }
        }
        return result;
    }
}