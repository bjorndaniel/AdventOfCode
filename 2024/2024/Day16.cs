namespace AoC2024;

public class Day16
{
    private static readonly (int dx, int dy, char dir)[] Directions = new[]
    {
            (0, -1, '^'), // Up
            (0, 1, 'v'),  // Down
            (-1, 0, '<'), // Left
            (1, 0, '>')   // Right
    };

    public static (char[,] grid, (int x, int y) start, (int x, int y) end) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var grid = new char[lines.Length, lines.First().Length];
        (int x, int y) start = (0, 0);
        (int x, int y) end = (0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines.First().Length; j++)
            {
                if (lines[i][j] == 'S')
                {
                    start = (j, i);
                    grid[i, j] = '>';
                    continue;
                }
                if (lines[i][j] == 'E')
                {
                    end = (j, i);
                }
                grid[i, j] = lines[i][j];
            }
        }
        return (grid, start, end);
    }

    [Solveable("2024/Puzzles/Day16.txt", "Day 16 part 1", 16)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (grid, start, end) = ParseInput(filename);
        var result = FindAllPathsWithScore(grid, start, end);
        return new SolutionResult(result.First().score.ToString());
    }

    [Solveable("2024/Puzzles/Day16.txt", "Day 16 part 2", 16)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (grid, start, end) = ParseInput(filename);
        var paths = FindAllPathsWithScore(grid, start, end);
        return new SolutionResult(paths.SelectMany(_ => _.path).Distinct().Count().ToString());
    }

    private static List<(List<(int x, int y)> path, int score)> FindAllPathsWithScore(char[,] grid, (int x, int y) start, (int x, int y) end)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var openSet = new PriorityQueue<(int x, int y, char dir, List<(int x, int y)> path, int score), int>();
        var gScore = new Dictionary<(int x, int y, char dir), int>();
        var allPaths = new List<(List<(int x, int y)> path, int score)>();

        foreach (var direction in Directions)
        {
            gScore[(start.x, start.y, direction.dir)] = int.MaxValue;
        }

        gScore[(start.x, start.y, '>')] = 0;
        openSet.Enqueue((start.x, start.y, '>', new List<(int x, int y)> { (start.x, start.y) }, 0), 0);

        while (openSet.Count > 0)
        {
            var (currentX, currentY, currentDir, currentPath, currentScore) = openSet.Dequeue();

            if ((currentX, currentY) == end)
            {
                allPaths.Add((new List<(int x, int y)>(currentPath), currentScore));
                continue;
            }

            foreach (var (dx, dy, newDir) in Directions)
            {
                var neighbor = (x: currentX + dx, y: currentY + dy, dir: newDir);
                if (neighbor.x < 0 || neighbor.x >= cols || neighbor.y < 0 || neighbor.y >= rows || grid[neighbor.y, neighbor.x] == '#')
                {
                    continue;
                }

                var tentativeGScore = currentScore + 1;
                if (currentDir != newDir)
                {
                    tentativeGScore += 1000;
                }

                if (tentativeGScore <= gScore.GetValueOrDefault((neighbor.x, neighbor.y, neighbor.dir), int.MaxValue))
                {
                    gScore[(neighbor.x, neighbor.y, neighbor.dir)] = tentativeGScore;
                    var newPath = new List<(int x, int y)>(currentPath) { (neighbor.x, neighbor.y) };
                    openSet.Enqueue((neighbor.x, neighbor.y, neighbor.dir, newPath, tentativeGScore), tentativeGScore + Helpers.ManhattanDistance((neighbor.x, neighbor.y), end) );
                }
            }
        }

        var lowestScore = allPaths.Min(p => p.score);
        return allPaths.Where(p => p.score == lowestScore).Select(p => (p.path, lowestScore)).ToList();
    }
}