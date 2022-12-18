namespace AoC2022;
public static class Day18
{
    public static IEnumerable<Cube> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var sides = l.Split(',');
            yield return new Cube
            (
                int.Parse(sides[0]),
                int.Parse(sides[1]),
                int.Parse(sides[2])
            );
        }
    }

    public static (int result, List<Cube> cubes) SolvePart1(string filename)
    {
        var cubes = ParseInput(filename).ToList();
        var queue = new Queue<Cube>(cubes);
        while (queue.Any())
        {
            var cube = queue.Dequeue();
            foreach (var c in cubes.Except(new List<Cube> { cube }))
            {
                var side = cube.IsAdjacent(c);
                if (side == Cube.Side.None)
                {
                    continue;
                }
                cube.FreeSides.Remove(side);
            }
        }
        return (cubes.Sum(_ => _.FreeSides.Count), cubes);
    }

    public static int SolvePart2(string filename)
    {
        var emptyCubes = new List<Cube>();
        var (result, cubes) = SolvePart1(filename);
        foreach (var cube in cubes)
        {
            var surroundingCubes = GetSurrounding(cube);
            foreach (var s in surroundingCubes)
            {
                if (cubes.Contains(s) || emptyCubes.Contains(s))
                {
                    continue;
                }
                emptyCubes.Add(s);
            }
        }
        var airCubes = new List<Cube>();
        foreach (var c in emptyCubes)
        {
            foreach (var c1 in cubes.Where(_ => !c.Equals(_)))
            {
                // Check if c and c1 are adjacent
                if (c.X == c1.X && c.Y == c1.Y && (c.Z == c1.Z - 1 || c.Z == c1.Z + 1))
                {
                    // c and c1 are adjacent in the Z direction
                    // Remove the side of c that is adjacent to c1
                    c.FreeSides.Remove(c.Z < c1.Z ? Cube.Side.Back : Cube.Side.Front);
                }
                else if (c.X == c1.X && (c.Y == c1.Y - 1 || c.Y == c1.Y + 1) && c.Z == c1.Z)
                {
                    // c and c1 are adjacent in the Y direction
                    // Remove the side of c that is adjacent to c1
                    c.FreeSides.Remove(c.Y < c1.Y ? Cube.Side.Top : Cube.Side.Bottom);
                }
                else if ((c.X == c1.X - 1 || c.X == c1.X + 1) && c.Y == c1.Y && c.Z == c1.Z)
                {
                    // c and c1 are adjacent in the X direction
                    // Remove the side of c that is adjacent to c1
                    c.FreeSides.Remove(c.X < c1.X ? Cube.Side.Right : Cube.Side.Left);
                }
                if (!airCubes.Contains(c) && c.FreeSides.Count() == 0)
                {
                    airCubes.Add(c);
                }
            }
        }
        return cubes.Select(_ => _.FreeSides.Count()).Sum() - airCubes.Count() * 6;
        //return result - (airCubes.Count(_ => _.FreeSides.Count() == 0) * 6);
        static List<Cube> GetSurrounding(Cube c)
        {
            return new List<Cube>
            {
                new Cube(c.X - 1, c.Y, c.Z),
                new Cube(c.X + 1, c.Y, c.Z),
                new Cube(c.X, c.Y - 1, c.Z),
                new Cube(c.X, c.Y + 1, c.Z),
                new Cube(c.X, c.Y, c.Z-1),
                new Cube(c.X, c.Y, c.Z+1),
            };
        }
    }
}

public class Cube
{
    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Z { get; private set; }
    public List<Side> FreeSides { get; set; } = new List<Side> { Side.Top, Side.Bottom, Side.Left, Side.Right, Side.Front, Side.Back };
    public Side IsAdjacent(Cube other)
    {
        if (Math.Abs(X - other.X) == 1 && Y == other.Y && Z == other.Z)
        {
            return X < other.X ? Side.Right : Side.Left;
        }
        if (Math.Abs(Y - other.Y) == 1 && X == other.X && Z == other.Z)
        {
            return Y < other.Y ? Side.Top : Side.Bottom;
        }
        if (Math.Abs(Z - other.Z) == 1 && X == other.X && Y == other.Y)
        {
            return Z < other.Z ? Side.Front : Side.Back;
        }
        return Side.None;

    }

    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right,
        Front,
        Back,
        None
    }

    public override bool Equals(object? obj)
    {
        var c = (Cube)obj!;
        if (c == null)
        {
            return false;
        }
        return c.X == X && c.Y == Y && c.Z == Z;
    }

    public override int GetHashCode() =>
        base.GetHashCode();

}
