using System.Drawing;
using System.Linq;

namespace AoC2018;
public class Day3
{
    public static List<Claim> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Claim>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            var id = int.Parse(parts[0].Substring(1));
            var left = int.Parse(parts[2].Split(',')[0]);
            var top = int.Parse(parts[2].Split(',')[1].TrimEnd(':'));
            var width = int.Parse(parts[3].Split('x')[0]);
            var height = int.Parse(parts[3].Split('x')[1]);
            result.Add(new Claim(id, left, top, width, height));
        }
        return result;
    }

    [Solveable("2018/Puzzles/Day3.txt", "Day3 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var claims = ParseInput(filename);
        var coords = new Dictionary<(int, int), int>();
        foreach (var claim1 in claims)
        {
            foreach (var claim2 in claims)
            {
                if (!claim1.Equals(claim2) && claim1.Overlaps(claim2))
                    
                {
                    for (int x = claim1.Left; x < claim1.Left + claim1.Width; x++)
                    {
                        for (int y = claim1.Top; y < claim1.Top + claim1.Height; y++)
                        {
                            if (claim2.Left <= x && x < claim2.Left + claim2.Width &&
                                claim2.Top <= y && y < claim2.Top + claim2.Height)
                            {
                                if (!coords.ContainsKey((x, y)))
                                {
                                    coords.Add((x, y), 0);
                                }
                            }
                        }
                    }
                }
            }
        }
        return new SolutionResult(coords.Count().ToString());
    }

    [Solveable("2018/Puzzles/Day3.txt", "Day3 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


    public class Claim
    {
        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Claim(int id, int left, int top, int width, int height)
        {
            Id = id;
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        public bool Overlaps(Claim other)
        {
            return (Left < other.Left + other.Width &&
                    Left + Width > other.Left &&
                    Top < other.Top + other.Height &&
                    Top + Height > other.Top);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Id == ((Claim)obj).Id;
        }

        public override int GetHashCode() =>
            base.GetHashCode();
    }
}