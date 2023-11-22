namespace AoC2018;
public class Day1
{
    public static List<FreqChange> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<FreqChange>();
        foreach (var l in lines)
        {
            if (l[0] == '+')
            {
                result.Add(new FreqChange(int.Parse(l[1..]), true));
            }
            else if (l[0] == '-')
            {
                result.Add(new FreqChange(int.Parse(l[1..]), false));
            }
            else
            {
                result.Add(new FreqChange(int.Parse(l), true));
            }
        }
        return result;
    }

    [Solveable("2018/Puzzles/Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var changes = ParseInput(filename);
        var result = 0;
        foreach (var line in changes)
        {
            result = line.isPositive ? result + line.change : result - line.change;
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2018/Puzzles/Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var changes = ParseInput(filename);
        var notDone = true;
        var frequencies = new List<int>();
        var result = 0;
        var count = 0;
        while (notDone)
        {
            result = changes[count % changes.Count].isPositive ? result + changes[count % changes.Count].change : result - changes[count % changes.Count].change;
            if (frequencies.Contains(result))
            {
                return new SolutionResult(result.ToString());
            }
            else
            {
                frequencies.Add(result);
            }
            count++;
        }
        return new SolutionResult("");
    }

    public record FreqChange(int change, bool isPositive);


}
