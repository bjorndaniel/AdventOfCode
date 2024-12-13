namespace AoC2024;
public class Day13
{
    public static List<Claw> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        List<List<string>> claws = new List<List<string>>();
        var current = new List<string>();
        var result = new List<Claw>();
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                claws.Add(current);
                current = new List<string>();
                continue;
            }
            current.Add(line);
        }
        claws.Add(current);
        foreach (var claw in claws)
        {
            var a = claw[0].Split(" ");
            var button = a[1][0];
            var x = long.Parse(a[2].Split("+").Last()[..^1]);
            var y = long.Parse(a[3].Split("+").Last());
            var b = claw[1].Split(" ");
            var x2 = long.Parse(b[2].Split("+").Last()[..^1]);
            var y2 = long.Parse(b[3].Split("+").Last());
            var button2 = b[1][0];
            var prize = claw[2].Split(" ");
            var x3 = long.Parse(prize[1].Split("=")[1][..^1]);
            var y3 = long.Parse(prize[2].Split("=").Last());
            result.Add(new Claw(new Button(x, y, button, 3), new Button(x2, y2, button2, 1), new Prize(x3, y3)));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day13.txt", "Day 13 part 1", 13)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var claws = ParseInput(filename);
        var result = 0L;
        foreach (var claw in claws)
        {
            var (acount, bcount, success) = CheckPrizePossible(claw);
            if (success)
            {
                result += (claw.A.Cost * acount) + (claw.B.Cost * bcount);
            }
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day13.txt", "Day 13 part 2", 13)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var claws = ParseInput(filename);
        var result = 0L;
        foreach (var claw in claws)
        {
            var newClaw = claw with { Price = claw.Price with { X = claw.Price.X + 10000000000000, Y = claw.Price.Y + 10000000000000 } };
            var (acount, bcount, success) = SolveUsingLinearAlgebra(newClaw);
            if (success)
            {
                result += (newClaw.A.Cost * acount) + (newClaw.B.Cost * bcount);
            }
        }

        return new SolutionResult(result.ToString());
    }

    public static (long acount, long bcount, bool success) SolveUsingLinearAlgebra(Claw claw)
    {
        var matrix = DenseMatrix.OfArray(new double[,]
        {
            { claw.A.XMovement, claw.B.XMovement },
            { claw.A.YMovement, claw.B.YMovement }
        });

        var vector = DenseVector.OfArray(
        [
            claw.Price.X,
            claw.Price.Y
        ]);

        var solution = matrix.Solve(vector);

        var acount = (long)Math.Round(solution[0]);
        var bcount = (long)Math.Round(solution[1]);

        if (acount >= 0 && bcount >= 0)
        {
            var x = acount * claw.A.XMovement + bcount * claw.B.XMovement;
            var y = acount * claw.A.YMovement + bcount * claw.B.YMovement;
            if (x == claw.Price.X && y == claw.Price.Y)
            {
                return (acount, bcount, true);
            }
        }

        return (0, 0, false);
    }

    public static (long acount, long bcount, bool success) CheckPrizePossible(Claw claw)
    {
        var directions = new (long x, long, char button)[]
        {
        (claw.A.XMovement, claw.A.YMovement, claw.A.ButtonName),
        (claw.B.XMovement, claw.B.YMovement, claw.B.ButtonName)
        };

        var queue = new PriorityQueue<(long x, long y, long acount, long bcount), long>();
        var memo = new Dictionary<(long x, long y), (long acount, long bcount)>();

        long Heuristic(long x, long y) => Math.Abs(x - claw.Price.X) + Math.Abs(y - claw.Price.Y);

        queue.Enqueue((0, 0, 0, 0), Heuristic(0, 0));
        memo[(0, 0)] = (0, 0);

        while (queue.Count > 0)
        {
            var (x, y, acount, bcount) = queue.Dequeue();

            if (x == claw.Price.X && y == claw.Price.Y)
            {
                return (acount, bcount, true);
            }

            foreach (var (dx, dy, button) in directions)
            {
                var newX = x + dx;
                var newY = y + dy;
                var newAcount = acount + (button == claw.A.ButtonName ? 1 : 0);
                var newBcount = bcount + (button == claw.B.ButtonName ? 1 : 0);

                if (newAcount > 100 || newBcount > 100)
                {
                    continue;
                }

                var newCost = newAcount + newBcount;
                if (!memo.TryGetValue((newX, newY), out var counts) || newCost < counts.acount + counts.bcount)
                {
                    var priority = newCost + Heuristic(newX, newY);
                    queue.Enqueue((newX, newY, newAcount, newBcount), priority);
                    memo[(newX, newY)] = (newAcount, newBcount);
                }
            }
        }

        return (0, 0, false);
    }
}

public record Claw(Button A, Button B, Prize Price);

public record Button(long XMovement, long YMovement, char ButtonName, long Cost);

public record Prize(long X, long Y);
