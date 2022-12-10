namespace AoC2022;
public static class Day3
{
    public static int GetValue(char value)
    {
        if (char.IsUpper(value))
        {
            return value - 38;
        }
        return value - 96;
    }

    public static IEnumerable<Rucksack> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(l => new Rucksack(l));
    }

    public static int SolvePart1(string filename)
    {
        var backpacks = ParseInput(filename);

        var intersections = backpacks.Select(_ =>
        {
            var comps = _.Compartments();
            return comps.comp1.Intersect(comps.comp2);
        });

        return intersections.SelectMany(_ => _).Sum(_ => GetValue(_));

    }

    public static int SolvePart2(string filename)
    {
        var backpacks = ParseInput(filename);
        var groups = backpacks.Chunk(3);
        var badges = groups.Select(_ => GetBadge(_));
        return badges.Sum(_ => GetValue(_));
    }

    public static char GetBadge(Rucksack[] chunk)
    {
        var first = chunk[0].Contents.Intersect(chunk[1].Contents);
        var second = first.Intersect(chunk[2].Contents);
        return second.First();
    }
}

public record Rucksack(string Contents)
{
    public (List<char> comp1, List<char> comp2) Compartments()
    {
        var length = Contents.Length / 2;
        return (Contents[..length].ToList(), Contents[length..].ToList());
    }
}
