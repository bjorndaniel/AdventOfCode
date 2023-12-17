namespace AoC2023;
public class Day18
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return [.. lines];
    }

    [Solveable("2023/Puzzles/Day18.txt", "Day 18 part 1", 18)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    [Solveable("2023/Puzzles/Day18.txt", "Day 18 part 2", 18)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


}