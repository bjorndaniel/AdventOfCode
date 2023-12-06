namespace AoC2017;
public class Day4
{
    public static List<List<string>> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(_ => _.Split(" ").ToList()).ToList();
    }

    [Solveable("2017/Puzzles/Day4.txt", "Day 4 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var result = lines.Select(_ => _.GroupBy(_ => _));
        return new SolutionResult(result.Where(_ => _.All(_ => _.Count() == 1)).Count().ToString());
    }

    [Solveable("2017/Puzzles/Day4.txt", "Day 4 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var result = lines.Select(_ => _.GroupBy(_ => _));
        var valid = result.Where(_ => _.All(_ => _.Count() == 1)).Select(_ => _.Select(_ => _.Key).ToList()).ToList();
        var count = 0;
        foreach(var str in valid)
        {
            var anagramsExist = str.GroupBy(s => new string(s.OrderBy(c => c).ToArray())).Any(g => g.Count() > 1);
            if (!anagramsExist)
            {
                count++;
            }
        }
        return new SolutionResult(count.ToString());
    }

}   