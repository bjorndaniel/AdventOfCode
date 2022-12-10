namespace AoC2022;
public static class Day8
{
    public static int[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new int[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                result[i, j] = line[j] - '0';
            }
        }
        return result;
    }

    public static int SolvePart1(string filename)
    {
        var input = ParseInput(filename);
        var edge = ((input.GetLength(0) * 2) + (input.GetLength(1) * 2)) - 4;
        var inside = GetTrees(input);
        return edge + inside.Count();
    }

    public static int SolvePart2(string filename)
    {
        var input = ParseInput(filename);
        var trees = GetTrees(input);
        return GetMostVisibleTrees(input, trees);
    }

    private static int GetMostVisibleTrees(int[,] matrix, Dictionary<(int row, int col), int> trees)
    {
        var maxScore = 0;
        foreach(var tree in trees)
        {
            var (left, right) = GetLineOfSightScoreHorizontal(matrix, tree.Key);
            var (top, bottom) = GetLineOfSightScoreVertical(matrix, tree.Key);
            var score = left * right * top * bottom;
            maxScore = score > maxScore ? score : maxScore;
        }
        return maxScore;
    }

    private static (int top, int bottom) GetLineOfSightScoreVertical(int[,] matrix, (int row, int col) key)
    {
        var top = key.row - 1;
        var bottom = key.row + 1;
        var topTrees = 0;
        var bottomTrees = 0;
        while (top >= 0)
        {
            if (matrix[top, key.col] >= matrix[key.row, key.col])
            {
                topTrees++;
                break;
            }
            topTrees++;
            top--;
        }
        while (bottom < matrix.GetLength(0))
        {
            if (matrix[bottom, key.col] >= matrix[key.row, key.col])
            {
                bottomTrees++;
                break;
            }
            bottomTrees++;
            bottom++;
        }
        return (topTrees, bottomTrees);
    }

    private static (int left, int right) GetLineOfSightScoreHorizontal(int[,] matrix, (int row, int col) key)
    {
        var left = key.col - 1;
        var right = key.col + 1;
        var leftTrees = 0;
        var rightTrees = 0;
        while (left >= 0)
        {
            if (matrix[key.row, left] >= matrix[key.row, key.col])
            {
                leftTrees++;
                break;
            }
            leftTrees++;
            left--;
        }

        while (right < matrix.GetLength(1))
        {
            if (matrix[key.row, right] >= matrix[key.row, key.col])
            {
                rightTrees++;
                break;
            }
            rightTrees++;
            right++;
        }

        return (leftTrees, rightTrees);
    }

    private static Dictionary<(int row, int col), int> GetTrees(int[,] matrix)
    {
        var inside = new Dictionary<(int row, int col), int>();
        for (var row = 1; row < matrix.GetLength(0) - 1; row++)
        {
            for (int col = 1; col < matrix.GetLength(1) - 1; col++)
            {
                if (HasLineOfSightHorizontal(matrix, new Point(col, row)) || HasLineOfSightVertical(matrix, new Point(col, row)))
                {
                    if (!inside.ContainsKey((row, col)))
                    {
                        inside.Add((row, col), matrix[row, col]);
                    }
                }
            }
        }
        return inside;
    }

    private static bool HasLineOfSightHorizontal(int[,] matrix, Point p)
    {
        var row = p.Y;
        var col = p.X;
        var left = col - 1;
        var right = col + 1;
        var visibleLeft = true;
        var visibleRight = true;
        while (left >= 0)
        {
            if (matrix[row, left] >= matrix[row, col])
            {
                visibleLeft = false;
                break;
            }
            left--;
        }

        while (right < matrix.GetLength(1))
        {
            if (matrix[row, right] >= matrix[row, col])
            {
                visibleRight = false;
                break;
            }
            right++;
        }
        return visibleLeft || visibleRight;
    }

    private static bool HasLineOfSightVertical(int[,] matrix, Point p)
    {
        var row = p.Y;
        var col = p.X;
        var top = row - 1;
        var bottom = row + 1;
        var visibleTop = true;
        var visibleBottom = true;
        while (top >= 0)
        {
            if (matrix[top, col] >= matrix[row, col])
            {
                visibleTop = false;
                break;
            }
            top--;
        }
        while (bottom < matrix.GetLength(0))
        {
            if (matrix[bottom, col] >= matrix[row, col])
            {
                visibleBottom = false;
                break;
            }
            bottom++;
        }
        return visibleTop || visibleBottom;
    }

}