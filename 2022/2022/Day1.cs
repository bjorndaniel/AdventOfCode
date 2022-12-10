namespace AoC2022;
public static class Day1
{
    public static IEnumerable<Backpack> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        List<List<int>> result = new();
        var current = new List<int>();
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                result.Add(current);
                current = new List<int>();
            }
            else
            {
                current.Add(int.Parse(l));
            }
        }
        result.Add(current);
        return result.Select(x => new Backpack(x));
    }

    public static long SolvePart1(string filename)
    {
        var backpacks = ParseInput(filename);
        return backpacks.Max(_ => _.TotalCalories);
    }

    public static long SolvePart2(string filename)
    {
        var backpacks = ParseInput(filename);

        return backpacks
            .OrderByDescending(_ => _.TotalCalories)
            .Take(3)
            .Sum(_ => _.TotalCalories);
    }
}

public record Backpack(List<int> Calories)
{
    public long TotalCalories => Calories.Sum();
}

