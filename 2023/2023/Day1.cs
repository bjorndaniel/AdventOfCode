namespace AoC2023;
public class Day1
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2023/Puzzles/Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    [Solveable("2023/Puzzles/Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


}