namespace AoC2025;

public class Day9
{
    public static List<(int x, int y)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var tiles = new List<(int x, int y)>();
        foreach(var line in lines)
        {
            var parts = line.Split(",");
            tiles.Add((int.Parse(parts[0]), int.Parse(parts[1])));
        }
        return tiles;
    }

    [Solveable("2025/Puzzles/Day9.txt", "Day 9 part 1", 9)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var tiles = ParseInput(filename);

        var maxArea = 0L;
        for (int i = 0; i < tiles.Count; i++)
        {
            for (int j = i + 1; j < tiles.Count; j++)
            {
                var dx = Math.Abs(tiles[i].x - tiles[j].x) + 1;
                var dy = Math.Abs(tiles[i].y - tiles[j].y) + 1;
                long area = (long)dx * (long)dy;
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }

        return new SolutionResult(maxArea.ToString());
    }

    [Solveable("2025/Puzzles/Day9.txt", "Day 9 part 2", 9)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var reds = ParseInput(filename);
        var edges = new List<((int x, int y) a, (int x, int y) b)>();
        for (var i = 0; i < reds.Count; i++)
        {
            var a = reds[i];
            var b = reds[(i + 1) % reds.Count];
            edges.Add((a, b));
        }
        var allX = new HashSet<int>();
        var allY = new HashSet<int>();
        foreach (var r in reds)
        {
            allX.Add(r.x);
            allY.Add(r.y);
        }

        var sortedX = allX.OrderBy(x => x).ToList();
        var sortedY = allY.OrderBy(y => y).ToList();
        var xToIndex = sortedX.Select((x, i) => (x, i)).ToDictionary(p => p.x, p => p.i);
        var yToIndex = sortedY.Select((y, i) => (y, i)).ToDictionary(p => p.y, p => p.i);
        var width = sortedX.Count;
        var height = sortedY.Count;
        var allowedCompressed = new HashSet<(int xi, int yi)>();
        foreach (var e in edges)
        {
            var a = e.a; 
            var b = e.b;
            if (a.x == b.x)
            {
                var xi = xToIndex[a.x];
                var yi0 = yToIndex[Math.Min(a.y, b.y)];
                var yi1 = yToIndex[Math.Max(a.y, b.y)];
                for (var yi = yi0; yi <= yi1; yi++)
                {
                    allowedCompressed.Add((xi, yi));
                }
            }
            else
            {
                var yi = yToIndex[a.y];
                var xi0 = xToIndex[Math.Min(a.x, b.x)];
                var xi1 = xToIndex[Math.Max(a.x, b.x)];
                for (var xi = xi0; xi <= xi1; xi++)
                {
                    allowedCompressed.Add((xi, yi));
                }
            }
        }

        var verticals = new List<(int xi, int yi0, int yi1)>();
        foreach (var e in edges)
        {
            var a = e.a; 
            var b = e.b;
            if (a.x == b.x)
            {
                var xi = xToIndex[a.x];
                var yi0 = yToIndex[Math.Min(a.y, b.y)];
                var yi1 = yToIndex[Math.Max(a.y, b.y)];
                verticals.Add((xi, yi0, yi1));
            }
        }

        for (var yi = 0; yi < height; yi++)
        {
            var crossings = new List<int>();
            foreach (var v in verticals)
            {
                if (yi >= v.yi0 && yi < v.yi1)
                {
                    crossings.Add(v.xi);
                }
            }
            crossings.Sort();
            for (var k = 0; k + 1 < crossings.Count; k += 2)
            {
                var xi0 = crossings[k];
                var xi1 = crossings[k + 1];
                for (var xi = xi0; xi <= xi1; xi++)
                {
                    allowedCompressed.Add((xi, yi));
                }
            }
        }

        var prefix = new long[width + 1, height + 1];
        for (var xi = 1; xi <= width; xi++)
        {
            for (var yi = 1; yi <= height; yi++)
            {
                var val = allowedCompressed.Contains((xi - 1, yi - 1)) ? 1 : 0;
                prefix[xi, yi] = val + prefix[xi - 1, yi] + prefix[xi, yi - 1] - prefix[xi - 1, yi - 1];
            }
        }

        var maxArea = 0L;
        for (var i = 0; i < reds.Count; i++)
        {
            for (var j = i + 1; j < reds.Count; j++)
            {
                var a = reds[i];
                var b = reds[j];
                if (a.x == b.x || a.y == b.y)
                {
                    continue;
                }

                var x0 = Math.Min(a.x, b.x);
                var x1 = Math.Max(a.x, b.x);
                var y0 = Math.Min(a.y, b.y);
                var y1 = Math.Max(a.y, b.y);

                var xi0 = xToIndex[x0];
                var xi1 = xToIndex[x1];
                var yi0 = yToIndex[y0];
                var yi1 = yToIndex[y1];

                var expectedCount = (long)(xi1 - xi0 + 1) * (long)(yi1 - yi0 + 1);
                var actualCount = prefix[xi1 + 1, yi1 + 1] - prefix[xi0, yi1 + 1] - prefix[xi1 + 1, yi0] + prefix[xi0, yi0];

                if (actualCount == expectedCount)
                {
                    var area = (long)(x1 - x0 + 1) * (long)(y1 - y0 + 1);
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
        }

        return new SolutionResult(maxArea.ToString());
    }
}