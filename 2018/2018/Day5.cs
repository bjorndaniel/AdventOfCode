namespace AoC2018;
public class Day5
{
    public static string ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines[0];
    }

    [Solveable("2018/Puzzles/Day5.txt", "Day5 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        input = Reduce(input);
        return new SolutionResult(input.Length.ToString());
    }

    private static string Reduce(string input)
    {
        var didRemove = true;
        while (didRemove)
        {
            var current = input.Length;
            for (var i = 0; i < input.Length - 1; i++)
            {
                if (input[i] != input[i + 1] && (input[i] == char.ToLower(input[i + 1]) || char.ToLower(input[i]) == input[i + 1]))
                {
                    input = input.Remove(i, 2);
                }
            }
            if (current == input.Length)
            {
                didRemove = false;
            }
        }

        return input;
    }

    [Solveable("2018/Puzzles/Day5.txt", "Day5 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var alphabetArray = new List<(char, char)>();
        for (int i = 0; i < 26; i++)
        {
            alphabetArray.Add(((char)(97 + i), (char)(65 + i)));
        }
        var input = ParseInput(filename);
        var shortest = int.MaxValue;
        foreach(var tuple in alphabetArray)
        {
            var c = tuple.Item1;
            var c2 = tuple.Item2;
            var newInput = input.Replace(c.ToString(), "").Replace(c2.ToString(), "");
            var reduced = Reduce(newInput);
            if (reduced.Length < shortest)
            {
                shortest = reduced.Length;
            }
        }
        return new SolutionResult(shortest.ToString());
    }

}