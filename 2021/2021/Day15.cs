namespace Advent2021;
public class Day15
{
    static int[] _dx = { -1, 0, 1, 0 };
    static int[] _dy = { 0, 1, 0, -1 };

    public static long CalculateRisk(string filename)
    {
        var matrix = GetMatrix(filename);
        return DijkstrasAlgorithm(matrix, 0, 0);
    }

    public static int[,] GetMatrix(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new int[lines.Max(_ => _.Length), lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            var x = lines[row];
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[col, row] = int.Parse(lines[row][col].ToString());
            }
        }
        return result;
    }

    public static int CalculateRisk2(string filename)
    {
        var matrix = GetMatrix(filename);
        var largerMatrix = RepeatMatrix(matrix);
        return DijkstrasAlgorithm(largerMatrix, 0, 0);

    }

    public static int[,] RepeatMatrix(int[,] matrix)
    {
        var newMatrix = new int[matrix.GetLength(0) * 5, matrix.GetLength(1) * 5];
        for (int row = 0; row < newMatrix.GetLength(1); row++)//right
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                if (row < matrix.GetLength(1) && col < matrix.GetLength(0))
                {
                    newMatrix[col, row] = matrix[col, row];
                }

            }
        }
        //Down
        var startRow = matrix.GetLength(1);
        var copyRow = 0;
        for (int row = startRow; row < newMatrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                var val = newMatrix[col, copyRow];
                newMatrix[col, row] = val + 1 > 9 ? 1 : val + 1;
            }
            copyRow++;
        }
        //Right
        var startColumn = matrix.GetLength(0);
        var copyColumn = 0;
        for (int col = startColumn; col < newMatrix.GetLength(1); col++)
        {
            for (int row = 0; row < newMatrix.GetLength(0); row++)
            {
                var val = newMatrix[copyColumn, row];
                newMatrix[col, row] = val + 1 > 9 ? 1 : val + 1;
            }
            copyColumn++;
        }
        return newMatrix;
    }

    public static int DijkstrasAlgorithm(int[,] matrix, int startx, int starty)
    {
        var distances = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                distances[x, y] = int.MaxValue;
            }
        }
        distances[startx, starty] = 0;
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((startx, starty));
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                var (nextx, nexty) = (current.x + _dx[i], current.y + _dy[i]);
                if (nextx < 0 || nextx >= matrix.GetLength(0) || nexty < 0 || nexty >= matrix.GetLength(1))
                {
                    continue;
                }
                if (distances[nextx, nexty] > distances[current.x, current.y] + matrix[nextx, nexty])
                {
                    distances[nextx, nexty] = distances[current.x, current.y] + matrix[nextx, nexty];
                    queue.Enqueue((nextx, nexty));
                }
            }
        }
        return distances[distances.GetLength(0) - 1, distances.GetLength(1) - 1];
    }
}

