namespace AoC2015;

public class Day17
{
    public static List<Container> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Container>();

        foreach (var line in lines)
        {
            result.Add(new Container(int.Parse(line)));
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day17.txt", "Day 17 part 1", 17)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var containers = ParseInput(filename);
        var ways = CountCombinations(containers, 150);
        return new SolutionResult(ways.ToString());
    }

    [Solveable("2015/Puzzles/Day17.txt", "Day 17 part 2", 17)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var containers = ParseInput(filename);
        var (minUsed, ways) = CountMinCombinations(containers, 150);
        return new SolutionResult(ways.ToString());
    }

    private static int CountCombinations(List<Container> containers, int target)
    {
        var count = 0;
        ForEachCombination(containers, target, (used) =>
        {
            count++;
        });
        return count;
    }

    private static (int minUsed, int ways) CountMinCombinations(List<Container> containers, int target)
    {
        var minUsed = int.MaxValue;
        var ways = 0;

        ForEachCombination(containers, target, (used) =>
        {
            if (used < minUsed)
            {
                minUsed = used;
                ways = 1;
            }
            else if (used == minUsed)
            {
                ways++;
            }
        });

        if (minUsed == int.MaxValue)
        {
            return (0, 0);
        }

        return (minUsed, ways);
    }

    private static void ForEachCombination(List<Container> containers, int target, Action<int> onSolution)
    {
        var counts = new int[containers.Count];

        void Recurse(int index, int sum, int used)
        {
            if (sum == target)
            {
                onSolution(used);
                return;
            }

            if (sum > target || index >= containers.Count)
            {
                return;
            }

            Recurse(index + 1, sum + containers[index].Liters, used + 1);
            Recurse(index + 1, sum, used);
        }

        Recurse(0, 0, 0);
    }

    public record Container(int Liters);
}