using System.Text;

namespace AoC2024;
public class Day3
{
    public static string ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return string.Join("", lines);
    }

    [Solveable("2024/Puzzles/Day3.txt", "Day 3 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var line = ParseInput(filename);
        return new SolutionResult(Calculate(line).ToString());
    }
    
    [Solveable("2024/Puzzles/Day3.txt", "Day 3 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var line = ParseInput(filename);
        line = RemoveDontDoParts(line);
        return new SolutionResult(Calculate(line).ToString());

        string RemoveDontDoParts(string input)
        {
            var pattern = @"(don't\(\)|do\(\)|mul\(\d+,\d+\))";
            var matches = Regex.Matches(input, pattern);
            var isEnabled = true;
            var result = new StringBuilder();

            foreach (Match match in matches)
            {
                if (match.Value == "don't()")
                {
                    isEnabled = false;
                }
                else if (match.Value == "do()")
                {
                    isEnabled = true;
                }
                else if (isEnabled && match.Value.StartsWith("mul"))
                {
                    result.Append(match.Value);
                }
            }

            return result.ToString();
        }
    }

    private static int Calculate(string line)
    {
        var pattern = @"mul\((\d+),(\d+)\)";
        var instructions = Regex.Matches(line, pattern);
        var result = 0;
        foreach (Match instruction in instructions)
        {
            var a = int.Parse(instruction.Groups[1].Value);
            var b = int.Parse(instruction.Groups[2].Value);
            result += a * b;
        }
        return result;
    }
}