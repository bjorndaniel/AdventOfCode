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
        var bounds = GetBounds(cubes);
        var filled = FloodFill(bounds.min, cubes, bounds);
        var many = cubes.SelectMany(GetSurrounding).ToList();
        var x = many.Count(_ => filled.Contains(_));
        return x;
        IEnumerable<Cube> FloodFill(Cube cube, List<Cube> cubes, (Cube min, Cube max) bounds)
        {
            var queue = new Queue<Cube>();
            queue.Enqueue(cube);
            var result = new List<Cube>();
            result.Add(cube);
            while (queue.Any())
            {
                var c = queue.Dequeue();
                foreach (var surround in GetSurrounding(c))
                {
                    if (!result.Contains(surround) && !cubes.Contains(surround) && InBounds(bounds, surround))
                    {
                        result.Add(surround);
                        queue.Enqueue(surround);
                    }
                }
            }
            return result;
        }

        static (Cube min, Cube max) GetBounds(IEnumerable<Cube> cubes)
        {
            var minX = cubes.Select(p => p.X).Min() - 1;
            var maxX = cubes.Select(p => p.X).Max() + 1;

            var minY = cubes.Select(p => p.Y).Min() - 1;
            var maxY = cubes.Select(p => p.Y).Max() + 1;

            var minZ = cubes.Select(p => p.Z).Min() - 1;
            var maxZ = cubes.Select(p => p.Z).Max() + 1;

            return (new Cube(minX, minY, minZ), new Cube(maxX, maxY, maxZ));
        }


        static bool InBounds((Cube min, Cube max) surrounding, Cube cube) =>
            surrounding.min.Y <= cube.X && cube.X <= surrounding.max.X &&
            surrounding.min.Y <= cube.Y && cube.Y <= surrounding.max.Y &&
            surrounding.min.Z <= cube.Z && cube.Z <= surrounding.max.Z;

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


