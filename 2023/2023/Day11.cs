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
            if (dictionary.ContainsKey(galaxies[i]))
            {
                galaxies[i] = (galaxies[i].x, galaxies[i].y + dictionary[galaxies[i]]);
            }
        }
        dictionary = new Dictionary<(long x, long), long>();
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
            if (dictionary.ContainsKey(galaxies[i]))
            {
                galaxies[i] = (galaxies[i].x + dictionary[galaxies[i]], galaxies[i].y);
            }
        }

        return galaxies;
    }
}