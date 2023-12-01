namespace AoC2017;
public class Day2
{
    public static List<List<int>> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<List<int>>();
        foreach (var line in lines)
        {
            var cleanedLine = Regex.Replace(line, @"\s+", " ");
            result.Add(cleanedLine.Split([' ']).Select(int.Parse).ToList());
        }
        return result;
    }

    [Solveable("2017/Puzzles/Day2.txt", "Day 2 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        return new SolutionResult(input.Select(_ => _.Max() - _.Min()).Sum().ToString());
    }

    [Solveable("2017/Puzzles/Day2.txt", "Day 2 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = 0;
        foreach(var line in input)
        {
            for (int i = 0; i < line.Count; i++)
            {
                for (int j = i + 1; j < line.Count; j++)
                {
                    if (line[i] % line[j] == 0)
                    {
                        result += line[i] / line[j];
                    }
                    else if(line[j] % line[i] == 0)
                    {
                        result += line[j] / line[i];
                    }
                }
            }
        }
        return new SolutionResult(result.ToString());
    }


}