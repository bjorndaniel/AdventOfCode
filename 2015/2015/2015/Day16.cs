namespace AoC2015;

public class Day16
{
    public static List<Aunt> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Aunt>();
        foreach (var line in lines)
        {
            var parts = line.Split([' ', ':', ','], StringSplitOptions.RemoveEmptyEntries);
            var aunt = new Aunt
            {
                Number = int.Parse(parts[1])
            };
            for (int i = 2; i < parts.Length; i += 2)
            {
                aunt.Properties[parts[i]] = int.Parse(parts[i + 1]);
            }
            result.Add(aunt);
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day16.txt", "Day 16 part 1", 16)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var aunts = ParseInput(filename);
        foreach (var aunt in aunts)
        {
            var matches = true;
            foreach (var prop in aunt.Properties)
            {
                if (TargetProperties[prop.Key] != prop.Value)
                {
                    matches = false;
                    break;
                }
            }
            if (matches)
            {
                return new SolutionResult(aunt.Number.ToString());
            }
        }
        return new("No match found");
    }

    [Solveable("2015/Puzzles/Day16.txt", "Day 16 part 2", 16)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var aunts = ParseInput(filename);
        foreach (var aunt in aunts)
        {
            var matches = true;
            foreach (var prop in aunt.Properties)
            {
                if (prop.Key == "cats" || prop.Key == "trees")
                {
                    if (TargetProperties[prop.Key] >= prop.Value)
                    {
                        matches = false;
                        break;
                    }
                }
                else if (prop.Key == "pomeranians" || prop.Key == "goldfish")
                {
                    if (TargetProperties[prop.Key] <= prop.Value)
                    {
                        matches = false;
                        break;
                    }
                }
                else
                {
                    if (TargetProperties[prop.Key] != prop.Value)
                    {
                        matches = false;
                        break;
                    }
                }
            }
            if (matches)
            {
                return new SolutionResult(aunt.Number.ToString());
            }
        }
        return new("No match found");
    }

    private static readonly Dictionary<string, int> TargetProperties = new()
{
    { "children", 3 },
    { "cats", 7 },
    { "samoyeds", 2 },
    { "pomeranians", 3 },
    { "akitas", 0 },
    { "vizslas", 0 },
    { "goldfish", 5 },
    { "trees", 3 },
    { "cars", 2 },
    { "perfumes", 1 }
};
}

public class Aunt
{
    public int Number { get; set; }
    public Dictionary<string, int> Properties { get; set; } = new();
}

