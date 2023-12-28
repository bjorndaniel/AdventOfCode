namespace AoC2022;
public class Day1
{
    public static IEnumerable<Backpack> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        List<List<int>> result = [];
        var current = new List<int>();
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                result.Add(current);
                current = [];
            }
            else
            {
                current.Add(int.Parse(l));
            }
        }
        result.Add(current);
        return result.Select(x => new Backpack(x));
    }

    [Solveable("2022/Puzzles/Day1.txt", "Day 1 part 1", 1)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var backpacks = ParseInput(filename);
        return new SolutionResult(backpacks.Max(_ => _.TotalCalories).ToString());
    }

    [Solveable("2022/Puzzles/Day1.txt", "Day 1 part 2", 1)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var backpacks = ParseInput(filename);

        return new SolutionResult(backpacks
            .OrderByDescending(_ => _.TotalCalories)
            .Take(3)
            .Sum(_ => _.TotalCalories).ToString());
    }

    public record Backpack(List<int> Calories)
    {
        public long TotalCalories => Calories.Sum();
    }
}

