namespace AoC2023;
public class Day11
{
    private static readonly int[] dx = { -1, 0, 1, 0 };
    private static readonly int[] dy = { 0, 1, 0, -1 };

    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var expandedLines = new List<string>();
        foreach (var line in lines)
        {
            if (line.Contains("#"))
            {
                expandedLines.Add(line);
            }
            else
            {
                expandedLines.Add(new string(line));
                expandedLines.Add(line);
            }
        }
        var columnsToCopy = new List<int>();
        for (int col = 0; col < expandedLines[0].Length; col++)
        {
            var allDots = true;
            for (int row = 0; row < expandedLines.Count; row++)
            {
                if (expandedLines[row][col] != '.')
                {
                    allDots = false;
                    break;
                }
            }
            if (allDots)
            {
                columnsToCopy.Add(col);
            }
        }
        var counter = 1;
        foreach (var col in columnsToCopy)
        {
            for (int row = 0; row < expandedLines.Count; row++)
            {
                expandedLines[row] = expandedLines[row].Insert(col + counter, ".");
            }
            counter++;
        }
        var result = new char[expandedLines.First().Length, expandedLines.Count()];
        for (int row = 0; row < expandedLines.Count(); row++)
        {
            for (int col = 0; col < expandedLines.First().Length; col++)
            {
                result[col, row] = expandedLines[row][col];
            }
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var matrix = ParseInput(filename);
        var m = matrix.GetLength(0);
        var n = matrix.GetLength(1);
        var total = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] == '#')
                {
                    var paths = ShortestPaths(matrix, i, j);
                    total += paths.Sum(_ => _.distance);
                }
            }
        }
        return new SolutionResult((total / 2).ToString());
    }

    [Solveable("2023/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        var expandRows = new List<int>();
        var expandCols = new List<int>();
        var galaxies = new List<(int x, int y)>();
        for (int row = 0; row < lines.Length; row++)
        {
            if (!lines[row].Contains("#"))
            {
                expandRows.Add(row);
            }
        }
        for (int col = 0; col < lines[0].Length; col++)
        {
            var allDots = true;
            for (int row = 0; row < lines[0].Length; row++)
            {
                if (lines[row][col] != '.')
                {
                    galaxies.Add((col, row));
                    allDots = false;
                }
            }
            if (allDots)
            {
                expandCols.Add(col);
            }
        }
        foreach (var row in expandRows)
        {
            for (var i = 0; i < galaxies.Count; i++)
            {

                if (galaxies[i].y > row)
                {
                    galaxies[i] = (galaxies[i].x, galaxies[i].y + 1);
                }
            }
        }
        foreach (var col in expandCols)
        {
            for (var i = 0; i < galaxies.Count; i++)
            {
                if (galaxies[i].x > col)
                {
                    galaxies[i] = (galaxies[i].x + 1, galaxies[i].y);
                }
            }
        }

        var matrix = Helpers.CreateMatrix(galaxies);
        printer.PrintMatrixXY(matrix);
        printer.Flush();

        var pairs = new List<((int, int), (int,int), int)>();
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                pairs.Add((galaxies[i], galaxies[j], Helpers.ManhattanDistance(galaxies[i], galaxies[j])));
            }
        }

        //var total = 0L;
        //var dist = new Dictionary<(int x, int y), long>();
        //var temp = new List<Dictionary<(int x, int y), long>>();    
        //foreach (var galaxy in galaxies)
        //{
        //    var distances = Dijkstra(galaxies, galaxy);
        //    temp.Add(distances.Where(_ => galaxies.Contains(_.Key) && _.Key != galaxy).ToDictionary());
        //    //foreach(var distance in distances.Where(_ =>  galaxies.Contains(_.Key) && _.Key != galaxy))
        //    //{
        //    //    if(dist.ContainsKey(distance.Key))
        //    //    {
        //    //        dist[distance.Key]  = Math.Min(dist[distance.Key], distance.Value);
        //    //    }
        //    //    else
        //    //    {
        //    //        dist[distance.Key] = distance.Value;
        //    //    }
        //    //}
        //   //var distances = Dijkstra(galaxies, galaxy);
        //   //dist[galaxy] = distances.Where(_ => _.Value != long.MaxValue).Sum(_ => _.Value);
        //   //total += distances.Where(_ => _.Value != long.MaxValue).Sum(_ => _.Value);
        //}
        printer.Print(galaxies.Count.ToString());
        printer.Flush();
        return new SolutionResult(pairs.Sum(_ => _.Item3).ToString());
    }

    private static List<(int distance, (int x, int y))> ShortestPaths(char[,] matrix, int x, int y)
    {
        var m = matrix.GetLength(0);
        var n = matrix.GetLength(1);
        var dist = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                dist[i, j] = int.MaxValue;
            }
        }

        var queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(Tuple.Create(x, y));
        dist[x, y] = 0;

        var shortestPaths = new List<(int distance, (int x, int y))>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                int nx = current.Item1 + dx[i];
                int ny = current.Item2 + dy[i];

                if (nx >= 0 && nx < m && ny >= 0 && ny < n && dist[nx, ny] == int.MaxValue)
                {
                    dist[nx, ny] = dist[current.Item1, current.Item2] + 1;
                    if (matrix[nx, ny] == '#')
                    {
                        shortestPaths.Add((dist[nx, ny], (nx, ny)));
                    }

                    queue.Enqueue(Tuple.Create(nx, ny));
                }
            }
        }
        return shortestPaths;
    }

    private static Dictionary<(int x, int y), long> Dijkstra(List<(int x, int y)> coordinates, (int x, int y) source)
    {
        var distances = new Dictionary<(int x, int y), long>();
        for(int row = 0;row<coordinates.Max(_ => _.y); row++)
        {
            for(int col = 0;col<coordinates.Max(_ => _.x); col++)
            {
                distances[(col, row)] = long.MaxValue;
            }
        }
        distances[source] = 0;
        var maxVals = (coordinates.Max(_ => _.x), coordinates.Max(_ => _.y));
        
        var queue = new PriorityQueue<(long distance, (int x, int y) coordinate)>();
        queue.Enqueue((0, source));

        while (queue.Count() > 0)
        {
            var (currentDistance, currentCoordinate) = queue.Dequeue();

            if (currentDistance > distances[currentCoordinate])
            {
                continue;
            }

            foreach (var neighbor in GetNeighbors(currentCoordinate, maxVals))
            {
                var newDistance = currentDistance + GetDistance(currentCoordinate, neighbor);
                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    queue.Enqueue((newDistance, neighbor));
                }
            }
        }

        return distances;
    }

    private static long GetDistance((int x, int y) currentCoordinate, (int x, int y) neighbor)
    {
        int dx = Math.Abs(currentCoordinate.x - neighbor.x);
        int dy = Math.Abs(currentCoordinate.y - neighbor.y);
        return dx + dy;
    }

    private static List<(int x, int y)> GetNeighbors((int x, int y) coordinate, (int maxX, int maxY) maxVals)
    {
        var neighbors = new List<(int x, int y)>
        {
            (coordinate.x - 1, coordinate.y),
            (coordinate.x + 1, coordinate.y),
            (coordinate.x, coordinate.y - 1),
            (coordinate.x, coordinate.y + 1)
        };

        return neighbors.Where(_ => _.x >= 0 && _.x < maxVals.maxX && _.y >= 0 && _.y < maxVals.maxY).ToList();
    }
}