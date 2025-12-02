namespace AoC2016;

public class Day6
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2016/Puzzles/Day6.txt", "Day 6 part 1", 6)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = "";
        foreach (var i in Enumerable.Range(0, input[0].Length))
        {
            var maxChar = input.Select(line => line[i])
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count()).
                ThenBy(g => g.Key).First().Key;
            result += maxChar;
        }
        return new SolutionResult(result);
    }

    [Solveable("2016/Puzzles/Day6.txt", "Day 6 part 2", 6)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = "";
        foreach (var i in Enumerable.Range(0, input[0].Length))
        {
            var maxChar = input.Select(line => line[i])
             .GroupBy(c => c)
             .OrderBy(g => g.Count()).
             ThenBy(g => g.Key).First().Key;
            result += maxChar;
        }
        return new SolutionResult(result);
    }
}