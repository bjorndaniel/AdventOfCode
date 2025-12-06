namespace AoC2025;

public class Day5
{
    public static (List<(long low, long high)> ranges, List<long> ingredients) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var ranges = new List<(long low, long high)>();
        var ingredients = new List<long>();
        foreach(var line in lines)
        {
            if(string.IsNullOrEmpty(line))
            {
                continue; 
            }
            if (line.Contains("-"))
            {
                var parts = line.Split('-');
                ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));
            }
            else
            {
                ingredients.Add(long.Parse(line));
            }
        }
        return (ranges, ingredients);
        
    }

    [Solveable("2025/Puzzles/Day5.txt", "Day 5 part 1", 5)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (ranges, ingredients) = ParseInput(filename);
        var result = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var (low, high) in ranges)
            {
                if (ingredient >= low && ingredient <= high)
                {
                    result++;
                    break;
                }
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2025/Puzzles/Day5.txt", "Day 5 part 2", 5)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (ranges, _) = ParseInput(filename);
        
        var sortedRanges = ranges.OrderBy(r => r.low).ToList();
        var mergedRanges = new List<(long low, long high)>();
        var currentRange = sortedRanges[0];
        for (int i = 1; i < sortedRanges.Count; i++)
        {
            var nextRange = sortedRanges[i];
            if (nextRange.low <= currentRange.high + 1)
            {
                currentRange = (currentRange.low, Math.Max(currentRange.high, nextRange.high));
            }
            else
            {
                mergedRanges.Add(currentRange);
                currentRange = nextRange;
            }
        }

        mergedRanges.Add(currentRange);
        var result = mergedRanges.Sum(_ => _.high - _.low + 1);
        return new SolutionResult(result.ToString());
    }


}