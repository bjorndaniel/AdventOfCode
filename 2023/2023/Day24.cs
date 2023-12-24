namespace AoC2023;
public class Day24
{
    public static List<Hailstone> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Hailstone>();
        foreach (var line in lines)
        {
            var parts = line.Split('@');
            var positions = parts[0].Split(',').Select(long.Parse).ToArray();
            var velocities = parts[1].Split(',').Select(long.Parse).ToArray();
            result.Add(new Hailstone(positions[0], positions[1], positions[2], velocities[0], velocities[1], velocities[2]));
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day24.txt", "Day 24 part 1", 24)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var stones = ParseInput(filename);
        var boundary = filename.Contains("test") ? (7, 7, 27, 27) : (200000000000000, 200000000000000, 400000000000000, 400000000000000);
        var count = 0;
        for (var i = 0; i < stones.Count; i++)
        {
            for (var j = i + 1; j < stones.Count; j++)
            {
                if (Intersect(stones[i], stones[j], boundary, Math.Max(boundary.Item2, boundary.Item4) * 2))
                {
                    count++;
                }
            }
        }

        return new SolutionResult(count.ToString());

        static bool Intersect(Hailstone h1, Hailstone h2, (long x1, long y1, long x2, long y2) boundary, long t)
        {
            // Calculate the end polongs
            var ex1 = h1.X + h1.Vx * t;
            var ey1 = h1.Y + h1.Vy * t;
            var ex2 = h2.X + h2.Vx * t;
            var ey2 = h2.Y + h2.Vy * t;

            // Calculate the direction of the lines
            BigInteger dx1 = ex1 - h1.X;
            BigInteger dy1 = ey1 - h1.Y;
            BigInteger dx2 = ex2 - h2.X;
            BigInteger dy2 = ey2 - h2.Y;

            // If the lines are parallel, they longersect only if they are collinear
            if (dx1 * dy2 == dy1 * dx2)
            {
                // Check if the lines are collinear
                if ((dx1 * (h2.Y - h1.Y) == dy1 * (h2.X - h1.X)) &&
                    (Math.Min(h1.X, h1.Vx) <= Math.Max(h2.X, h2.Vx) && Math.Max(h1.X, h1.Vx) >= Math.Min(h2.X, h2.Vx)))
                {
                    return true;
                }
                return false;
            }

            // Calculate longersection polong
            var x = ((h1.X * dy1 * dx2 - h2.X * dy2 * dx1) - dx1 * dx2 * (h1.Y - h2.Y)) / (dy1 * dx2 - dy2 * dx1);
            var y = ((h1.Y * dx1 * dy2 - h2.Y * dx2 * dy1) - dy1 * dy2 * (h1.X - h2.X)) / (dx1 * dy2 - dx2 * dy1);
            var tx = (x - h1.X) / h1.Vx;
            var ty = (y - h1.Y) / h1.Vy;
            var tx2 = (x - h2.X) / h2.Vx;
            var ty2 = (y - h2.Y) / h2.Vy;
            if (tx < 0 || ty < 0 || tx2 < 0 || ty2 < 0)
            {
                return false;
            }
            // Check if the longersection polong is within the boundary
            if (x >= boundary.x1 && x <= boundary.x2 && y >= boundary.y1 && y <= boundary.y2)
            {
                return true;
            }
            return false;
        }

    }

    [Solveable("2023/Puzzles/Day24.txt", "Day 24 part 2", 24)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var stones = ParseInput(filename);
        var (x, y, z) = Solve(stones);
            
        return new SolutionResult((x + y + z).ToString());
        static (long x, long y, long z) Solve(List<Hailstone> hailstones)
        {

            using var ctx = new Context();
            var rockInitialPositionX = ctx.MkIntConst("rockInitialPositionX");
            var rockInitialPositionY = ctx.MkIntConst("rockInitialPositionY");
            var rockInitialPositionZ = ctx.MkIntConst("rockInitialPositionZ");
            var rockVelocityX = ctx.MkIntConst("rockVelocityX");
            var rockVelocityY = ctx.MkIntConst("rockVelocityY");
            var rockVelocityZ = ctx.MkIntConst("rockVelocityZ");

            var collisionConstraints = hailstones.Select(hailstone =>
            {
                var collisionTime = ctx.MkIntConst($"collisionTime_{hailstone.GetHashCode()}");
                var collisionTimePositive = ctx.MkGt(collisionTime, ctx.MkInt(0));

                var rockPositionAtCollisionX = ctx.MkAdd(rockInitialPositionX, ctx.MkMul(rockVelocityX, collisionTime));
                var rockPositionAtCollisionY = ctx.MkAdd(rockInitialPositionY, ctx.MkMul(rockVelocityY, collisionTime));
                var rockPositionAtCollisionZ = ctx.MkAdd(rockInitialPositionZ, ctx.MkMul(rockVelocityZ, collisionTime));

                var hailstonePositionAtCollisionX = ctx.MkAdd(ctx.MkInt(hailstone.X), ctx.MkMul(ctx.MkInt(hailstone.Vx), collisionTime));
                var hailstonePositionAtCollisionY = ctx.MkAdd(ctx.MkInt(hailstone.Y), ctx.MkMul(ctx.MkInt(hailstone.Vy), collisionTime));
                var hailstonePositionAtCollisionZ = ctx.MkAdd(ctx.MkInt(hailstone.Z), ctx.MkMul(ctx.MkInt(hailstone.Vz), collisionTime));

                var collisionOccurs = ctx.MkAnd(ctx.MkEq(rockPositionAtCollisionX, hailstonePositionAtCollisionX), ctx.MkEq(rockPositionAtCollisionY, hailstonePositionAtCollisionY), ctx.MkEq(rockPositionAtCollisionZ, hailstonePositionAtCollisionZ));

                return ctx.MkAnd(collisionTimePositive, collisionOccurs);
            }).ToArray();

            var s = ctx.MkSolver();
            s.Add(collisionConstraints);
            if (s.Check() == Status.SATISFIABLE)
            {
                var m = s.Model;
                var x = ((IntNum)m.Evaluate(rockInitialPositionX)).Int64;
                var y = ((IntNum)m.Evaluate(rockInitialPositionY)).Int64;
                var z = ((IntNum)m.Evaluate(rockInitialPositionZ)).Int64;
                return(x, y, z);
            }
            else
            {
                throw new Exception("Unsatisfiable");
            }
        }

    }

    public record Hailstone(long X, long Y, long Z, long Vx, long Vy, long Vz) { }

}