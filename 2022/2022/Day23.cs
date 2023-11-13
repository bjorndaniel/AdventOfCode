namespace AoC2022;
public static class Day23
{
    public static List<Elf> ParseInput(string filename)
    {
        var result = new List<Elf>();
        var lines = File.ReadAllLines(filename);
        //var result = new char[lines.Length, lines[0].Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    result.Add(new Elf(j, i, Guid.NewGuid()));
                }
                //result[i, j] = lines[i][j];
            }
        }
        return result;
    }

    public static int SolvePart1(string filename, IPrinter printer)
    {
        var elves = ParseInput(filename);
        printer.PrintMatrix(CreateMatrix(elves));
        printer.Flush();
        var directions = new List<ElfDirection> { ElfDirection.North, ElfDirection.South, ElfDirection.West, ElfDirection.East };
        for (int i = 0; i < 10; i++)
        {
            var proposedOnce = new List<Elf>();
            var proposedTwice = new List<Elf>();
            for (int j = 0; j < elves.Count(); j++)
            {
                var elf = elves[j];
                foreach (var direction in directions)
                {
                    var adjacent = GetAdjacentElves(elf, elves, direction);
                    if (!adjacent.Any())
                    {
                        var proposed = new Elf(elf.X + (direction == ElfDirection.East ? 1 : direction == ElfDirection.West ? -1 : 0), elf.Y + (direction == ElfDirection.South ? 1 : direction == ElfDirection.North ? -1 : 0), elf.Id);
                        var once = proposedOnce.FirstOrDefault(_ => _.Equals(proposed));
                        if (once != null)
                        {
                            proposedOnce.Remove(once);
                            proposedTwice.Add(once);
                            proposedTwice.Add(proposed);
                        }
                        else
                        {
                            proposedOnce.Add(proposed);
                        }
                        break;
                    }
                }

            }
            elves = MoveElves(elves, proposedOnce, proposedTwice);
            printer.PrintMatrix(CreateMatrix(elves));
            printer.Flush();
            var dir = directions.First();
            directions.RemoveAt(0);
            directions.Add(dir);
            printer.Print(directions.First().ToString());
            printer.Flush();
        }
        var maxX = elves.Max(_ => _.X);
        var minX = elves.Min(_ => _.X);
        var minY = elves.Min(_ => _.Y);
        var maxY = elves.Max(_ => _.Y);
        return ((maxX - minX) + 1) * ((maxY - minY) + 1) - elves.Count();
    }

    public static List<Elf> MoveElves(List<Elf> elves, List<Elf> proposedOnce, List<Elf> proposedTwice)
    {
        var proposed = proposedOnce.Select(_ => _.Id);
        var remaining = elves.Where(_ => !proposed.Contains(_.Id)).ToList();
        proposedOnce.AddRange(remaining);
        return proposedOnce;
    }
    public static char[,] CreateMatrix(List<Elf> elves)
    {
        int maxX = elves.Max(e => e.X);
        int maxY = elves.Max(e => e.Y);
        int minX = elves.Min(e => e.X);
        int minY = elves.Min(e => e.Y);
        int adjMinX = Math.Abs(minX - 2);
        int adjMinY = Math.Abs(minY - 2);
        char[,] matrix = new char[maxY + adjMinY + 3, maxX + adjMinX + 3];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = '.';
            }
        }
        foreach (var elf in elves)
        {
            matrix[elf.Y + adjMinY, elf.X + adjMinX] = '#';
        }
        return matrix;
    }
    public static List<Elf> GetAdjacentElves(Elf elf, List<Elf> elves, ElfDirection direction)
    {
        var adjacentElves = new List<Elf>();
        switch (direction)
        {
            case ElfDirection.North:
                var n = elves.FirstOrDefault(e => e.X == elf.X && e.Y == elf.Y - 1);
                var nw = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y - 1);
                var ne = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y - 1);
                if (n != null) adjacentElves.Add(n);
                if (nw != null) adjacentElves.Add(nw);
                if (ne != null) adjacentElves.Add(ne);
                break;
            case ElfDirection.South:
                var s = elves.FirstOrDefault(e => e.X == elf.X && e.Y == elf.Y + 1);
                var sw = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y + 1);
                var se = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y + 1);
                if (s != null) adjacentElves.Add(s);
                if (sw != null) adjacentElves.Add(sw);
                if (se != null) adjacentElves.Add(se);
                break;
            case ElfDirection.West:
                var w = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y);
                var nw2 = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y - 1);
                var sw2 = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y + 1);
                if (w != null) adjacentElves.Add(w);
                if (nw2 != null) adjacentElves.Add(nw2);
                if (sw2 != null) adjacentElves.Add(sw2);
                break;
            case ElfDirection.East:
                var e = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y);
                var ne2 = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y - 1);
                var se2 = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y + 1);
                if (e != null) adjacentElves.Add(e);
                if (ne2 != null) adjacentElves.Add(ne2);
                if (se2 != null) adjacentElves.Add(se2);
                break;
        }
        return adjacentElves;
    }



    public class Elf
    {
        public Elf(int x, int y, Guid id)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public int X { get; }
        public int Y { get; }
        public Guid Id { get; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Elf)obj;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public enum ElfDirection
    {
        North,
        South,
        West,
        East
    }
}
