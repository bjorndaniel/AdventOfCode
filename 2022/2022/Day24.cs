namespace AoC2022;
public static class Day24
{
    public static ValleyPoint[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new ValleyPoint[lines.Length, lines[0].Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[row, col] = new();
                result[row, col].Occupants.Add(lines[row][col]);
            }
        }
        return result;
    }

    public static int SolvePart1(string filename, IPrinter printer)
    {
        var map = ParseInput(filename);
        var rows = map.GetLength(1) - 2;//Remove the walls
        var cols = map.GetLength(0) - 2;//Remove the walls
        var lcm = (int)BigInteger.Abs(BigInteger.Multiply(rows, cols) / BigInteger.GreatestCommonDivisor(rows, cols));
        printer.Print($"LCM for map: {lcm}");
        printer.Flush();
        var moves = 0;
        var (x, y) = (0, 1);
        map[x, y].AddOccupant('E');
        printer.PrintMatrix(map);
        printer.Flush();



        return moves;
    }
}

public class ValleyPoint
{
    public List<char?> Occupants { get; set; } = new();

    public void AddOccupant(char occupant)
    {
        if (Occupants.All(_ => _ == '.'))
        {
            Occupants.Clear();
            Occupants.Add(occupant);
        }
        else if (Occupants.Any(_ => _ == '#'))
        {
            throw new ArgumentException("Cannot add occupant to a wall");
        }
        else
        {
            Occupants.Add(occupant);
        }
    }

    public bool Skip() =>
        Occupants.All(_ => _ == '.' || _ == '#' || _ == 'E');

    public override string ToString() =>
        Occupants.Count() > 1 ? Occupants.Count().ToString() : Occupants[0]?.ToString() ?? ".";

    public void RemoveOccupant(char c)
    {
        Occupants.Remove(c);
        if (Occupants.Count == 0)
        {
            Occupants.Add('.');
        }
    }
}