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
        var allHighest = new Dictionary<int, List<int>>();
        var counter = filename.Contains("test3") ? 10 : 2000;
        for (int j = 0; j < data.Count; j++)
        {
            //var highestLastDigit = new Dictionary<int, int>();
            //var highestSequence = new List<long>();
            List<int> lastDigits = [(int)(data[0] % 10)];
            for (int i = 0; i < counter; i++)
            {
                data[j] = GenerateNext(data[j]);
                lastDigits.Add((int)(data[j] % 10));
                //highestLastDigit[i] = (int)data[j] % 10;
            }
            allHighest[j] = lastDigits;
        }
        foreach(var kvp in allHighest)
        {
            printer.Print($"{kvp.Key} {kvp.Value.Select(_ => _.ToString()).Aggregate((a,b) => $"{a}, {b}")}");
            printer.Flush();
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