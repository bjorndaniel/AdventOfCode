namespace AoC2023;
public class Day10
{
    public static (char[,] pipes, (int x, int y) start) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.First().Length, lines.Count()];
        var start = (0, 0);
        for (int i = 0; i < lines.Count(); i++)
        {
            for (int j = 0; j < lines.First().Length; j++)
            {
                result[j, i] = lines[i][j];
                if (result[j, i] == 'S')
                {
                    start = (j, i);
                }
            }
        }
        return (result, start);
    }

    [Solveable("2023/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (pipes, start) = ParseInput(filename);
        var (pipeConnections, sConnections) = GetConnections(pipes, start);
        pipeConnections.Add('S', new List<(int dx, int dy)> { sConnections.First() });
        var loops = FindLoop(pipes, start, pipeConnections);
        return new SolutionResult((loops.Count() / 2).ToString());
    }

    [Solveable("2023/Puzzles/Day10.txt", "Day 10 part 2", 10)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (pipes, start) = ParseInput(filename);
        var (pipeConnections, sConnections) = GetConnections(pipes, start);
        pipeConnections.Add('S', new List<(int dx, int dy)> { sConnections.First() });
        var loop = FindLoop(pipes, start, pipeConnections);
        return new SolutionResult(CalculatePolygonArea(loop).ToString());

    }

    private static int CalculatePolygonArea(List<(int x, int y)> vertices)
    {
        var n = vertices.Count; // Get the number of vertices in the polygon
        var area = 0; // Initialize the area variable
        for (int i = 0; i < n; i++)
        {
            var j = (i + 1) % n; // Get the index of the next vertex (wraps around to the first vertex if necessary)
            area += (vertices[i].x * vertices[j].y) - (vertices[j].x * vertices[i].y); // Calculate the signed area of the triangle formed by the current vertex, the next vertex, and the origin
        }
        return Math.Abs(area / 2) - (vertices.Count() / 2) + 1; // Calculate the absolute value of the area divided by 2, subtract half the number of vertices, and add 1 to get the final area
    }

    private static List<(int x, int y)> FindLoop(char[,] pipes, (int x, int y) start, Dictionary<char, List<(int dx, int dy)>> pipeConnections)
    {
        var queue = new Queue<(int x, int y)>();
        var visited = new List<(int x, int y)>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var currentTile = queue.Dequeue();
            visited.Add(currentTile);
            var possibleMovements = pipeConnections[pipes[currentTile.x, currentTile.y]];

            foreach (var movement in possibleMovements)
            {
                var nextTile = (currentTile.x + movement.dx, currentTile.y + movement.dy);

                if (nextTile == start && visited.Count() > 2)
                {
                    return visited;
                }
                if (!visited.Contains(nextTile))
                {
                    queue.Enqueue(nextTile);
                }
            }
        }
        return new List<(int x, int y)>();
    }

    private static (Dictionary<char, List<(int dx, int dy)>> pipeConnections, List<(int dx, int dy)> sConnections) GetConnections(char[,] pipes, (int x, int y) start)
    {
        var pipeConnections = new Dictionary<char, List<(int dx, int dy)>>()
        {
            { '|', new List<(int dx, int dy)> { (0, -1), (0, 1) } },
            { '-', new List<(int dx, int dy)> { (-1, 0), (1, 0) } },
            { 'L', new List<(int dx, int dy)> { (0, -1), (1, 0) } },
            { 'J', new List<(int dx, int dy)> { (0, -1), (-1, 0) } },
            { '7', new List<(int dx, int dy)> { (0, 1), (-1, 0) } },
            { 'F', new List<(int dx, int dy)> { (0, 1), (1, 0) } },
            { '.', new List<(int dx, int dy)>() },
        };
        var sDirections = new List<(int dx, int dy)>();
        if (start.x > 0 && (pipes[start.x - 1, start.y] == '-' || pipes[start.x - 1, start.y] == 'L' || pipes[start.x - 1, start.y] == 'F'))
        {
            sDirections.Add((-1, 0));
        }
        if (start.y > 0 && (pipes[start.x, start.y - 1] == '|' || pipes[start.x, start.y - 1] == 'F' || pipes[start.x, start.y - 1] == '7'))
        {
            sDirections.Add((0, -1));
        }
        if (start.y < pipes.GetLength(1) && (pipes[start.x, start.y + 1] == '|' || pipes[start.x, start.y - 1] == 'J' || pipes[start.x - 1, start.y] == 'L'))
        {
            sDirections.Add((0, 1));
        }
        if (start.x < pipes.GetLength(0) && (pipes[start.x + 1, start.y] == '-' || pipes[start.x + 1, start.y] == 'J' || pipes[start.x + 1, start.y] == '7'))
        {
            sDirections.Add((1, 0));
        }
        return (pipeConnections, sDirections);
    }

}