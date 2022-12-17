using static AoC2022.Day17;
namespace AoC2022;
public static class Day17
{
    public static IEnumerable<Direction> ParseInput(string filename)
    {
        var result = new List<Direction>();
        var lines = File.ReadAllLines(filename);
        return lines.First().Select(_ => _ == '>' ? Direction.Right : Direction.Left);
    }
    public static Chamber SolvePart1(string filename, int nrOfSteps, IPrinter printer)
    {
        var chamber = new Chamber();
        var input = ParseInput(filename);
        var moveCount = 0;
        var rockCount = 0;
        Rock? current = null;
        var position = new Point(2, 3);
        for (int i = 0; i < nrOfSteps; i++)
        {
            if (current == null)
            {
                current = GetRock(rockCount);
                rockCount++;
                if (rockCount >= 4)
                {
                    rockCount = 0;
                }
                position = chamber.GetNextDrop();
                current.BottomLeft = position;
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
            chamber.Print(printer);
            if (!couldDrop)
            {
                current = null;
            }
        }
        return chamber;
        Rock GetRock(int count) =>
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
        Right
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
    public int Width => 7;
    public int Height =>
        Rocks.Max(_ => _.TopRight.Y);
    public List<Rock> Rocks { get; set; } = new();
    public int CurrentBottom { get; private set; }
    public Point GetNextDrop() =>
        new Point(2, CurrentBottom + 3);
    public void Print(IPrinter printer)
    {
        for (int row = Height; row > -2; row--)
        {
            var rocks = Rocks.Where(_ => _.IsOnRow(row));
            for (int col = -1; col < 8; col++)
            {
                if (col == -1 || col == 7)
                {
                    printer.Print("|");
                    continue;
                }
                else if (row < 0)
                {
                    printer.Print("-");
                }
                else if (rocks.Any(_ => _.IsOnColumn(new Point(col, row))))
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
        var positionsNeeded = current.GetPositionsNeeded(nextMove);
        var other = Rocks.Except(new List<Rock> { current });
        var canMove = true;
        if (positionsNeeded.Any(_ => _.X < 0 || _.X > 6))
        {
            return;
        }
        foreach (var p in positionsNeeded)
        {
            if (other.Any(_ => _.IsOnPosition(p)))
            {
                return;
            }
            if (canMove)
            {
                current.Move(nextMove);
            }

        }
    }

    public bool TryDrop(Rock current)
    {
        //TODO: check positions
        if (current.BottomLeft.Y - 1 < 0)
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

    public Point TopRight =>
        new Point(BottomLeft.X + Width - 1, BottomLeft.Y + Height - 1);

    public Point BottomLeft { get; set; }

    public int Width =>
        _formation switch
        {
            RockFormation.HLine => 4,
            RockFormation.Plus => 3,
            RockFormation.ReverseL => 2,
            RockFormation.VLine => 1,
            RockFormation.Square => 2,
            _ => throw new Exception($"Unexpected rock formation {_formation}")
        };

    public int Height =>
        _formation switch
        {
            RockFormation.HLine => 1,
            RockFormation.Plus => 3,
            RockFormation.ReverseL => 3,
            RockFormation.VLine => 4,
            RockFormation.Square => 2,
            _ => throw new Exception($"Unexpected rock formation {_formation}")
        };

    public char[,] Print() =>
         _formation switch
         {
             RockFormation.Plus => new[,]
              {
                  {'.', '#', '.'},
                  {'#', '#', '#'},
                  {'.', '#', '.'},
              },
             RockFormation.HLine => new[,]
             {
                 {'#', '#', '#', '#'},
            },
             RockFormation.VLine => new[,]
             {
                 {'#'},
                 {'#'},
                 {'#'},
                 {'#'},
             },
             RockFormation.Square => new[,]
             {
                 {'#', '#'},
                 {'#', '#'},
             },
             RockFormation.ReverseL => new[,]
             {
                 {'.', '#'},
                 {'.', '#'},
                 {'#', '#'},
             },
             _ => throw new NotImplementedException(),
         };

    public bool IsOnRow(int row) =>
        row >= BottomLeft.Y && row < TopRight.Y || (row == BottomLeft.Y && Height == 1);

    public bool IsOnColumn(Point p)
    {
        if (IsOnRow(p.Y))
        {
            if (p.X >= BottomLeft.X && p.X <= TopRight.X)
            {
                switch (_formation)
                {
                    case RockFormation.Square:
                        return true;
                    case RockFormation.HLine:
                        return p.X >= BottomLeft.X && p.X <= TopRight.X;
                    case RockFormation.Plus:
                        if (p.Y == BottomLeft.Y || p.Y == TopRight.Y)
                        {
                            return p.X == BottomLeft.X + 1;
                        }
                        else if (p.Y == BottomLeft.Y + 1)
                        {
                            return p.X >= BottomLeft.X && p.X <= TopRight.X;
                        }
                        return false;
                    case RockFormation.VLine:
                        return p.X == BottomLeft.X;
                    case RockFormation.ReverseL:
                        if (p.Y == BottomLeft.Y)
                        {
                            return p.X >= BottomLeft.X && p.X <= TopRight.X;
                        }
                        else if (p.X == TopRight.X)
                        {
                            return true;
                        }
                        return false;
                    default:
                        throw new ArgumentException("Unexpected rock formation");
                }
            }
        }
        return false;
    }

    public List<Point> GetPositionsNeeded(Day17.Direction nextMove)
    {
        var positionsNeeded = new List<Point>();
        var position = nextMove == Day17.Direction.Left ? -1 : Width;
        switch (_formation)
        {
            case RockFormation.Plus:
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y + 1));
                break;
            case RockFormation.ReverseL:
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y));
                break;
            case RockFormation.Square:
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y));
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y + 1));
                break;
            case RockFormation.HLine:
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y));
                break;
            case RockFormation.VLine:
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y));
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y + 1));
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y + 2));
                positionsNeeded.Add(new Point(BottomLeft.X + position, BottomLeft.Y + 3));
                break;
            default:
                throw new ArgumentException("");
        }
        return positionsNeeded;
    }

    public void Move(Day17.Direction nextMove)
    {
        var movement = nextMove == Day17.Direction.Left ? -1 : 1;
        BottomLeft = new Point(BottomLeft.X + movement, BottomLeft.Y);
    }

    public void Drop() =>
        BottomLeft = new Point(BottomLeft.X, BottomLeft.Y - 1);

    public bool IsOnPosition(Point p) =>
        IsOnRow(p.Y) && IsOnColumn(p);


}
