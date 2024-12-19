namespace AoC2024;
public class Day19
{
    public static (List<Towel> towels, List<Design> designs) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var towels = new List<Towel>();
        var designs = new List<Design>();
        var isTowel = true;
        foreach (var l in lines)
        {
            if (isTowel)
            {
                var t = l.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(_ => new Towel(_.Trim()));
                towels.AddRange(t);
            }
            else
            {
                designs.Add(new Design(l));
            }
            if (string.IsNullOrEmpty(l))
            {
                isTowel = false;
            }
        }
        return (towels, designs);
    }

    [Solveable("2024/Puzzles/Day19.txt", "Day 19 part 1", 19)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (towels, designs) = ParseInput(filename);
        var towelPatterns = towels.Select(t => t.Pattern).ToList();
        var possibleDesigns = new List<Design>();

        foreach (var design in designs)
        {
            if (CanFormPattern(design, towelPatterns))
            {
                possibleDesigns.Add(design);
            }
        }

        return new SolutionResult(possibleDesigns.Count.ToString());
    }

    [Solveable("2024/Puzzles/Day19.txt", "Day 19 part 2", 19)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (towels, designs) = ParseInput(filename);
        var result = 0L;
        foreach (var design in designs)
        {
            result += CountWaysToFormPattern(design, towels);
        }

        return new SolutionResult(result.ToString());
    }

    private static bool CanFormPattern(Design design, List<string> towels)
    {
        var queue = new Queue<string>();
        var visited = new HashSet<string>();
        queue.Enqueue(design.Pattern);
        visited.Add(design.Pattern);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (string.IsNullOrEmpty(current))
            {
                return true;
            }

            foreach (var towel in towels)
            {
                if (current.StartsWith(towel))
                {
                    var next = current.Substring(towel.Length);
                    if (!visited.Contains(next))
                    {
                        queue.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }
        }

        return false;
    }

    private static long CountWaysToFormPattern(Design design, List<Towel> towels)
    {
        var n = design.Pattern.Length;
        var dp = new long[n + 1];
        dp[0] = 1;
        for (long i = 1; i <= n; i++)
        {
            foreach (var towel in towels)
            {
                long len = towel.Pattern.Length;
                if (i >= len && design.Pattern.Substring((int)i - (int)len, (int)len) == towel.Pattern)
                {
                    dp[i] += dp[i - len];
                }
            }
        }

        return dp[n];
    }
}

public record Towel(string Pattern);
public record Design(string Pattern);
