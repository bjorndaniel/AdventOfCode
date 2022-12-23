namespace AoC2022;
public static class Day17
{
    public static IEnumerable<Direction> ParseInput(string filename)
    {
        var result = new List<Direction>();
        var lines = File.ReadAllLines(filename);
        return lines.First().Select(_ => _ == '>' ? Direction.Right : Direction.Left);
    }

    public static long SolvePart1(string filename, long nrOfRocks, IPrinter printer)
    {
        var key = new List<string>();
        var chamber = new Chamber();
        var input = ParseInput(filename);
        var moveCount = 0;
        var rockCount = 0;
        Rock? current = null;
        var totalRocks = 1;
        var compareRows = new Dictionary<string, int>();
        var currentRow = new StringBuilder();

        var watch = new Stopwatch();
        watch.Start();
        while (totalRocks < nrOfRocks)
        {
            if (current == null)
            {
                current = GetRock(rockCount);
                rockCount++;
                totalRocks++;
                if (rockCount > 4)
                {
                    rockCount = 0;
                }
                current.BottomLeft = chamber.GetNextDrop();
                chamber.Rocks.Add(current);
                var removeBelow = chamber.CurrentBottom - 100;
                chamber.Rocks = chamber.Rocks.Where(_ => _.BottomLeft.Y >= removeBelow).ToList();
                continue;
            }
            var nextMove = input.ElementAt(moveCount);
            moveCount++;
            if (moveCount >= input.Count())
            {
                moveCount = 0;
            }
            chamber.TryMove(nextMove, current);
            var couldDrop = chamber.TryDrop(current, currentRow);
            if (!couldDrop)
            {
                //chamber.Flatten(current, currentRow, nextMove);
                current = null;

                //if (totalRocks % 100 == 0 && totalRocks > 1500)
                //{
                //    if (compareRows.ContainsKey(currentRow.ToString()))
                //    {
                //        printer.Print(currentRow.ToString());
                //        printer.Flush();
                //        var cycleHeight = compareRows[currentRow.ToString()];
                //        var cycleLength = totalRocks - cycleHeight;
                //        var fullCycles = totalRocks / cycleLength;

                //        var height = (nrOfRocks / fullCycles) * (chamber.Height - cycleHeight);
                //        return height;
                //    }
                //    else
                //    {
                //        compareRows.Add(currentRow.ToString(), chamber.Height);
                //        currentRow = new StringBuilder();
                //    }
                //}
            }
        }
        return (long)chamber.Height;
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

    public void Flatten(Rock current, StringBuilder sb, Day17.Direction move)
    {
        for (int i = current.Points.Min(_ => _.Y); i < current.Points.Max(_ => _.Y) + 1; i++)
        {
            for (int j = 1; j < Width + 1; j++)
            {
                if (Rocks.Any(_ => _.Points.Any(_ => _.X == j && _.Y == i)))
                {
                    sb.Append("#");
                }
                else
                {
                    sb.Append(".");
                }
            }
        }
        sb.Append(move);
    }
    //var sb = new StringBuilder();
    //for (int row = Height; row >= (start ? 0 : 100); row--)
    //{
    //    for (int col = 0; col < 9; col++)
    //    {
    //        if (col == 0 || col == 8)
    //        {
    //            //printer.Print("|");
    //            continue;
    //        }
    //        else if (row == 0)
    //        {
    //            //printer.Print("-");
    //        }
    //        else if (Rocks.Any(_ => _.Points.Any(_ => _.X == col && _.Y == row)))
    //        {
    //            sb.Append("#");
    //        }
    //        else
    //        {
    //            sb.Append(".");
    //            //printer.Print(".");
    //        }
    //    }
    //    //printer.Flush();
    //}
    //return sb.ToString();
    //}

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
        if (positionsNeeded.Any(_ => _.Y == 0))
        {
            return;
        }
        if (positionsNeeded.Any(_ => _.X < 1 || _.X > 7))
        {
            return;
        }
        if (positionsNeeded.Intersect(other).Any())
        {
            return;
        }
        current.Move(nextMove);
    }

    public bool TryDrop(Rock current, StringBuilder sb)
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
    public Rock(RockFormation formation)
    {
        Formation = formation;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public Point BottomLeft { get; set; }

    public List<Point> Points =>
        Formation switch
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
                new Point(BottomLeft.X + 1, BottomLeft.Y),
                new Point(BottomLeft.X , BottomLeft.Y + 1),
                new Point(BottomLeft.X  + 1, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 1, BottomLeft.Y + 2)
            },
            RockFormation.ReverseL => new List<Point>
            {
                new Point(BottomLeft.X, BottomLeft.Y),
                new Point(BottomLeft.X + 1, BottomLeft.Y),
                new Point(BottomLeft.X + 2, BottomLeft.Y),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 1),
                new Point(BottomLeft.X + 2, BottomLeft.Y + 2)
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
            _ => throw new Exception($"Unexpected formation {Formation}")
        };

    public RockFormation Formation { get; }

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
