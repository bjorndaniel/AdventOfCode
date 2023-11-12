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
        for (int i = 0; i < 10; i++)
        {
//            north is positive y south negative y west is negative x and east is positive x

//check the elf according to this:If there is no Elf in the N, NE, or NW adjacent positions, the Elf proposes moving north one step.If there is no Elf in the S, SE, or SW adjacent positions, the Elf proposes moving south one step.If there is no Elf in the W, NW, or SW adjacent positions, the Elf proposes moving west one step.If there is no Elf in the E, NE, or SE adjacent positions, the Elf proposes moving east one step. if there is no elf with this position in the proposedonce add it there otherwise check proposedTwice and if there is an elf there do nothing
            var proposedOnce = new List<Elf>();
            var proposedTwice = new List<Elf>();
            for (int j = 0; j < elves.Count(); j++)
            {
                var elf = elves[j];
                var north = elves.FirstOrDefault(e => e.X == elf.X && e.Y == elf.Y - 1);
                var northEast = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y - 1);
                var northWest = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y - 1);
                var south = elves.FirstOrDefault(e => e.X == elf.X && e.Y == elf.Y + 1);
                var southEast = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y + 1);
                var southWest = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y + 1);
                var west = elves.FirstOrDefault(e => e.X == elf.X - 1 && e.Y == elf.Y);
                var east = elves.FirstOrDefault(e => e.X == elf.X + 1 && e.Y == elf.Y);

                if (north == null && northEast == null && northWest == null)
                {
                    if(proposedOnce.Any(_ => _.Equals(elf)))
                    {
                        if(!proposedTwice.Any(_ => _.Equals(elf)))
                        {
                            proposedTwice.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                        }
                    }
                    else
                    {
                        proposedOnce.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                    }
                }
                else if (south == null && southEast == null && southWest == null)
                {
                    if (proposedOnce.Any(_ => _.Equals(elf)))
                    {
                        if (!proposedTwice.Any(_ => _.Equals(elf)))
                        {
                            proposedTwice.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                        }
                    }
                    else
                    {
                        proposedOnce.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                    }
                }
                else if (west == null && northWest == null && southWest == null)
                {
                    if (proposedOnce.Any(_ => _.Equals(elf)))
                    {
                        if (!proposedTwice.Any(_ => _.Equals(elf)))
                        {
                            proposedTwice.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                        }
                    }
                    else
                    {
                        proposedOnce.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                    }
                }
                else if (east == null && northEast == null && southEast == null)
                {
                    if (proposedOnce.Any(_ => _.Equals(elf)))
                    {
                        if (!proposedTwice.Any(_ => _.Equals(elf)))
                        {
                            proposedTwice.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                        }
                    }
                    else
                    {
                        proposedOnce.Add(new Elf(elf.X, elf.Y - 1, elf.Id));
                    }
                }
            }
            elves = MoveElves(elves, proposedOnce, proposedTwice);
            printer.PrintMatrix(CreateMatrix(elves));
            printer.Flush();
        }
        return (elves.Max(_ => _.X) - (elves.Min(_ => _.X) + 1) * elves.Max(_ => _.Y) - (elves.Min(_ => _.Y) + 1)) - elves.Count();
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
        int adjMinX = Math.Abs(minX);
        int adjMinY = Math.Abs(minY);
        char[,] matrix = new char[maxY + adjMinY + 1, maxX + adjMinX + 1];

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
}
