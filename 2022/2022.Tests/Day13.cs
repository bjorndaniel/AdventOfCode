namespace AoC2022.Tests;
public class Day13
{
    public static List<Pair> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Pair>();
        for (int i = 0; i < lines.Length - 1; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }

            var left = lines[i][1..(lines[i].Length -1)];
            var right = lines[i+1][1..(lines[i+1].Length - 1)];
            i++;
            result.Add(new Pair(left, right));
        }
        return result;
    }
    public static int SolvePart1(string filename)
    {
        var pairs = ParseInput(filename);
        var result = 0;
        foreach(var p in pairs)
        {
            if (ComparePair(p))
            {
                result++;
            }
        }

        return result;
    }
    public static bool ComparePair(Pair p)
    {
        return false;
    }
}


public record Pair(string Left, string Right);

