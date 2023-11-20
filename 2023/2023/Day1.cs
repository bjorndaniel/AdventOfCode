namespace AoC2023;
public static class Day1
{
    [Solveable("Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        return new SolutionResult($"A result for {filename}");
    }

    [Solveable("Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        printer.Print("Inside Part 2 day 1");
        printer.Flush();
        Thread.Sleep(1000);
        return new SolutionResult($"{lines.First()}");
    }

}
