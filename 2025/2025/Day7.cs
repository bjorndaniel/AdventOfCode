namespace AoC2025;

public class Day7
{
    public static (char[,] manifold, (int x, int y) start)  ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var width = lines.First().Length;
        var height = lines.Length;
        var result = new char[width, height];
        var start = (x: 0, y: 0);
        for (var y = 0; y < height; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                result[x, y] = line[x];
                if (line[x] == 'S')
                {
                    start = (x, y);
                }
            }
        }
        return (result, start);
    }

    [Solveable("2025/Puzzles/Day7.txt", "Day 7 part 1", 7)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (manifold, start) = ParseInput(filename);
        var width = manifold.GetLength(0);
        var height = manifold.GetLength(1);
        var currentBeams = new HashSet<(int x, int y)> { (start.x, start.y) };
        var nextBeams = new HashSet<(int x, int y)>();
        var splits = 0;

        while (currentBeams.Count > 0)
        {
            nextBeams.Clear();

            foreach (var (bx, by) in currentBeams)
            {
                var ny = by + 1; // move downward 1 step
                if (ny >= height)
                {
                    continue;
                }
                var cell = manifold[bx, ny];
                if (cell == '^')
                {
                    splits++;
                    var leftX = bx - 1;
                    var rightX = bx + 1;
                    if (leftX >= 0 && leftX < width)
                    {
                        nextBeams.Add((leftX, ny));
                    }
                    if (rightX >= 0 && rightX < width)
                    {
                        nextBeams.Add((rightX, ny));
                    }
                }
                else
                {
                    nextBeams.Add((bx, ny));
                }
            }
            currentBeams = nextBeams.ToHashSet();
        }

        return new SolutionResult(splits.ToString());
    }

    [Solveable("2025/Puzzles/Day7.txt", "Day 7 part 2", 7)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (manifold, start) = ParseInput(filename);
        var width = manifold.GetLength(0);
        var height = manifold.GetLength(1);
        var currentCounts = new long[width];
        var nextCounts = new long[width];
        var row = start.y;
        currentCounts[start.x] = 1;
        var terminalPaths = 0L;

        while (true)
        {
            var nextRow = row + 1;
            if (nextRow >= height)
            {
                for (var x = 0; x < width; x++)
                {
                    terminalPaths += currentCounts[x];
                }
                break;
            }

            Array.Clear(nextCounts);
            for (var x = 0; x < width; x++)
            {
                var count = currentCounts[x];
                if (count == 0)
                {
                    continue;
                }
                var cell = manifold[x, nextRow];
                if (cell == '^')
                {
                    var leftX = x - 1;
                    var rightX = x + 1;
                    if (leftX >= 0)
                    {
                        nextCounts[leftX] += count;
                    }
                    if (rightX < width)
                    {
                        nextCounts[rightX] += count;
                    }
                }
                else
                {
                    nextCounts[x] += count;
                }
            }
            var swap = currentCounts;
            currentCounts = nextCounts;
            nextCounts = swap;
            row = nextRow;
        }

        return new SolutionResult(terminalPaths.ToString());
    }


}