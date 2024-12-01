namespace AoC2024;
public class Day1
{
    public static List<(long left, long right)> ParseInput(string filename)
    {
        var result = new List<(long left, long right)>();
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var parts = l.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var left = long.Parse(parts[0]);
            var right = long.Parse(parts[1]);
            result.Add((left, right));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day1.txt", "Day 1 part 1", 1)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var left = input.Select(i => i.left).OrderBy(i => i).ToList();
        var right = input.Select(i => i.right).OrderBy(i => i).ToList();
        var result = left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day1.txt", "Day 1 part 2", 1)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var left = input.Select(i => i.left).OrderBy(i => i).ToList();
        var right = input.Select(i => i.right).OrderBy(i => i).ToList();
        var result = left.Sum(l => l * right.Count(r => r == l));
        return new SolutionResult(result.ToString());
    }


}