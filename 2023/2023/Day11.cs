namespace AoC2023;
public class Day11
{
    [Solveable("2023/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        var galaxies = ExpandRows(lines, 1);
        var pairs = new List<((long, long), (long, long), long)>();
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                pairs.Add((galaxies[i], galaxies[j], Helpers.ManhattanDistance(galaxies[i], galaxies[j])));
            }
        }
        return new SolutionResult(pairs.Sum(_ => _.Item3).ToString());
    }

    [Solveable("2023/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        var galaxies = ExpandRows(lines, filename.Contains("test") ? 99 : 999999);
        var pairs = new List<((long, long), (long, long), long)>();
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                pairs.Add((galaxies[i], galaxies[j], Helpers.ManhattanDistance(galaxies[i], galaxies[j])));
            }
        }
        return new SolutionResult(pairs.Sum(_ => _.Item3).ToString());
    }

    private static List<(long x, long y)> ExpandRows(string[] lines, long expansion)
    {
        var expandRows = new List<int>();
        var expandCols = new List<int>();
        var galaxies = new List<(long x, long y)>();
        for (int row = 0; row < lines.Length; row++)
        {
            if (!lines[row].Contains('#'))
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
        var dictionary = new Dictionary<(long x, long), long>();
        foreach (var row in expandRows)
        {
            for (var i = 0; i < galaxies.Count; i++)
            {
                if (galaxies[i].y > row)
                {
                    if (dictionary.ContainsKey(galaxies[i]))
                    {
                        dictionary[galaxies[i]] += expansion;
                    }
                    else
                    {
                        dictionary[galaxies[i]] = expansion;
                    }
                }
            }
        }
        for (int i = 0; i < galaxies.Count; i++)
        {
            if (dictionary.TryGetValue(galaxies[i], out long value))
            {
                galaxies[i] = (galaxies[i].x, galaxies[i].y + value);
            }
        }
        dictionary = [];
        foreach (var col in expandCols)
        {
            for (var i = 0; i < galaxies.Count; i++)
            {
                if (galaxies[i].x > col)
                {
                    if (dictionary.ContainsKey(galaxies[i]))
                    {
                        dictionary[galaxies[i]] += expansion;
                    }
                    else
                    {
                        dictionary[galaxies[i]] = expansion;
                    }
                }
            }
        }
        for (int i = 0; i < galaxies.Count; i++)
        {
            if (dictionary.TryGetValue(galaxies[i], out long value))
            {
                galaxies[i] = (galaxies[i].x + value, galaxies[i].y);
            }
        }

        return galaxies;
    }
}