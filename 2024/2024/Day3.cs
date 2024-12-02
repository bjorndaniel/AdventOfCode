namespace AoC2024;
public class Day3
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2024/Puzzles/Day3.txt", "Day 3 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    [Solveable("2024/Puzzles/Day3.txt", "Day 3 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


}