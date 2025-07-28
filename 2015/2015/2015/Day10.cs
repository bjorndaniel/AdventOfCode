namespace AoC2015;
public class Day10
{
    public static List<int> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<int>();
        foreach (var line in lines)
        {
            foreach (var ch in line)
            {
                if (char.IsDigit(ch))
                {
                    result.Add(int.Parse(ch.ToString())); 
                }
            }
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var rounds = filename.Contains("test") ? 1 : 40; // 1 round for test, 40 for real input

        var current = CreateSequence(input, rounds);

        // Output the length of the final sequence as the answer
        return new SolutionResult(current.Length.ToString());
    }

    [Solveable("2015/Puzzles/Day10.txt", "Day 10 part 2", 10)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var current = CreateSequence(input, 50);
        return new SolutionResult(current.Length.ToString());
    }

    private static string CreateSequence(List<int> input, int rounds)
    {
        var current = new string(input.Select(i => (char)('0' + i)).ToArray());

        for (int i = 0; i < rounds; i++)
        {
            var sb = new StringBuilder();
            int count = 1;
            for (int j = 1; j <= current.Length; j++)
            {
                if (j < current.Length && current[j] == current[j - 1])
                {
                    count++;
                }
                else
                {
                    sb.Append(count);
                    sb.Append(current[j - 1]);
                    count = 1;
                }
            }
            current = sb.ToString();
        }

        return current;
    }
}