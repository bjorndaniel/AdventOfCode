namespace AoC2023;
public class Day15
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return [.. lines[0].Split(",")];
    }

    [Solveable("2023/Puzzles/Day15.txt", "Day 15 part 1", 15)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        return new SolutionResult(input.Sum(_ => HashAlgo(_)).ToString());
    }

    [Solveable("2023/Puzzles/Day15.txt", "Day 15 part 2", 15)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var boxes = new Dictionary<int, List<(string label, int focal)>>();
        Enumerable.Range(0, 256).ToList().ForEach(_ => boxes[_] = []);
        foreach (var step in input)
        {
            var count = HashAlgo(step);
            if (step.Contains('='))
            {
                var parts = step.Split("=");
                var boxNr = HashAlgo(parts[0]);
                var box = boxes[boxNr];
                var existing = box.FirstOrDefault(_ => _.label == parts[0]);
                if (existing != default)
                {
                    var existingIndex = box.FindIndex(_ => _.label == parts[0]);
                    if (existingIndex != -1)
                    {
                        box[existingIndex] = (existing.label, int.Parse(parts[1]));
                    }
                }
                else
                {
                    box.Add((parts[0], int.Parse(parts[1])));
                }
            }
            else
            {
                var label = step.Replace("-", "");
                var boxNr = HashAlgo(label);
                var box = boxes[boxNr];
                var existing = box.FirstOrDefault(_ => _.label == label);
                if (box.Count != 0)
                {
                    box.Remove(existing);
                }
            }
        }
        var sum = 0;
        for (int i = 0; i < 256; i++)
        {
            var box = boxes[i];
            if (box.Count == 0)
            {
                continue;
            }
            var boxSum = 0;
            for (int j = 1; j < box.Count + 1; j++)
            {
                boxSum += (i+1) * (box.ElementAt(j - 1).focal * j); ;
            }
            sum += boxSum;
        }
        return new SolutionResult(sum.ToString());
    }

    private static int HashAlgo(string step)
    {
        var count = 0;
        foreach (var c in step)
        {
            count += (int)c;
            count *= 17;
            count %= 256;
        }
        return count;
    }
}