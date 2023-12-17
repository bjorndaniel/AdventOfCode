namespace AoC2023;
public class Day17
{
    public static int[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new int[lines[0].Length, lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[col, row] = int.Parse(lines[row][col].ToString());
            }
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day17.txt", "Day 17 part 1", 17)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var blocks = ParseInput(filename);
        var minHeatLoss = Dijkstra(blocks);
        return new SolutionResult(minHeatLoss.ToString());
    }

    [Solveable("2023/Puzzles/Day17.txt", "Day 17 part 2", 17)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var blocks = ParseInput(filename);
        var minHeatLoss = Dijkstra(blocks, true);
        return new SolutionResult(minHeatLoss.ToString());
    }

    private static int Dijkstra(int[,] blocks, bool isPart2 = false)
    {
        var directions = new (int dx, int dy)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        var visited = new HashSet<(int x, int y, int dx, int dy, int steps)>();
        var queue = new PriorityQueue<(int hl, int x, int y, int dx, int dy, int steps), int>();
        queue.Enqueue((0, 0, 0, 0, 0, 0), 0);
        while (queue.Count > 0)
        {
            var (hl, x, y, dx, dy, s) = queue.Dequeue();

            if (x == blocks.GetLength(0) - 1 && y == blocks.GetLength(1) - 1 && (!isPart2 || s >= 4))
            {
                return hl;
            }

            if (visited.Contains((x, y, dx, dy, s)))
            {
                continue;
            }
            visited.Add((x, y, dx, dy, s));
            if (isPart2)
            {
                if (s < 10 && (dx, dy) != (0, 0))
                {
                    var nx = x + dx;
                    var ny = y + dy;
                    if (nx >= 0 && ny >= 0 && nx < blocks.GetLength(0) && ny < blocks.GetLength(1))
                    {
                        var nHl = hl + blocks[nx, ny];
                        queue.Enqueue((nHl, nx, ny, dx, dy, s + 1), nHl);
                    }
                }
                if (s >= 4 || (dx, dy) == (0,0))
                {
                    foreach (var d in directions)
                    {
                        if (d != (dx, dy) && d != (-dx, -dy))
                        {
                            var nx = x + d.dx;
                            var ny = y + d.dy;
                            if (nx >= 0 && ny >= 0 && nx < blocks.GetLength(0) && ny < blocks.GetLength(1))
                            {
                                var nHl = hl + blocks[nx, ny];
                                queue.Enqueue((nHl, nx, ny, d.dx, d.dy, 1), nHl);
                            }
                        }
                    }

                }
            }
            else
            {
                if (s < 3 && (dx, dy) != (0, 0))
                {
                    var nx = x + dx;
                    var ny = y + dy;
                    if (nx >= 0 && ny >= 0 && nx < blocks.GetLength(0) && ny < blocks.GetLength(1))
                    {
                        var nHl = hl + blocks[nx, ny];
                        queue.Enqueue((nHl, nx, ny, dx, dy, s + 1), nHl);
                    }
                }
                foreach (var d in directions)
                {
                    if (d != (dx, dy) && d != (-dx, -dy))
                    {
                        var nx = x + d.dx;
                        var ny = y + d.dy;
                        if (nx >= 0 && ny >= 0 && nx < blocks.GetLength(0) && ny < blocks.GetLength(1))
                        {
                            var nHl = hl + blocks[nx, ny];
                            queue.Enqueue((nHl, nx, ny, d.dx, d.dy, 1), nHl);
                        }
                    }
                }
            }

        }
        return -1;
    }



}
