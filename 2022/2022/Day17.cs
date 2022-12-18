namespace AoC2022;
public static class Day17
{
    public static IEnumerable<Direction> ParseInput(string filename)
    {
        var result = new List<Direction>();
        var lines = File.ReadAllLines(filename);
        return lines.First().Select(_ => _ == '>' ? Direction.Right : Direction.Left);
    }

    public static Chamber SolvePart1(string filename, int nrOfRocks, IPrinter printer)
    {
        var chamber = new Chamber();
        var input = ParseInput(filename);
        var moveCount = 0;
        var rockCount = 0;
        Rock? current = null;
        var totalRocks = 0;
        while (totalRocks < nrOfRocks)
        {
            if (current == null)
            {
                current = GetRock(rockCount);
                rockCount++;
                totalRocks++;
                if (rockCount >= 4)
                {
                    rockCount = 0;
                }

                current.BottomLeft = chamber.GetNextDrop();
                chamber.Rocks.Add(current);
                chamber.Print(printer);
                continue;
            }
            var nextMove = input.ElementAt(moveCount);
            moveCount++;
            if (moveCount >= input.Count())
            {
                moveCount = 0;
            }
            chamber.TryMove(nextMove, current);
            var couldDrop = chamber.TryDrop(current);
            if (!couldDrop)
            {
                current = null;
            }
            chamber.Print(printer);
        }
        return chamber;
        static Rock GetRock(int count) =>
            count switch
            {
                0 => new Rock(RockFormation.HLine),
                1 => new Rock(RockFormation.Plus),
                2 => new Rock(RockFormation.ReverseL),
                3 => new Rock(RockFormation.VLine),
                4 => new Rock(RockFormation.Square),
                _ => throw new Exception($"Unexpected rock count {count}")
            }; ;
    }

    public enum Direction
    {
        Left,
        Right,
        Down
    }

    public enum RockFormation
    {
        HLine,
        Plus,
        ReverseL,
        VLine,
        Square
    }
}

public class Chamber
{
    public static int Width => 7;

    public int Height =>
        Rocks.Max(_ => _.Points.Max(_ => _.Y));

    public List<Rock> Rocks { get; set; } = new();

    public int CurrentBottom =>
       !Rocks.Any() ? 0 : Rocks.Max(_ => _.Points.Max(p => p.Y));

    public Point GetNextDrop() =>
        new(3, CurrentBottom == 0 ? 4 : CurrentBottom + 4);

    public void Print(IPrinter printer)
    {
        for (int row = Height; row >= 0; row--)
        {
            for (int col = 0; col < 9; col++)
            {
                if (col == 0 || col == 8)
                {
                    printer.Print("|");
                    continue;
                }
                else if (row == 0)
                {
                    printer.Print("-");
                }
                else if (Rocks.Any(_ => _.Points.Any(_ => _.X == col && _.Y == row)))
                {
                    printer.Print("#");
                }
                else
                {
                    printer.Print(".");
                }
            }
            printer.Flush();
        }
        printer.Flush();
    }

    public void TryMove(Day17.Direction nextMove, Rock current)
    {
        var positionsNeeded = current.PositionsNeeded(nextMove).ToList();
        var other = Rocks.Except(new List<Rock> { current }).SelectMany(_ => _.Points);
        var positonsNeeded = current.PositionsNeeded(Day17.Direction.Down);
        if (positonsNeeded.Any(_ => _.Y == 0))
        {
            return;
        }
        if (positionsNeeded.Any(_ => _.X < 1 || _.X > 7))
        {
            return;
        }
        if (positonsNeeded.Intersect(other).Any())
        {
            return;
        }
        current.Move(nextMove);
    }

    public bool TryDrop(Rock current)
    {
        var other = Rocks.Except(new List<Rock> { current }).SelectMany(_ => _.Points);
        var positonsNeeded = current.PositionsNeeded(Day17.Direction.Down);
        if (positonsNeeded.Any(_ => _.Y == 0))
        {
            return false;
        }
        if (positonsNeeded.Intersect(other).Any())
        {
            return false;
        }
        current.Drop();
        return true;
    }
}

public class Rock
{
    private readonly RockFormation _formation;

    public Rock(RockFormation formation)
    {
        _formation = formation;
    }

    public Point BottomLeft { get; set; }

    public List<Point> Points =>
        _formation switch
        {
            RockFormation.HLine => new List<Point>
            {
                new Point(BottomLeft.X, BottomLeft.Y),
                new Point(BottomLeft.X + 1, BottomLeft.Y),
                new Point(BottomLeft.X + 2, BottomLeft.Y),
                new Point(BottomLeft.X + 3, BottomLeft.Y)
            },
            RockFormation.Plus => new List<Point>
            {
                new Point(BottomLeft.X + 2, BottomLeft.Y),
                new Point(BottomLeft.X + 1, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 3, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 2)
            },
            RockFormation.ReverseL => new List<Point>
            {
                new Point(BottomLeft.X, BottomLeft.Y),

                new Point(BottomLeft.X + 1, BottomLeft.Y),
                new Point(BottomLeft.X + 2, BottomLeft.Y),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 1)
            },
            RockFormation.VLine => new List<Point>
            {
                new Point(BottomLeft.X, BottomLeft.Y),
                new Point(BottomLeft.X, BottomLeft.Y + 1),
                new Point(BottomLeft.X, BottomLeft.Y + 2),
                new Point(BottomLeft.X, BottomLeft.Y + 3)
            },
            RockFormation.Square => new List<Point>
            {
                new Point(BottomLeft.X, BottomLeft.Y),
                new Point(BottomLeft.X + 1, BottomLeft.Y),
                new Point(BottomLeft.X, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 1, BottomLeft.Y + 1)
            },
            _ => throw new Exception($"Unexpected formation {_formation}")
        };

    public void Move(Day17.Direction nextMove)
    {
        var movement = nextMove == Day17.Direction.Left ? -1 : 1;
        BottomLeft = new Point(BottomLeft.X + movement, BottomLeft.Y);
    }

    public void Drop() =>
        BottomLeft = new Point(BottomLeft.X, BottomLeft.Y - 1);

    public bool IsOnPosition(Point p) =>
        Points.Any(_ => _.X == p.X && _.Y == p.Y);

    public List<Point> PositionsNeeded(Day17.Direction nextMove) =>
        nextMove switch
        {
            Day17.Direction.Left => Points.Select(_ => new Point(_.X - 1, _.Y)).ToList(),
            Day17.Direction.Right => Points.Select(_ => new Point(_.X + 1, _.Y)).ToList(),
            Day17.Direction.Down => Points.Select(_ => new Point(_.X, _.Y - 1)).ToList(),
            _ => throw new ArgumentException("Unexpected direction"),
        };

}
