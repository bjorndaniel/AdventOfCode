namespace AoC2015;
public class Day8
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return [.. lines];
    }

    [Solveable("2015/Puzzles/Day8.txt", "Day 8 part 1", 8)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        int totalCharacters = 0;
        foreach (var line in input)
        {
            var unquoted = line.Substring(1, line.Length - 2);
            var decoded = Regex.Replace(unquoted, @"\\(\\|""|x[0-9a-fA-F]{2})", match =>
            {
                if (match.Value.StartsWith("\\x"))
                {
                    return ((char)Convert.ToInt32(match.Value.Substring(2), 16)).ToString();
                }
                else
                {
                    return match.Value.Substring(1);
                }
            });
            totalCharacters += line.Length - decoded.Length;
        }
        return new SolutionResult(totalCharacters.ToString());
    }

    [Solveable("2015/Puzzles/Day8.txt", "Day 8 part 2", 8)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        int totalCharacters = 0;
        foreach (var line in input)
        {
            var encoded = "\"" + line.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
            totalCharacters += encoded.Length - line.Length;
        }
        return new SolutionResult(totalCharacters.ToString());
    }


}