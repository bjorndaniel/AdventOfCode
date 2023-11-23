using System.Text;

namespace AoC2018;
public class Day2
{
    public static List<string> ParseInput(string filename) =>
        File.ReadAllLines(filename).ToList();

    [Solveable("2018/Puzzles/Day2.txt", "Day 2 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var dictionary = new Dictionary<int, int>();
        foreach (var l in lines)
        {

            var charCount = l
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
            var twice = charCount.Where(_ => _.Value == 2).Count();
            var thrice = charCount.Where(_ => _.Value == 3).Count();
            if(twice > 0)
            {
                AddOrIncrement(ref dictionary, 2, twice);
            }
            if(thrice > 0)
            {
                AddOrIncrement(ref dictionary, 3, thrice);
            }
        }
        return new SolutionResult((dictionary[2] * dictionary[3]).ToString());
        void AddOrIncrement(ref Dictionary<int,int> dictionary, int key , int count) 
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
            }
            else
            {
                dictionary[key] = 1;
            }
        }
    }

    [Solveable("2018/Puzzles/Day2.txt", "Day 2 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);

        var differingLines = new List<string>();
        for (int i = 0; i < lines.Count - 1; i++)
        {
            for (int j = i + 1; j < lines.Count; j++)
            {
                int diffCount = 0;
                for (int k = 0; k < lines[i].Length; k++)
                {
                    if (lines[i][k] != lines[j][k])
                    {
                        diffCount++;
                        if (diffCount > 1)
                        {
                            break;
                        }
                    }
                }
                if (diffCount == 1)
                {
                    differingLines.Add(lines[i]);
                    differingLines.Add(lines[j]);
                    break;
                }
            }
        }
        if(differingLines.Count != 2)
        {
            new ArgumentException("Expected only 2 lines to differ");
        }
        var builder = new StringBuilder();
        var first = differingLines.First();
        var second = differingLines.Last();
        for(int i = 0; i < first.Length; i++)
        {
            if (second[i] == first[i])
            {
                builder.Append(first[i]);
            }
        }
        
        
        return new SolutionResult(builder.ToString());
    }


}

