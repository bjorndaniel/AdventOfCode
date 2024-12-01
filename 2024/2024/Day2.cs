namespace AoC2024;
public class Day2
{   
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2024/Puzzles/Day2.txt", "Day 2 part 1", 2)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    [Solveable("2024/Puzzles/Day2.txt", "Day 2 part 2", 2)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }
}