namespace AoC2023;
public class Day3
{
    public static Schematic ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var schematic = new Schematic(new List<Part>(), new List<Symbol>());
        for (int y = 0; y < lines.Length; y++)
        {
            var currentNumber = "";
            var width = 0;
            for (int x = 0; x < lines[y].Length; x++)
            {
                var c = lines[y][x];
                if(char.IsNumber(c))
                {
                    currentNumber += c;
                    width++;
                }
                else
                {
                    if (int.TryParse(currentNumber, out var number))
                    {
                        schematic.Parts.Add(new Part(x - width, y, width, number));
                        currentNumber = "";
                        width = 0;
                    }
                    if (c != '.')
                    {
                        schematic.Symbols.Add(new Symbol(c, x, y, new List<Part>()));
                    }
                }
            }
            if (int.TryParse(currentNumber, out var remaining))
            {
                schematic.Parts.Add(new Part(lines[y].Length - width, y, width, remaining));
            }
        }
        return schematic;
    }

    [Solveable("2023/Puzzles/Day3.txt", "Day 3 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var schematic = ParseInput(filename);   
        var adjacent = schematic.GetAdjacent();
        return new SolutionResult(adjacent.Sum(_ => _.Value).ToString());

    }

    [Solveable("2023/Puzzles/Day3.txt", "Day 3 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var schematic = ParseInput(filename);
        var adjacent = schematic.GetAdjacent();
        var stars = schematic.Symbols.Where(_ => _.C == '*' && _.AdjacentTo.Count == 2);
        var result = stars.Sum(_ => _.AdjacentTo.Select(_ => _.Value).Aggregate((a,b) => a*b));
        return new SolutionResult(result.ToString());
    }

    public record Schematic(List<Part> Parts, List<Symbol> Symbols)
    {
        public List<Part> GetAdjacent()
        {
            var adjacent = new List<Part>();
            foreach (var part in Parts)
            {
                for (var i = 0; i < part.Width; i++)
                {
                    var x = part.X + i;
                    var symbol = Symbols.FirstOrDefault(symbol => Math.Abs(symbol.X - x) <= 1 && Math.Abs(symbol.Y - part.Y) <= 1);
                    if (symbol != null)
                    {
                        adjacent.Add(part);
                        symbol.AdjacentTo.Add(part);
                        break;
                    }
                }
            }
            return adjacent;
        }
    }
    public record Part(int X, int Y, int Width, int Value) { }
    public record Symbol(char C, int X, int Y, List<Part> AdjacentTo) { }
}