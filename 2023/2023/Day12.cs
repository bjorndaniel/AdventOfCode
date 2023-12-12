
namespace AoC2023;
public class Day12
{
    private static Dictionary<string, long> _cache = new Dictionary<string, long>();
    public static List<ConditionRecord> ParseInput(string filename, bool isPart2 = false)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<ConditionRecord>();
        foreach (var line in lines)
        {
            var conditions = line.Split(" ")[0];
            var groups = line.Split(" ")[1];
            if (isPart2)
            {
                conditions = Enumerable.Range(0, 5).Select(e => conditions + "?").Aggregate((x, y) => $"{x}{y}")[..^1];
                groups = Enumerable.Range(0, 5).Select(e => groups + ",").Aggregate((x, y) => $"{x}{y}")[..^1];
                if(conditions.Length != (line.Split(" ")[0].Length * 5) + 4)
                {
                    throw new ArgumentException("Wrong length");
                }
                if(groups.Length != (line.Split(" ")[1].Length * 5) + 4)
                {
                    throw new ArgumentException("Wrong length");
                }
                result.Add(new ConditionRecord(conditions, groups.Split(",").Select(_ => int.Parse(_)).ToList()));
            }
            else
            {
                result.Add(new ConditionRecord(conditions, groups.Split(",").Select(_ => int.Parse(_)).ToList()));
            }
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        _cache = new Dictionary<string, long>();
        var records = ParseInput(filename);
        var sum = 0L;
        foreach (var record in records)
        {
            var result = CountConfigs(record.Records, record.Groups);
            sum += result;
        }
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2023/Puzzles/Day12.txt", "Day 12 part 2", 12)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        _cache = new Dictionary<string, long>();
        var records = ParseInput(filename, true);
        var sum = 0L;
        foreach (var record in records)
        {
            var result = CountConfigs(record.Records, record.Groups);
            sum += result;
        }
        return new SolutionResult(sum.ToString());
    }

    public record ConditionRecord(string Records, List<int> Groups) { }

    private static long CountConfigs(string records, List<int> groups)
    {
        if (records.Length == 0)
        {
            return groups.Any() ? 0 : 1;
        }
        if (!groups.Any())
        {
            return records.Contains("#") ? 0 : 1;
        }
        var key = $"{records}{(groups.Select(_ => _.ToString()).Aggregate((a, b) => $"{a}{b}"))}";
        if (_cache.ContainsKey(key))
        {
            return _cache[key];
        }

        var result = 0L;

        if (records[0] == '?' || records[0] == '.')
        {
            result += CountConfigs(records[1..], groups);
        }
        if (records[0] == '#' || records[0] == '?')
        {
            if (groups[0] <= records.Length &&
                !records[..groups[0]].Contains('.') &&
                (groups[0] == records.Length || records[groups[0]] != '#')
            )
            {
                var index = (groups[0] + 1) > records.Length ? groups[0] : groups[0] + 1;
                result += CountConfigs(records[index..], groups[1..]);
            }
        }
        _cache[key] = result;
        return result;

    }

}