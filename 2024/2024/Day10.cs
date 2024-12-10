namespace AoC2024;
public class Day10
{
    public static int[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new int[lines.Length, lines.First().Length];
        
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[row, col] =  int.Parse((lines[row][col]).ToString());
            }
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var trails = ParseInput(filename);
        var rows = trails.GetLength(0);
        var cols = trails.GetLength(1);
        var totalPaths = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (trails[row, col] == 0)
                {
                    var visited = new bool[rows, cols];
                    var currentPath = new List<(int, int)>();
                    var paths = DFS(row, col, 0, visited, currentPath, rows, cols, trails);
                    var last = paths.Select(_ => _.Last()).Distinct().Count();
                    totalPaths += last;
                }
            }
        }
        return new SolutionResult(totalPaths.ToString());
    }

    [Solveable("2024/Puzzles/Day10.txt", "Day 10 part 2", 10)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var trails = ParseInput(filename);
        var rows = trails.GetLength(0);
        var cols = trails.GetLength(1);
        var totalPaths = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (trails[row, col] == 0)
                {
                    var visited = new bool[rows, cols];
                    var currentPath = new List<(int, int)>();
                    var paths = DFS(row, col, 0, visited, currentPath, rows, cols, trails);
                    totalPaths += paths.Count();
                }
            }
        }

        return new SolutionResult(totalPaths.ToString());
    }

    private static List<List<(int, int)>> DFS(int row, int col, int currentValue, bool[,] visited, List<(int, int)> currentPath, int rows, int cols, int[,] trails)
    {
        var result = new List<List<(int, int)>>();
        if (row < 0 || row >= rows || col < 0 || col >= cols || trails[row, col] != currentValue || visited[row, col])
        {
            return result;
        }
        currentPath.Add((row, col));
        visited[row, col] = true;
        if (currentValue == 9)
        {
            result.Add(new List<(int, int)>(currentPath));
        }
        else
        {
            var nextValue = currentValue + 1;

            result.AddRange(DFS(row + 1, col, nextValue, visited, currentPath, rows, cols, trails));
            result.AddRange(DFS(row - 1, col, nextValue, visited, currentPath, rows, cols, trails));
            result.AddRange(DFS(row, col + 1, nextValue, visited, currentPath, rows, cols, trails));
            result.AddRange(DFS(row, col - 1, nextValue, visited, currentPath, rows, cols, trails));
        }

        visited[row, col] = false;
        currentPath.RemoveAt(currentPath.Count - 1);

        return result;
    }
}
