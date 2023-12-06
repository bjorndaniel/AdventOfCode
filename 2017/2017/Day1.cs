namespace AoC2017;
public class Day1
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2017/Puzzles/Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename).First();
        var sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (i == input.Length - 1 && input[0] == input[i])
            {
                sum += int.Parse(input[i].ToString());
            }
            else if (i < input.Length - 1 && input[i] == input[i + 1])
            {
                sum += int.Parse(input[i].ToString());
            }
        }
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2017/Puzzles/Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename).First();
        var sum = 0;
        var stepsForward = input.Length / 2;
        for (int i = 0; i < input.Length; i++)
        {
            var currentChar = input[i];
            var targetCharIndex = (i + stepsForward) % input.Length;
            var targetChar = input[targetCharIndex];
            if (currentChar == targetChar)
            {
                sum += int.Parse(currentChar.ToString());
            }
        }

        return new SolutionResult(sum.ToString());
    }


}