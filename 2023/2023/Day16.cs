namespace AoC2023;
public class Day16
{
    private static readonly Dictionary<(int x, int y), List<Direction>> _visited = [];
    private static IPrinter _printer = null!;

    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines[0].Length, lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[col, row] = lines[row][col];
            }
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day16.txt", "Day 16 part 1", 16)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        _visited.Clear();
        _printer = printer;
        var cave = ParseInput(filename);
        Beamer(0, 0, Direction.Right, new Beam(0, 0), cave);
        return new SolutionResult(_visited.Count.ToString());
    }

    [Solveable("2023/Puzzles/Day16.txt", "Day 16 part 2", 16)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var cave = ParseInput(filename);
        var highest = 0;
        for(int col = 0;col<cave.GetLength(0);col++)
        {
            _visited.Clear();
            Beamer(col, 0, Direction.Down, new Beam(col, 0), cave);
            highest = Math.Max(highest, _visited.Count);
            _visited.Clear();
            Beamer(col, cave.GetLength(1)-1, Direction.Up, new Beam(col, cave.GetLength(1)-1), cave);
        }
        for(int row = 0;row<cave.GetLength(1);row++)
        {
            _visited.Clear();
            Beamer(0, row, Direction.Right, new Beam(0, row), cave);
            highest = Math.Max(highest, _visited.Count);
            _visited.Clear();
            Beamer(cave.GetLength(0)-1, row, Direction.Left, new Beam(cave.GetLength(0)-1, row), cave);
        }
        return new SolutionResult(highest.ToString());
    }

    private static void Beamer(int x, int y, Direction direction, Beam beam, char[,] cave)
    {
        if (x < 0 || y < 0 || x >= cave.GetLength(0) || y >= cave.GetLength(1))
        {
            return;
        }

        if (_visited.ContainsKey((x, y)))
        {
            if (_visited[(x, y)].Contains(direction))
            {
                return;
            }
            _visited[(x, y)].Add(direction);
        }
        else
        {
            _visited.Add((x, y), [direction]);
        }
        switch (cave[x, y])
        {
            case '.':
                ContinueStraight(x, y, direction, beam, cave);
                break;
            case '/':
                if (direction == Direction.Up || direction == Direction.Down)
                {
                    var (nx, ny) = (direction == Direction.Up ? x + 1 : x - 1, y);
                    var nd = direction == Direction.Up ? Direction.Right : Direction.Left;
                    Beamer(nx, ny, nd, beam, cave);
                }
                else
                {
                    var (nx, ny) = (x, direction == Direction.Right ? y - 1 : y + 1);
                    var nd = direction == Direction.Right ? Direction.Up : Direction.Down;
                    Beamer(nx, ny, nd, beam, cave);
                }
                break;
            case '\\':
                if (direction == Direction.Up || direction == Direction.Down)
                {
                    var (nx, ny) = (direction == Direction.Up ? x - 1 : x + 1, y);
                    var nd = direction == Direction.Up ? Direction.Left : Direction.Right;
                    Beamer(nx, ny, nd, beam, cave);
                }
                else
                {
                    var (nx, ny) = (x, direction == Direction.Right ? y + 1 : y - 1);
                    var nd = direction == Direction.Right ? Direction.Down : Direction.Up;
                    Beamer(nx, ny, nd, beam, cave);
                }
                break;
            case '|':
                if (direction == Direction.Right || direction == Direction.Left)
                {
                    var nbU = new Beam(x, y);
                    Beamer(x, y - 1, Direction.Up, nbU, cave);
                    Beamer(x, y + 1, Direction.Down, beam, cave);
                }
                else
                {
                    ContinueStraight(x, y, direction, beam, cave);
                }
                break;
            case '-':
                if (direction == Direction.Up || direction == Direction.Down)
                {
                    var nbL = new Beam(x, y);
                    Beamer(x - 1, y, Direction.Left, nbL, cave);
                    Beamer(x + 1, y, Direction.Right, beam, cave);
                }
                else
                {
                    ContinueStraight(x, y, direction, beam, cave);
                }
                break;
        }
    }

    private static void ContinueStraight(int x, int y, Direction direction, Beam beam, char[,] cave)
    {
        switch (direction)
        {
            case Direction.Up:
                Beamer(x, y - 1, direction, beam, cave);
                break;
            case Direction.Down:
                Beamer(x, y + 1, direction, beam, cave);
                break;
            case Direction.Left:
                Beamer(x - 1, y, direction, beam, cave);
                break;
            case Direction.Right:
                Beamer(x + 1, y, direction, beam, cave);
                break;
            default:
                throw new ArgumentException("Unknown direction");

        }
    }

    private static char GetDirection(Direction direction)
    {
        return direction switch
        {
            Direction.Up => '^',
            Direction.Down => 'v',
            Direction.Left => '<',
            Direction.Right => '>',
            _ => throw new ArgumentException("Unknown direction"),
        };
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private record Beam(int X, int Y) { }

    private static void Print(char[,] cave, int x, int y, Direction direction)
    {
        _printer.Print($"({x},{y}) {GetDirection(direction)}");
        _printer.Flush();

        var printMatrix = new char[cave.GetLength(0), cave.GetLength(1)];
        for (int row = 0; row < cave.GetLength(1); row++)
        {
            for (int col = 0; col < cave.GetLength(0); col++)
            {
                printMatrix[col, row] = cave[col, row];
            }
        }
        foreach (var group in _visited)
        {
            if (printMatrix[group.Key.x, group.Key.y] != '.')
            {
                continue;
            }

            if (group.Value.Count > 1)
            {
                printMatrix[group.Key.x, group.Key.y] = group.Value.Count.ToString()[0];
            }
            else
            {
                printMatrix[group.Key.x, group.Key.y] = GetDirection(group.Value.First());
            }
        }
        _printer.PrintMatrixXY(printMatrix);
        _printer.Flush();
        _printer.Print("");
        _printer.Flush();
    }

}