namespace AoC2022;
public static class Day9
{
    public static IEnumerable<RopeRound> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var parts = l.Split(' ');
            yield return new RopeRound(l[0], int.Parse(parts[1]));
        }
    }

    public static int SolvePart1(string filename)
    {
        var rounds = ParseInput(filename);
        var rope = new Rope();
        foreach (var r in rounds)
        {
            rope.MoveHead(r);
        }
        return rope.Visited.Count;
    }

    public static Dictionary<(int row, int col), int> SolvePart2(string filename)
    {
        var rounds = ParseInput(filename);
        var rope = new Rope();
        foreach (var r in rounds)
        {
            rope.MoveHead(r, true);
        }
        return rope.Visited;
    }

}

public record RopeRound(char Direction, int Length);

public class Rope
{
    public int Row { get; set; }
    public int Col { get; set; }
    public int TailRow { get; private set; }
    public int TailCol { get; private set; }
    public Dictionary<int, (int row, int col)> Tails { get; private set; } =
         new Dictionary<int, (int row, int col)>
         {
             {1, (0,0)},
             {2, (0,0)},
             {3, (0,0)},
             {4, (0,0)},
             {5, (0,0)},
             {6, (0,0)},
             {7, (0,0)},
             {8, (0,0)},
             {9, (0,0)},
         };

    public void MoveHead(RopeRound r, bool part2 = false)
    {
        for (int i = 0; i < r.Length; i++)
        {
            switch (r.Direction)
            {
                case 'U':
                    Row += 1;
                    break;
                case 'D':
                    Row -= 1;
                    break;
                case 'L':
                    Col -= 1;
                    break;
                case 'R':
                    Col += 1;
                    break;
            }
            if (part2)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (j == 1)
                    {
                        var (row, col) = MoveTail2((Row, Col), Tails[j]);
                        Tails[j] = (row, col);
                    }
                    else
                    {
                        var (row, col) = MoveTail2(Tails[j - 1], Tails[j], j == 9);
                        Tails[j] = (row, col);
                    }
                }
            }
            else
            {
                MoveTail();
            }
        }
    }

    public Dictionary<(int row, int col), int> Visited { get; } = new();

    private void MoveTail()
    {

        if (Row == TailRow)
        {
            if (Col > TailCol + 1)
            {
                TailCol++;
            }
            else if (Col < TailCol - 1)
            {
                TailCol--;
            }
        }
        else if (Col == TailCol)
        {
            if (Row > TailRow + 1)
            {
                TailRow++;
            }
            else if (Row < TailRow - 1)
            {
                TailRow--;
            }
        }
        else
        {
            var (isDiag, dir) = IsDiagonal((Row, Col), (TailRow, TailCol));
            if (isDiag)
            {
                switch (dir)
                {
                    case Direction.UpLeft:
                        TailCol--;
                        TailRow++;
                        break;
                    case Direction.UpRight:
                        TailCol++;
                        TailRow++;
                        break;
                    case Direction.DownLeft:
                        TailCol--;
                        TailRow--;
                        break;
                    case Direction.DownRight:
                        TailCol++;
                        TailRow--;
                        break;
                }
            }
        }

        if (Visited.ContainsKey((TailRow, TailCol)))
        {
            Visited[(TailRow, TailCol)]++;
        }
        else
        {
            Visited.Add((TailRow, TailCol), 1);
        }
    }

    private (int row, int col) MoveTail2((int Row, int Col) previous, (int Row, int Col) tail, bool addToVisited = false)
    {
        if (previous.Row == tail.Row)
        {
            if (previous.Col > tail.Col + 1)
            {
                tail.Col++;
            }
            else if (previous.Col < tail.Col - 1)
            {
                tail.Col--;
            }
        }
        else if (previous.Col == tail.Col)
        {
            if (previous.Row > tail.Row + 1)
            {
                tail.Row++;
            }
            else if (previous.Row < tail.Row - 1)
            {
                tail.Row--;
            }
        }
        else
        {
            var (isDiag, dir) = IsDiagonal((previous.Row, previous.Col), (tail.Row, tail.Col));
            if (isDiag)
            {
                switch (dir)
                {
                    case Direction.UpLeft:
                        tail.Col--;
                        tail.Row++;
                        break;
                    case Direction.UpRight:
                        tail.Col++;
                        tail.Row++;
                        break;
                    case Direction.DownLeft:
                        tail.Col--;
                        tail.Row--;
                        break;
                    case Direction.DownRight:
                        tail.Col++;
                        tail.Row--;
                        break;
                }
            }
        }
        if (addToVisited)
        {
            if (Visited.ContainsKey((tail.Row, tail.Col)))
            {
                Visited[(tail.Row, tail.Col)]++;
            }
            else
            {
                Visited.Add((tail.Row, tail.Col), 1);
            }
        }
        return (tail.Row, tail.Col);
    }

    private (bool isDiagonal, Direction d) IsDiagonal((int Row, int Col) p1, (int Row, int Col) p2)
    {
        if (p1.Col == p2.Col || p1.Row == p2.Row)
        {
            return (false, Direction.NA);
        }
        var dx = p1.Col - p2.Col;
        var dy = p1.Row - p2.Row;
        var distance = Math.Sqrt(dx * dx + dy * dy);
        Direction direction;
        if (dx > 0 && dy > 0)
        {
            direction = Direction.UpRight;
        }
        else if (dx > 0 && dy < 0)
        {
            direction = Direction.DownRight;
        }
        else if (dx < 0 && dy > 0)
        {
            direction = Direction.UpLeft;
        }
        else
        {
            direction = Direction.DownLeft;
        }
        return (distance > 2, direction);
    }
}

public enum Direction
{
    NA,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}
