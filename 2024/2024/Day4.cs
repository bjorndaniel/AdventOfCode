namespace AoC2024;
public class Day4
{
    private static readonly int[] RowDirections = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] ColDirections = { -1, 0, 1, -1, 1, -1, 0, 1 };
    private const string TARGET = "XMAS";

    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var grid = new char[lines.First().Length, lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines.First().Length; col++)
            {
                grid[row, col] = lines[row][col];
            }
        }
        return grid;
    }

    [Solveable("2024/Puzzles/Day4.txt", "Day 4 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var result = Count(grid);
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day4.txt", "Day 4 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var result = CountXs(grid);
        return new SolutionResult(result.ToString());
    }

    private static bool IsInsideGrid(int row, int col, int numRows, int numCols) =>
          row >= 0 && row < numRows && col >= 0 && col < numCols;

    private static bool FindWord(char[,] grid, int startRow, int startCol, int dirRow, int dirCol, string word)
    {
        var numRows = grid.GetLength(0);
        var numCols = grid.GetLength(1);
        for (int i = 0; i < word.Length; i++)
        {
            var newRow = startRow + i * dirRow;
            var newCol = startCol + i * dirCol;
            if (IsInsideGrid(newRow, newCol, numRows, numCols) is false || grid[newRow, newCol] != word[i])
            {
                return false;
            }
        }
        return true;

      
    }

    private static int Count(char[,] grid)
    {
        var count = 0;
        var numRows = grid.GetLength(0);
        var numCols = grid.GetLength(1);
        var visited = new HashSet<(int, int, int, int)>();

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                for (int dir = 0; dir < 8; dir++)
                {
                    var dirRow = RowDirections[dir];
                    var dirCol = ColDirections[dir];
                    if (FindWord(grid, row, col, dirRow, dirCol, TARGET) && visited.Contains((row, col, dirRow, dirCol)) is false)
                    {
                        count++;
                        visited.Add((row, col, dirRow, dirCol));
                    }
                }
            }
        }
        return count;
    }

    private static int CountXs(char[,] grid)
    {
        var result = 0;
        var grids = GetGrids(grid);
        foreach (var g in grids)
        {
            if (CheckDiagonals(g))
            {
                result++;
            }
        }

        return result;

        bool CheckDiagonals(char[,] grid)
        {
            var diag1 = $"{grid[0, 0]}{grid[1, 1]}{grid[2, 2]}";
            var diag2 = $"{grid[0, 2]}{grid[1, 1]}{grid[2, 0]}";
            return (diag1 == "SAM" || diag1 == "MAS") && (diag2 == "SAM" || diag2 == "MAS");
        }

        List<char[,]> GetGrids(char[,] grid)
        {
            var grids = new List<char[,]>();
            var numRows = grid.GetLength(0);
            var numCols = grid.GetLength(1);

            for (int row = 1; row < numRows - 1; row++)
            {
                for (int col = 1; col < numCols - 1; col++)
                {
                    if (grid[row, col] == 'A')
                    {
                        var subGrid = new char[3, 3];
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                subGrid[i + 1, j + 1] = grid[row + i, col + j];
                            }
                        }
                        grids.Add(subGrid);
                    }
                }
            }
            return grids;
        }
    }
}