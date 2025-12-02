namespace AoC2025;

public class Day2
{
    public static List<(long from, long to)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(long from, long to)> ();
        foreach(var line in lines)
        {
            var parts = line.Split(',');
            result.AddRange(parts.Select(part =>
            {
                var rangeParts = part.Split('-');
                return (from: long.Parse(rangeParts[0]), to: long.Parse(rangeParts[1]));
            }));
        }
        return result;
    }


    [Solveable("2025/Puzzles/Day2.txt", "Day 2 part 1", 2)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var ranges = ParseInput(filename);
        long total = 0;
        foreach (var range in ranges)
        {
            for (long v = range.from; v <= range.to; v++)
            {
                if (GetRepeatedPatternOccurrences(v) == 2)
                {
                    total += v;
                }
            }
        }

        return new SolutionResult(total.ToString());
    }

    [Solveable("2025/Puzzles/Day2.txt", "Day 2 part 2", 2)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var ranges = ParseInput(filename);
        long count = 0;
        foreach (var range in ranges)
        {
            for (long v = range.from; v <= range.to; v++)
            {
                if (GetRepeatedPatternOccurrences(v) >= 2)
                {
                    count += v;
                }
            }
        }
        return new SolutionResult(count.ToString());
    }

    private static int GetRepeatedPatternOccurrences(long value)
    {
        var numberAsString = value.ToString();
        var totalLength = numberAsString.Length;
        
        // Check if the number can be split exactly in half with both halves matching
        if (totalLength % 2 == 0)
        {
            var halfLength = totalLength / 2;
            if (numberAsString.Substring(0, halfLength) == numberAsString.Substring(halfLength, halfLength))
            {
                return 2;
            }
        }
        
        // Check for other repeating patterns (smaller patterns that repeat more times)
        for (int patternLength = 1; patternLength <= totalLength / 2; patternLength++)
        {
            if (totalLength % patternLength != 0)
            {
                continue;
            }
            
            var pattern = numberAsString.Substring(0, patternLength);
            var allPatternsMatch = true;
            
            for (int position = patternLength; position < totalLength; position += patternLength)
            {
                if (numberAsString.Substring(position, patternLength) != pattern)
                {
                    allPatternsMatch = false;
                    break;
                }
            }
            
            if (allPatternsMatch)
            {
                return totalLength / patternLength;
            }
        }
        return 1;
    }

}