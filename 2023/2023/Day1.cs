namespace AoC2023;
public class Day1
{
    private static Dictionary<string, int> _numbers = new Dictionary<string, int>
    {
        {"one", 1 },
        {"two", 2},
        {"three", 3 },
        {"four", 4},
        {"five", 5 },
        {"six", 6 },
        {"seven", 7 },
        {"eight", 8 },
        {"nine", 9 }
    };


    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return [.. lines];
    }

    [Solveable("2023/Puzzles/Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var value = 0;
        foreach (var line in lines)
        {
            var first = "";
            var last = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    if (string.IsNullOrEmpty(first))
                    {
                        first = line[i].ToString();
                    }
                    else
                    {
                        last = line[i].ToString();
                    }

                }
            }
            var index = $"{first}{(last == "" ? first : last)}";
            value += int.Parse(index);
        }
        return new SolutionResult(value.ToString());
    }

    [Solveable("2023/Puzzles/Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var value = 0;
        foreach (var line in lines)
        {
            var first = "";
            var last = "";
            var wordNumber = "";
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    if (string.IsNullOrEmpty(first))
                    {
                        first = line[i].ToString();
                    }
                    else
                    {
                        last = line[i].ToString();
                    }
                    wordNumber = "";
                }
                else
                {
                    wordNumber += line[i];
                    var pair = _numbers.FirstOrDefault(p => wordNumber.Contains(p.Key));
                    if (pair.Key != null)
                    {
                        if (string.IsNullOrEmpty(first))
                        {
                            first = pair.Value.ToString();
                        }
                        else
                        {
                            last = pair.Value.ToString();
                        }
                        wordNumber = line[i].ToString();
                    }
                }
            }
            var index = $"{first}{(last == "" ? first : last)}";
            value += int.Parse(index);
        }
        return new SolutionResult(value.ToString());
    }

}