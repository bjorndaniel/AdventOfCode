namespace AoC2015;
public class Day2
{
    public static List<Present> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Present>();
        foreach (var line in lines)
        {
            var dimensions = line.Split('x');
            var present = new Present(int.Parse(dimensions[0]), int.Parse(dimensions[1]), int.Parse(dimensions[2]));
            result.Add(present);
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day2.txt", "Day 2 part 1", 2)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var presents = ParseInput(filename);
        var wrapping = presents.Sum(_ => _.WrappingPaper());
        return new SolutionResult(wrapping.ToString());
    }

    [Solveable("2015/Puzzles/Day2.txt", "Day 2 part 2", 2)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var presents = ParseInput(filename);    
        var ribbon = presents.Sum(_ => _.Ribbon());
        return new SolutionResult(ribbon.ToString());
    }

    public record Present(int Length, int Width, int Height)
    {
        public int WrappingPaper()
        {
            var volume = 2 * Length * Width + 2 * Width * Height + 2 * Height * Length;
            var side1 = Length * Width;
            var side2 = Width * Height;
            var side3 = Height * Length;
            var smallestSide = Math.Min(Math.Min(side1, side2), side3);
            return volume + smallestSide;
        }

        public int Ribbon()
        {
            var perimeter1 = 2 * Length + 2 * Width;
            var perimeter2 = 2 * Width + 2 * Height;
            var perimeter3 = 2 * Height + 2 * Length;
            var smallestPerimeter = Math.Min(Math.Min(perimeter1, perimeter2), perimeter3);
            var bow = Length * Width * Height;
            return smallestPerimeter + bow;
        }
    }
}