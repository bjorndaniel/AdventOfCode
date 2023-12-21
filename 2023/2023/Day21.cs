namespace AoC2023;
public class Day21
{
    private static List<(int x, int y)> _directions = new List<(int x, int y)>
    {
        ( 0, 1 ),
        ( 0, -1 ),
        (1, 0),
        ( -1, 0 )
    };
    private static Dictionary<(int, int, int), long> _memo = new();
    private static Dictionary<(int x, int y), bool> _found = new();

    public static ((int x, int y) start, Dictionary<(int x, int y), char> plot) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var plot = new Dictionary<(int x, int y), char>();
        var start = (0, 0);
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines.First().Length; col++)
            {
                plot.Add((col, row), lines[row][col]);
                if (lines[row][col] == 'S')
                {
                    start = (col, row);
                }
            }
        }
        return (start, plot);
    }

    [Solveable("2023/Puzzles/Day21.txt", "Day 21 part 1", 21)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (start, plot) = ParseInput(filename);
        _memo = new();
        _found = new()
        {
            [(start.x, start.y)] = true
        };

        DFS(plot, start.x, start.y, filename.Contains("test") ? 6 : 64, new());

        return new SolutionResult(_found.Count().ToString());
    }

    [Solveable("2023/Puzzles/Day21.txt", "Day 21 part 2", 21)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (start, plot) = ParseInput(filename);
        var originalX = plot.Keys.Max(x => x.x);
        var originalY = plot.Keys.Max(x => x.y);
        var x = originalX + 1;
        var steps = filename.Contains("test") ? 50L : 26501365L;
        //var pattern = new List<int>
        //{
        //    x / 2, 
        //    (int)(x * 1.5),
        //    (int)(x * 2.5), 
        //    (int)(x * 3.5), 
        //    (int)(x * 4.5), 
        //    (int)(x * 5.5),
        //    (int)(x * 6.5),
        //    (int)(x * 7.5),
        //    (int)(x * 8.5),
        //    (int)(x * 9.5)
        //};
        //foreach (var i in pattern)
        //{
        //    _memo = new();
        //    _found = new()
        //    {
        //        [(start.x, start.y)] = true
        //    };

        //    DFS(plot, start.x, start.y, i, plot, true, originalX + 1, originalY + 1);
        //    //Trying to find a pattern for the diamond shape n
        //    //put it into https://www.wolframalpha.com/ and found the equation below
        //    printer.Print($"steps: {i} nr: {_found.Count()}");
        //    printer.Flush();
        //}
        if (filename.Contains("test"))
        {
            _memo = new();
            _found = new()
            {
                [(start.x, start.y)] = true
            };
            DFS(plot, start.x, start.y, (int)steps, plot, true, originalX + 1, originalY + 1);
            return new SolutionResult(_found.Count().ToString());
        }
        var plots = 0.0;
        var number = ((steps - 65) / 131) + 1;
        plots = (15158 * Math.Pow(number, 2)) - (15061 * number) + 3724.67;
        return new SolutionResult(((long)plots).ToString());
    }

    private static long DFS(Dictionary<(int x, int y), char> plot, int x, int y, int steps, Dictionary<(int x, int y), char> originalGrid, bool isPart2 = false, int originalX = 0, int originalY = 0)
    {
        if (!plot.ContainsKey((x, y)))
        {
            if (isPart2)
            {
                var wrappedX = ((x % originalX) + originalX) % originalX;
                var wrappedY = ((y % originalY) + originalY) % originalY;
                plot.Add((x, y), originalGrid[(wrappedX, wrappedY)]);
            }
            else
            {
                return 0;
            }
        }
        if (plot[(x, y)] == '#')
        {
            return 0;
        }

        if (steps <= 0)
        {
            _found[(x, y)] = true;
            return 1;
        }

        if (_memo.ContainsKey((x, y, steps)))
        {
            return _memo[(x, y, steps)];
        }
        var count = 0L;


        for (int i = 0; i < 4; i++)
        {
            var newX = x + _directions[i].x;
            var newY = y + _directions[i].y;
            count += DFS(plot, newX, newY, steps - 1, originalGrid, isPart2, originalX, originalY);
        }

        _memo[(x, y, steps)] = count;

        return count;
    }

}