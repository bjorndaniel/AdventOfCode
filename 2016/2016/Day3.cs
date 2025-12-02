namespace AoC2016;

public class Day3
{
    public static List<(int a, int b, int c)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(int a, int b, int c)>();
        foreach (var line in lines)
        {
            var sides = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s));

            result.Add((sides.ElementAt(0), sides.ElementAt(1), sides.ElementAt(2)));
        }
        return result;
    }

    [Solveable("2016/Puzzles/Day3.txt", "Day 3 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var triangles = ParseInput(filename);
        var result = 0;
        foreach (var triangle in triangles)
        {
            var a = triangle.a;
            var b = triangle.b;
            var c = triangle.c;
            if (a + b > c && a + c > b && b + c > a)
            {
                result++;
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2016/Puzzles/Day3.txt", "Day 3 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var rows = ParseInput(filename);
        var result = 0;

        var groups = rows
            .Select((row, index) => new { row, index })
            .GroupBy(x => x.index / 3)
            .Select(g => g.Select(x => x.row).ToArray());

        foreach (var grp in groups)
        {
            if (grp.Length < 3)
            {
                continue;
            }

            var r1 = grp[0];
            var r2 = grp[1];
            var r3 = grp[2];

            var col1 = (a: r1.a, b: r2.a, c: r3.a);
            var col2 = (a: r1.b, b: r2.b, c: r3.b);
            var col3 = (a: r1.c, b: r2.c, c: r3.c);

            foreach (var t in new[] { col1, col2, col3 })
            {
                if (t.a + t.b > t.c && t.a + t.c > t.b && t.b + t.c > t.a)
                {
                    result++;
                }
            }
        }

        return new SolutionResult(result.ToString());
    }


}