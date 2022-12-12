namespace AoC2022;
public static class Day12
{
    private static int[] _dCol = { -1, 0, 1, 0 };
    private static int[] _dRow = { 0, 1, 0, -1 };

    public static (char[,], Point start, Point end) ParseInput(string filename)
    {
        Point start = new Point(-1, -1);
        Point end = new Point(-1, -1);
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.Length, lines[0].Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                if (lines[row][col] == 'S')
                {
                    start = new Point(col, row);
                }
                if (lines[row][col] == 'E')
                {
                    end = new Point(col, row);
                }
                result[row, col] = lines[row][col];
            }
        }
        return (result, start, end);
    }
    public static (int result, int[,] visited) SolvePart1(string filename)
    {
        var (matrix, s, e) = ParseInput(filename);
        return DijkstraWithObstacles(matrix, s, e);
    }

    public static (int result, int[,] visited) DijkstraWithObstacles(char[,] matrix, Point start, Point end)
    {
        var distances = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                distances[y, x] = int.MaxValue;
            }
        }
        distances[start.Y, start.X] = 0;
        var queue = new Queue<(int y, int x)>();
        queue.Enqueue((start.Y, start.X));
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.y == end.Y && current.x == end.X)
            {
                return (distances[current.y, current.x], distances);
            }
            for (int i = 0; i < 4; i++)
            {
                var (nextx, nexty) = (current.x + _dCol[i], current.y + _dRow[i]);
                if (nextx < 0 || nextx >= matrix.GetLength(1) || nexty < 0 || nexty >= matrix.GetLength(0))
                {
                    continue;
                }
                var canMove = (current.x == start.X && current.y == start.Y) || matrix[nexty, nextx] - matrix[current.y, current.x] < 2;
                if (distances[nexty, nextx] > distances[current.y, current.x] + 1 && canMove)
                {
                    distances[nexty, nextx] = distances[current.y, current.x] + 1;
                    queue.Enqueue((nexty, nextx));
                }
            }
        }
        return (distances[end.Y, end.X], distances);

    }

}

