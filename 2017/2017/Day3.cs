namespace AoC2017;

public class Day3
{
    public static int ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return int.Parse(lines.First());
    }

    [Solveable("2017/Puzzles/Day3.txt", "Day 3 part 1")]
    public static SolutionResult SolvePart1(string fileName, IPrinter printer)
    {
        var input = ParseInput(fileName);
        var (x, y) = SpiralCoords(input);
        var distance = CalculateManhattanDistance(0, 0, x, y);
        return new SolutionResult(distance.ToString());
    }

    [Solveable("2017/Puzzles/Day3.txt", "Day 3 part 2")]
    public static SolutionResult SolvePart2(string fileName, IPrinter printer)
    {
        var input = ParseInput(fileName);
        //var (x, y) = SpiralCoords(input);
        var current = 0;
        var position = 2;
        var valuePositions = new Dictionary<(int x, int y), int> { { (0, 0), 1 } };
        while (current < input)
        {
            var lowerValueCoords = new List<(int x, int y)>();
            var (x, y) = SpiralCoords(position);
            var adjacentPositions = new List<(int x, int y)>
            {
                (x - 1, y - 1),
                (x - 1, y),
                (x - 1, y + 1),
                (x, y - 1),
                (x, y + 1),
                (x + 1, y - 1),
                (x + 1, y),
                (x + 1, y + 1)
            };
            foreach(var adjacent in adjacentPositions)
            {
                lowerValueCoords.Add((adjacent.x, adjacent.y));
            }
            var sum = lowerValueCoords.Where(_ => valuePositions.ContainsKey((_.x, _.y))).Select(_ => valuePositions[(_.x, _.y)]).Sum();
            if (!valuePositions.ContainsKey((x, y)))
            {
                valuePositions.Add((x, y), sum);
            }
            current = sum;
            position++;
        }
        return new SolutionResult(current.ToString());
    }

    private static int CalculateManhattanDistance(int x1, int y1, int x2, int y2) =>
        Math.Abs(x1 - x2) + Math.Abs(y1 - y2);

    public static (int, int) SpiralCoords(int n)
    {
        if (n == 1)
        {
            return (0, 0);
        }
        var k = (int)Math.Ceiling((Math.Sqrt(n) - 1) / 2);
        var t = n - (2 * k - 1) * (2 * k - 1);
        int x, y;
        if (t <= 2 * k)
        {
            x = k;
            y = t - k;
        }
        else if (t <= 4 * k)
        {
            x = 3 * k - t;
            y = k;
        }
        else if (t <= 6 * k)
        {
            x = -k;
            y = 5 * k - t;
        }
        else
        {
            x = t - 7 * k;
            y = -k;
        }

        return (x, y);
    }
}
