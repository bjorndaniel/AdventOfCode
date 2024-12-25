namespace AoC2024;
public class Day25
{
    public static (List<List<int>> locks, List<List<int>> keys) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var locks = new List<List<int>>();
        var keys = new List<List<int>>();
        var parsed = new List<List<string>>();
        var current = new List<string>();
        foreach (var l in lines)
        {
            if(string.IsNullOrWhiteSpace(l))
            {
                parsed.Add(current);
                current = new List<string>();
                continue;
            }
            current.Add(l);
        }
        parsed.Add(current);
        foreach(var p in parsed)
        {
            if (p[0].Count(_ => _ == '#') == 5)
            {
                var lockRows = p.Skip(1).Take(5);
                var row = new List<int>(new int[lockRows.First().Length]);
                foreach (var lockRow in lockRows)
                {
                    for (int i = 0; i < lockRow.Length; i++)
                    {
                        if (lockRow[i] == '#')
                        {
                            row[i]++;
                        }
                    }
                }
                locks.Add(row);
            }
            else
            {
                var keyRows = p.Skip(1).Take(5);
                var row = new List<int>(new int[keyRows.First().Length]);
                foreach (var keyRow in keyRows)
                {
                    for (int i = 0; i < keyRow.Length; i++)
                    {
                        if (keyRow[i] == '#')
                        {
                            row[i]++;
                        }
                    }
                }
                keys.Add(row);
            }
        }
        return (locks, keys);
    }

    [Solveable("2024/Puzzles/Day25.txt", "Day 25 part 1", 25)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (locks, keys) = ParseInput(filename);
        var result = 0;
        foreach (var lockRow in locks)
        {
            foreach (var keyRow in keys)
            {
                bool isValid = true;
                for (int i = 0; i < lockRow.Count; i++)
                {
                    if (lockRow[i] + keyRow[i] > 5)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    result++;
                }
            }
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day25.txt", "Day 25 part 2", 25)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


}