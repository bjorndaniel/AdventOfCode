namespace AoC2022;
public static class Day4
{
    public static IEnumerable<SectionPair> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(l =>
        {
            var pair = l.Split(",");
            var left = pair[0];
            var right = pair[1];
            return new SectionPair(CreateSection(left), CreateSection(right));
        });

        static Section CreateSection(string input)
        {
            var pair = input.Split("-");
            return new Section(int.Parse(pair[0]), int.Parse(pair[1]));
        }
    }

    public static int SolvePart1(string filename)
    {
        var pairs = ParseInput(filename);

        return pairs.Count(_ => _.IsContained());
    }

    public static int SolvePart2(string filename)
    {
        var pairs = ParseInput(filename);
        return pairs.Count(_ => _.HasOverlap());
    }

}

public record Section(int Low, int High)
{
    public bool IsContained(Section test) =>
        test.Low <= Low && test.High >= High;

    public bool Overlaps(Section test) =>
        Low <= test.High && test.Low <= High;
}

public record SectionPair(Section First, Section Second)
{
    public bool IsContained() =>
        First.IsContained(Second) || Second.IsContained(First);

    public bool HasOverlap() =>
        First.Overlaps(Second);
}


