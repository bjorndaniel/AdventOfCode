namespace AoC2024;
public class Day22
{
    private static readonly Dictionary<(long, int), long> MemoizationCache = new();

    public static List<long> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<long>();
        foreach (var l in lines)
        {
            result.Add(long.Parse(l));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day22.txt", "Day 22 part 1", 22)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var data = ParseInput(filename);
        var result = 0L;

        for (int j = 0; j < data.Count; j++)
        {
            for (int i = 0; i < 2000; i++)
            {
                data[j] = GenerateNext(data[j]);
            }
        }
        result = data.Sum();
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day22.txt", "Day 22 part 2", 22)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var data = ParseInput(filename);
        var allHighest = new Dictionary<long, List<long>>();
        var counter = filename.Contains("test3") ? 10 : 2000;
        var sequences = new List<Dictionary<(long s1, long s2, long s3, long s4), (long banana, long change)>>();
        for (int j = 0; j < data.Count; j++)
        {
            var dict = new Dictionary<(long s1, long s2, long s3, long s4), (long banana, long change)>();
            var num = data[j] % 10;
            var changes = new List<(long banana, long change)>();
            for (int i = 0; i < counter; i++)
            {
                data[j] = GenerateNext(data[j]);
                if (i == 0)
                {
                    num = data[j] % 10;
                    continue;
                }
                else
                {
                    var newNum = data[j] % 10;
                    var change = newNum - num;
                    changes.Add((newNum, change));
                    num = newNum;
                }
                if (changes.Count == 4 && dict.ContainsKey((changes[0].change, changes[1].change, changes[2].change, changes[3].change)) is false)
                {
                    dict.Add((changes[0].change, changes[1].change, changes[2].change, changes[3].change), (changes[3].banana, changes[3].change));
                    changes.RemoveAt(0);
                }
            }
            sequences.Add(dict);
        }

        return new SolutionResult("");
    }

    public static long GenerateNext(long number)
    {
        var next = number * 64;
        next = Helpers.XorWithPadding(number, next);
        next = next % 16777216;
        var temp = next / 32;
        next = Helpers.XorWithPadding(next, temp);
        next = next % 16777216;
        temp = next * 2048;
        next = Helpers.XorWithPadding(next, temp);
        next = next % 16777216;
        return next;
    }
}