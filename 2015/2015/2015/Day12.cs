namespace AoC2015;
public class Day12
{
    public static string ParseInput(string filename) =>
        File.ReadAllText(filename);

    [Solveable("2015/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var numbers = Regex.Matches(input, @"-?\d+")
            .Select(m => int.Parse(m.Value))
            .ToList();
        return new SolutionResult($"{numbers.Sum()}");
    }

    [Solveable("2015/Puzzles/Day12.txt", "Day 12 part 2", 12)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var doc = JsonDocument.Parse(input);
        var sum = SumJson(doc.RootElement);
        return new SolutionResult(sum.ToString());
    }

    private static int SumJson(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var prop in element.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.String && prop.Value.GetString() == "red")
                    {
                        return 0;
                    }
                }
                var objSum = 0;
                foreach (var prop in element.EnumerateObject())
                {
                    objSum += SumJson(prop.Value);
                }
                return objSum;
            case JsonValueKind.Array:
                var arrSum = 0;
                foreach (var item in element.EnumerateArray())
                {
                    arrSum += SumJson(item);
                }
                return arrSum;
            case JsonValueKind.Number:
                return element.GetInt32();
            default:
                return 0;
        }
    }
}