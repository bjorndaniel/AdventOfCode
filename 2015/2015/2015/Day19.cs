namespace AoC2015;

public class Day19
{
    public static (List<Replacement> replacements, string molecule) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var replacements = new List<Replacement>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break;
            }
            var parts = line.Split(" => ");
            replacements.Add(new Replacement(parts[0], parts[1]));
        }
        return (replacements, lines.Last());
    }

    [Solveable("2015/Puzzles/Day19.txt", "Day19 part1", 19)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (replacements, molecule) = ParseInput(filename);
        var results = new HashSet<string>();

        foreach (var repl in replacements)
        {
            var from = repl.From;
            var to = repl.To;
            var start = 0;
            while (true)
            {
                var idx = molecule.IndexOf(from, start, StringComparison.Ordinal);
                if (idx < 0)
                {
                    break;
                }
                var newMolecule = $"{molecule.Substring(0, idx)}{to}{molecule.Substring(idx + from.Length)}";
                results.Add(newMolecule);
                start = idx + 1;
            }
        }

        return new SolutionResult(results.Count.ToString());
    }

    [Solveable("2015/Puzzles/Day19.txt", "Day19 part2",19)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (replacements, molecule) = ParseInput(filename);

        // Use BFS for small inputs (guaranteed correct), and the fast formula for larger inputs
        var useBfs = molecule.Length <=50 || replacements.Count <=10;

        if (useBfs)
        {
            var steps = ReverseBfsReduce(replacements, molecule, "e");
            return new SolutionResult(steps <0 ? string.Empty : steps.ToString());
        }

        // Fallback: token formula (very fast for real puzzle input)
        var tokens = new List<string>();
        for (var i =0; i < molecule.Length; i++)
        {
            var ch = molecule[i];
            if (char.IsUpper(ch))
            {
                if (i +1 < molecule.Length && char.IsLower(molecule[i +1]))
                {
                    tokens.Add(molecule.Substring(i,2));
                    i++; // skip next
                }
                else
                {
                    tokens.Add(molecule.Substring(i,1));
                }
            }
        }

        var tokenCount = tokens.Count;
        var rnCount = tokens.Count(t => t == "Rn");
        var arCount = tokens.Count(t => t == "Ar");
        var yCount = tokens.Count(t => t == "Y");

        var stepsFormula = tokenCount - rnCount - arCount -2 * yCount -1;
        return new SolutionResult(stepsFormula.ToString());
    }

    // Deterministic reverse BFS: apply reverse replacements (To -> From) until reaching target. Returns step count or -1 if not found.
    private static int ReverseBfsReduce(List<Replacement> replacements, string molecule, string target)
    {
        var rev = replacements.Select(r => new Replacement(r.To, r.From)).ToList();
        var q = new Queue<(string mol, int steps)>();
        var seen = new HashSet<string>();

        q.Enqueue((molecule,0));
        seen.Add(molecule);

        while (q.Count >0)
        {
            var (cur, steps) = q.Dequeue();
            if (cur == target)
            {
                return steps;
            }

            foreach (var r in rev)
            {
                var indices = AllIndexesOf(cur, r.From);
                foreach (var idx in indices)
                {
                    var next = ReplaceAt(cur, idx, r.From.Length, r.To);
                    if (!seen.Contains(next))
                    {
                        seen.Add(next);
                        q.Enqueue((next, steps +1));
                    }
                }
            }
        }

        return -1;
    }

    private static string ReplaceAt(string input, int index, int length, string replacement)
    {
        return input.Substring(0, index) + replacement + input.Substring(index + length);
    }

    private static List<int> AllIndexesOf(string text, string value)
    {
        var result = new List<int>();
        if (string.IsNullOrEmpty(value) || value.Length > text.Length)
        {
            return result;
        }
        var start =0;
        while (start <= text.Length - value.Length)
        {
            var idx = text.IndexOf(value, start, StringComparison.Ordinal);
            if (idx <0)
            {
                break;
            }
            result.Add(idx);
            start = idx +1;
        }
        return result;
    }

    public record Replacement(string From, string To);
}