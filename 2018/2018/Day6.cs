namespace AoC2018;
public class Day6
{
    public static List<(int x, int y)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(int x, int y)>();
        foreach(var l in lines)
        {
            result.Add((int.Parse(l.Split(',')[0]), int.Parse(l.Split(',')[1])));
        }
        return result;
    }

    [Solveable("2018/Puzzles/Day6.txt", "Day6 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var coords = ParseInput(filename);
        var minX = coords.Min(_ => _.x);
        var minY = coords.Min(_ => _.y);
        var maxX = coords.Max(_ => _.x);
        var maxY = coords.Max(_ => _.y);
        var grid = new int[maxX + 1, maxY + 1];
        var areas = new int[coords.Count];
        var isInfinite = new bool[coords.Count];
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var minDistance = int.MaxValue;
                var minIndex = -1;

                for (var i = 0; i < coords.Count; i++)
                {
                    var distance = Helpers.ManhattanDistance((x, y) , coords[i]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minIndex = i;
                    }
                    else if (distance == minDistance)
                    {
                        minIndex = -1;
                    }
                }

                grid[x, y] = minIndex;

                if (minIndex != -1)
                {
                    areas[minIndex]++;
                    if (x == minX || x == maxX || y == minY || y == maxY)
                    {
                        isInfinite[minIndex] = true;
                    }
                }
            }
        }

        var maxArea = 0;
        for (var i = 0; i < coords.Count; i++)
        {
            if (!isInfinite[i] && areas[i] > maxArea)
            {
                maxArea = areas[i];
            }
        }

        return new SolutionResult(maxArea.ToString());
    }

    [Solveable("2018/Puzzles/Day6.txt", "Day6 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var mindistance = 10000;
        if (filename.Contains("test"))
        {
            mindistance = 32;
        }
        var coords = ParseInput(filename);
        var minX = coords.Min(_ => _.x);
        var minY = coords.Min(_ => _.y);
        var maxX = coords.Max(_ => _.x);
        var maxY = coords.Max(_ => _.y);
        var grid = new int[maxX + 1, maxY + 1];
        var areas = new int[coords.Count];
        var isInfinite = new bool[coords.Count];
        var regionSize = 0;
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var totalDistance = 0;
                for (var i = 0; i < coords.Count; i++)
                {
                    var distance = Helpers.ManhattanDistance((x, y) , coords[i]);
                    totalDistance += distance;
                }

                if (totalDistance < mindistance)
                {
                    regionSize++;
                }
            }
        }

        return new SolutionResult(regionSize.ToString());
    }
}