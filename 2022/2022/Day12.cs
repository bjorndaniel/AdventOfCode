﻿namespace AoC2022;
public static class Day12
{
    private static int[] _dCol = { -1, 0, 1, 0 };
    private static int[] _dRow = { 0, 1, 0, -1 };

    public static (char[,], Point start, Point end) ParseInput(string filename)
    {
        var start = new Point(-1, -1);
        var end = new Point(-1, -1);
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
                    result[row, col] = 'z';
                }
                else
                {
                    result[row, col] = lines[row][col];
                }
            }
        }
        return (result, start, end);
    }
    public static (int result, int[,] visited) SolvePart1(string filename)
    {
        var (matrix, s, e) = ParseInput(filename);
        //matrix[0, 0] = 'a';
        return DijkstraWithObstacles(matrix, s, e);
    }

    public static (int result, int[,] visited) DijkstraWithObstacles(char[,] matrix, Point start, Point end)
    {
        var distances = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                distances[row, col] = int.MaxValue;
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
                var currentHeight = matrix[current.y, current.x] == 'S' ? 'a' : matrix[current.y, current.x];
                var canMove = matrix[nexty, nextx] - currentHeight < 2;
                if (distances[nexty, nextx] > distances[current.y, current.x] + 1 && canMove)
                {
                    distances[nexty, nextx] = distances[current.y, current.x] + 1;
                    queue.Enqueue((nexty, nextx));
                }
            }
        }
        return (distances[end.Y, end.X], distances);

    }

    public static int SolvePart2(string filename)
    {
        var (m, start, end) = ParseInput(filename);
        var starts = new List<Point> { start };
        starts.AddRange(GetPointsForStart(m));
        m[0, 0] = 'a';
        var currentLow = int.MaxValue;
        foreach (var s in starts)
        {
            var (r, _) = DijkstraWithObstacles(m, s, end);
            if (r < currentLow)
            {
                currentLow = r;
            }
        }
        return currentLow;

        static IEnumerable<Point> GetPointsForStart(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 'a')
                    {
                        yield return new Point(col, row);
                    }
                }
            }
        }


    }

}

