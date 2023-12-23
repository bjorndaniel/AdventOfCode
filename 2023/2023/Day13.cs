namespace AoC2023;
public class Day13
{
    public static List<char[,]> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<char[,]>();
        var patterns = new List<List<string>>();
        List<string> currentPattern = [];
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                patterns.Add(currentPattern);
                currentPattern = [];
            }
            else
            {
                currentPattern.Add(line);
            }
        }
        patterns.Add(currentPattern);
        foreach (var pattern in patterns)
        {
            var grid = new char[pattern[0].Length, pattern.Count];
            for (var row = 0; row < grid.GetLength(1); row++)
            {
                for (var col = 0; col < grid.GetLength(0); col++)
                {
                    grid[col, row] = pattern[row][col];
                }
            }
            result.Add(grid);
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day13.txt", "Day 13 part 1", 13)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var patterns = ParseInput(filename);
        var sum = 0L;
        foreach (var p in patterns)
        {
            sum += GetReflection(p, (-1, -1)).sum;
        }
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2023/Puzzles/Day13.txt", "Day 13 part 2", 13)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var patterns = ParseInput(filename);
        var sum = 0L;
        foreach (var p in patterns)
        {
            var (col, row, val) = GetReflection(p, (-1, -1));
            sum += GetReflection(p, (col, row), true).sum;
        }
        return new SolutionResult(sum.ToString());
    }

    private static (int col, int row, int sum) GetReflection(char[,] pattern, (int originalCol, int originalRow) compare, bool isPart2 = false)
    {
        for (int row = 0; row < pattern.GetLength(1) - 1; row++)
        {
            var rowsEqual = Helpers.GetRow(pattern, row).Equals(Helpers.GetRow(pattern, row + 1));
            if (isPart2)
            {
                rowsEqual = row != compare.originalRow && (rowsEqual || CountDifferingPositions(Helpers.GetRow(pattern, row), Helpers.GetRow(pattern, row + 1)) == 1);
            }
            if (rowsEqual)
            {
                var match = true;
                var below = row == 0 ? row : row - 1;
                var above = row == 0 ? row + 1 : row + 2;
                while (match && below >= 0 && above < pattern.GetLength(1))
                {
                    match = match && Helpers.GetRow(pattern, below).Equals(Helpers.GetRow(pattern, above));
                    if (isPart2 && row != compare.originalRow)
                    {
                        var aboveLine = Helpers.GetRow(pattern, above);
                        var belowLine = Helpers.GetRow(pattern, below);
                        if (CountDifferingPositions(aboveLine, belowLine) <= 1)
                        {
                            match = true;
                        }
                    }
                    below--;
                    above++;
                }
                if (match)
                {
                    return (-1, row, (row + 1) * 100);
                }
            }
        }

        for (int col = 0; col < pattern.GetLength(0) - 1; col++)
        {
            var colsEqual = Helpers.GetColumn(pattern, col).Equals(Helpers.GetColumn(pattern, col + 1));
            if (isPart2)
            {
                colsEqual = col != compare.originalCol && (colsEqual || CountDifferingPositions(Helpers.GetColumn(pattern, col), Helpers.GetColumn(pattern, col + 1)) == 1);
            }
            if (colsEqual)
            {
                var match = true;
                var left = col - 1;
                var right = col + 2;
                while (match && left >= 0 && right < pattern.GetLength(0))
                {
                    match = match && Helpers.GetColumn(pattern, left).Equals(Helpers.GetColumn(pattern, right));
                    if (isPart2 && col != compare.originalCol)
                    {
                        var leftLine = Helpers.GetColumn(pattern, left);
                        var rightLine = Helpers.GetColumn(pattern, right);
                        if (CountDifferingPositions(rightLine, leftLine) <= 1)
                        {
                            match = true;
                        }
                    }
                    left--;
                    right++;
                }
                if (match)
                {
                    return (col, -1, col + 1);
                }
            }
        }
        return (-1, -1, -1);
    }

    private static int CountDifferingPositions(string str1, string str2)
    {
        if (str1.Length != str2.Length)
        {
            throw new ArgumentException("Strings must be of equal length");
        }

        return str1.Zip(str2, (c1, c2) => c1 == c2 ? 0 : 1).Sum();
    }
}