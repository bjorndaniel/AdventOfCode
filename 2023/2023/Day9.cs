namespace AoC2023;
public class Day9
{
    public static List<List<long>> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(_ => _.Split(" ").Select(_ => long.Parse(_)).ToList()).ToList();
    }

    [Solveable("2023/Puzzles/Day9.txt", "Day 9 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var sequences = ParseInput(filename);
        var sum = 0L;
        foreach (var sequence in sequences)
        {
            var nextSequenses = GetSequences(sequence);
            var ordered = nextSequenses.OrderBy(_ => _.Count()).Skip(1).ToList();
            for (int i = 0; i < ordered.Count() - 1; i++)
            {
                ordered[i + 1].Add(ordered[i + 1].Last() + ordered[i].Last());
            }
            sum += ordered.Last().Last();
        }

        return new SolutionResult(sum.ToString());
    }


    [Solveable("2023/Puzzles/Day9.txt", "Day 9 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var sequences = ParseInput(filename);
        var sum = 0L;
        foreach (var sequence in sequences)
        {
            var nextSequenses = GetSequences(sequence);
            var ordered = nextSequenses.OrderBy(_ => _.Count()).Skip(1).ToList();
            for (int i = 0; i < ordered.Count() - 1; i++)
            {
                ordered[i + 1].Insert(0,ordered[i + 1].First() - ordered[i].First());
            }
            sum += ordered.Last().First();
        }
        return new SolutionResult(sum.ToString());
    }

    private static List<List<long>> GetSequences(List<long> sequence)
    {
        var nextSequenses = new List<List<long>>
            {
                sequence
            };
        var current = sequence;
        while (!current.All(_ => _ == 0))
        {
            var newSequence = new List<long>();
            for (int i = 1; i < current.Count; i++)
            {
                newSequence.Add(current[i] - current[i - 1]);
            }
            nextSequenses.Add(newSequence);
            current = newSequence;
        }

        return nextSequenses;
    }
}