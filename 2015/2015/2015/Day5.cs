namespace AoC2015;
public class Day5
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return [.. lines];
    }

    [Solveable("2015/Puzzles/Day5.txt", "Day5  part 1", 5)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var strings = ParseInput(filename);
        var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
        var badStrings = new List<string> { "ab", "cd", "pq", "xy" };
        var niceStrings = 0;
        foreach(var input in strings)
        {
            if(input.Count(c => vowels.Contains(c)) < 3)
            {
                continue;
            }
            if(input.Select((c, i) => new { c, i}).Any(_ => _.i < input.Length - 1 && _.c == input[_.i + 1]) is false)
            {
                continue;
            }
            if(badStrings.Any(b => input.Contains(b)))
            {
                continue;
            }
            niceStrings++;
        }
        return new SolutionResult(niceStrings.ToString());
    }

    [Solveable("2015/Puzzles/Day5.txt", "Day5  part 2", 5)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var strings = ParseInput(filename);
        var niceStrings = 0;
        foreach (var input in strings)
        {
           if(MeetsConditions(input) is false)
            {
                continue;
            }
            niceStrings++;
        }

        return new SolutionResult(niceStrings.ToString());

        static bool MeetsConditions(string input)
        {
            if (Regex.IsMatch(input, @"(..).*\1") is false)
            {
                return false; 
            }

            if (Regex.IsMatch(input, @"(.).\1") is false)
            {
                return false;
            }
            return true;
        }
    }
}