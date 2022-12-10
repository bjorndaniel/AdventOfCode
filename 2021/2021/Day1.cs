namespace Advent2021;
public class Day1
{
    public static int CountIncreases(string filename)
    {
        var depths = GetInput(filename);
        var increases = 0;
        int? previous = null;
        depths.ForEach(d =>
        {
            increases = d > previous ? ++increases : increases;
            previous = d;
        });
        return increases;
    }

    public static int CountIncreasesBy3(string filename)
    {
        var depths = GetInput(filename);
        int? previous = null;
        var increases = 0;
        for (int i = 0; i < depths.Count() - 2; i++)
        {
            var g = depths.Skip(i).Take(3);
            if (previous.HasValue && g.Sum() > previous)
            {
                increases++;
            }
            previous = g.Sum();
        }
        return increases;
    }

    private static List<int> GetInput(string filename)
    {
        using var reader = new StreamReader(filename);
        var content = reader.ReadToEnd();
        var depths = JsonSerializer.Deserialize<Day1Json>(content)?.Input ?? new();
        return depths;
    }

    private class Day1Json
    {
        public List<int> Input { get; set; } = new();
    }
}
