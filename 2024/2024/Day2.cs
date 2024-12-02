namespace AoC2024;
public class Day2
{
    public static List<List<int>> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<List<int>>();
        foreach (var line in lines)
        {
            result.Add(line.Split(" ").Select(_ => int.Parse(_)).ToList());
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day2.txt", "Day 2 part 1", 2)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = 0;
        foreach (var line in input)
        {
            if (IsSafe(line))
            {
                result++;
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day2.txt", "Day 2 part 2", 2)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = 0;
        foreach (var line in input)
        {
            if (IsSafe(line))
            {
                result += 1;
            }
            else
            {
                for (int i = 0; i < line.Count; i++)
                {
                    var modifiedLine = new List<int>(line);
                    modifiedLine.RemoveAt(i);
                    if (IsSafe(modifiedLine))
                    {
                        result += 1;
                        break;
                    }
                }
            }
        }
        return new SolutionResult(result.ToString());
    }

    private static bool IsSafe(List<int> sequence)
    {
        var unSafe = sequence.Zip(sequence.Skip(1), (a, b) => Math.Abs(a - b)).Any(_ => _ > 3);
        var increasing = IsIncreasingSequence(sequence);
        var decreasing = IsDecreasingSequence(sequence);
        return unSafe is false && (increasing || decreasing);
    }

    private static bool IsIncreasingSequence(List<int> sequence)
    {
        for (int i = 0; i < sequence.Count - 1; i++)
        {
            if (sequence[i] >= sequence[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsDecreasingSequence(List<int> sequence)
    {
        for (int i = 0; i < sequence.Count - 1; i++)
        {
            if (sequence[i] <= sequence[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}