namespace AoC2022;
public class Day4
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

    [Solveable("2022/Puzzles/Day4.txt", "Day 4 part 1", 4)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var pairs = ParseInput(filename);

        return new SolutionResult(pairs.Count(_ => _.IsContained()).ToString());
    }

    [Solveable("2022/Puzzles/Day4.txt", "Day 4 part 1", 4)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var pairs = ParseInput(filename);
        return new SolutionResult(pairs.Count(_ => _.HasOverlap()).ToString());
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
}



