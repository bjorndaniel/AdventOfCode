namespace AoC2022;
public static class Day22
{
    public static (BoardSquare[,] board, IEnumerable<Move> moves) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var matrixHeight = 0;
        var matrixWidth = 0;
        while (lines[matrixHeight] != string.Empty)
        {
            if (matrixWidth < lines[matrixHeight].Length)
            {
                matrixWidth = lines[matrixHeight].Length;
            }
            matrixHeight++;
        }
        return (ParseRows(lines, matrixWidth, matrixHeight), ParseMoves(lines[matrixHeight + 1]));

        static BoardSquare[,] ParseRows(string[] lines, int matrixWidth, int matrixHeight)
        {
            var matrix = new BoardSquare[matrixHeight, matrixWidth];
            for (var row = 0; row < matrixHeight; row++)
            {
                for (var col = 0; col < matrixWidth; col++)
                {
                    var l = lines[row];
                    if (lines[row].Length <= col)
                    {
                        matrix[row, col] = new BoardSquare { Type = BoardType.Void, Value = null };
                    }
                    else if (l[col] != ' ')
                    {
                        matrix[row, col] = new BoardSquare { Type = l[col] == '#' ? BoardType.Wall : BoardType.Open, Value = l[col] };
                    }
                    else
                    {
                        matrix[row, col] = new BoardSquare { Type = BoardType.Void, Value = null };
                    }
                }
            }
            return matrix;
            //var rows = new List<BoardRow>();
            //foreach (var l in lines)
            //{
            //    if (string.IsNullOrEmpty(l))
            //    {
            //        break;
            //    }
            //    var row = new BoardRow();
            //    for (int col = 0; col < matrixWidth; col++)
            //    {
            //        if (l.Length <= col)
            //        {
            //            row.Squares.Add(new BoardSquare { Type = BoardType.Void, Value = null });
            //        }
            //        else if (l[col] != ' ')
            //        {
            //            row.Squares.Add(new BoardSquare { Type = l[col] == '#' ? BoardType.Wall : BoardType.Open, Value = l[col] });
            //        }
            //        else
            //        {
            //            row.Squares.Add(new BoardSquare { Type = BoardType.Void, Value = null });
            //        }
            //    }
            //    rows.Add(row);
            //}
            //return rows;
        }
        static List<Move> ParseMoves(string moveString)
        {
            var moves = new List<Move>();
            for (int i = 0; i < moveString.Length; i++)
            {
                if (!char.IsDigit(moveString[i]))
                {
                    moves.Add(new Move(null, moveString[i] == 'L' ? Turn.Left : Turn.Right));
                }
                else if (i >= moveString.Length - 1)
                {
                    moves.Add(new Move(int.Parse(moveString[i].ToString()), null));
                }
                else
                {
                    if (char.IsDigit(moveString[i + 1]))
                    {
                        moves.Add(new Move(int.Parse($"{moveString[i]}{moveString[i + 1]}"), null));
                        i++;
                    }
                    else
                    {
                        moves.Add(new Move(int.Parse(moveString[i].ToString()), null));
                    }
                }
            }
            return moves;
        }
    }

    public static (int result, BoardSquare[,] matrix) SolvePart1(string filename, IPrinter printer)
    {
        var (matrix, moves) = ParseInput(filename);
        var startX = FindStart(matrix);
        var (x, y) = (startX, 0);
        var currentDirection = TileDirection.Right;
        var moveCount = 0;
        foreach (var move in moves)
        {
            if (move.Length.HasValue)
            {
                matrix[y, x].Value = GetMarker(currentDirection);
                //printer.PrintMatrix(matrix);
                //printer.Flush();
                switch (currentDirection)
                {
                    case TileDirection.Up:
                        y = Move(matrix, (x, y), currentDirection, move.Length.Value);
                        break;
                    case TileDirection.Down:
                        y = Move(matrix, (x, y), currentDirection, move.Length.Value);
                        break;
                    case TileDirection.Left:
                        x = Move(matrix, (x, y), currentDirection, move.Length.Value);
                        break;
                    case TileDirection.Right:
                        x = Move(matrix, (x, y), currentDirection, move.Length.Value);
                        break;
                }
            }
            else
            {
                currentDirection = GetDirection(move.Direction!.Value, currentDirection);
                matrix[y, x].Value = GetMarker(currentDirection);
            }
            moveCount++;
        }
        y++;
        x++;
        return ((1000 * y) + (x * 4) + (int)currentDirection, matrix);

        static int FindStart(BoardSquare[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[0, i].Type == BoardType.Open)
                {
                    return i;
                }
            }
            return -1;
        }

        static char GetMarker(TileDirection current) =>
            current switch
            {
                TileDirection.Up => '^',
                TileDirection.Down => 'v',
                TileDirection.Left => '<',
                TileDirection.Right => '>',
                _ => throw new NotImplementedException()
            };

        static TileDirection GetDirection(Turn turn, TileDirection current)
        {
            if (turn == Turn.Left)
            {
                return current switch
                {
                    TileDirection.Up => TileDirection.Left,
                    TileDirection.Right => TileDirection.Up,
                    TileDirection.Down => TileDirection.Right,
                    TileDirection.Left => TileDirection.Down,
                    _ => throw new ArgumentException("Illegal turn")
                };
            }
            else
            {
                return current switch
                {
                    TileDirection.Up => TileDirection.Right,
                    TileDirection.Right => TileDirection.Down,
                    TileDirection.Down => TileDirection.Left,
                    TileDirection.Left => TileDirection.Up,
                    _ => throw new ArgumentException("Illegal turn")
                };
            }
        }

        static int Move(BoardSquare[,] matrix, (int x, int y) start, TileDirection tileDirection, int steps)
        {
            var row = start.y;
            var col = start.x;

            switch (tileDirection)
            {
                case TileDirection.Up:
                    for (int i = steps; i > 0; i--)
                    {
                        row--;
                        if (row <= 0)
                        {
                            row = matrix.GetLength(0) - 1;
                        }
                        if (matrix[row, start.x].Type == BoardType.Wall)
                        {
                            return row + 1;
                        }
                        if (matrix[row, start.x].Type == BoardType.Void)
                        {
                            var next = GetNextOpenRowUp(matrix, matrix.GetLength(0) - 1, start.x);
                            if (next == -1)
                            {
                                return row - 1;
                            }
                            row = next;
                            continue;
                        }
                    }
                    return row;
                case TileDirection.Down:
                    for (int i = 0; i < steps; i++)
                    {
                        row++;
                        if (row >= matrix.GetLength(0))
                        {
                            row = 0;
                        }
                        if (matrix[row, start.x].Type == BoardType.Wall)
                        {
                            var next = row - 1;
                            if (next < 0)
                            {
                                return matrix.GetLength(0) - 1;
                            }
                            return next;
                        }
                        if (matrix[row, start.x].Type == BoardType.Void)
                        {
                            var next = GetNextOpenRowDown(matrix, 0, start.x);
                            if (next == -1)
                            {
                                return row - 1;
                            }
                            row = next;
                            continue;
                        }
                    }
                    return row;
                case TileDirection.Left:
                    for (int i = steps; i > 0; i--)
                    {
                        var prev = col;
                        col--;
                        if (col <= 0)
                        {
                            col = matrix.GetLength(1) - 1;
                        }
                        if (matrix[start.y, col].Type == BoardType.Void)
                        {
                            var next = GetNextOpenRowLeft(matrix, start.y, matrix.GetLength(1) - 1);
                            if (next == -1)
                            {
                                return col + 1;
                            }
                            col = next;
                            continue;
                        }
                        if (matrix[start.y, col].Type == BoardType.Wall)
                        {
                            return prev;
                        }
                    }
                    return col;
                case TileDirection.Right:
                    for (int i = 0; i < steps; i++)
                    {
                        var prev = col;
                        col++;
                        if (col >= matrix.GetLength(1))
                        {
                            col = 0;
                        }
                        if (matrix[start.y, col].Type == BoardType.Void)
                        {
                            var next = GetNextOpenRowRight(matrix, start.y, 0);
                            if (next == -1)
                            {
                                return col - 1;
                            }
                            col = next;
                            continue;
                        }
                        if (matrix[start.y, col].Type == BoardType.Wall)
                        {
                            return prev;
                        }
                    }
                    return col;
            }
            throw new ArgumentException("Unreachable condition");

            int GetNextOpenRowDown(BoardSquare[,] matrix, int row, int col)
            {
                for (int i = row; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, col].Type == BoardType.Void)
                    {
                        continue;
                    }
                    if (matrix[i, col].Type == BoardType.Wall)
                    {
                        return -1;
                    }
                    if (matrix[i, col].Type == BoardType.Open)
                    {
                        return i;
                    }
                }
                return -1;
            }

            int GetNextOpenRowUp(BoardSquare[,] matrix, int row, int col)
            {
                for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
                {
                    if (matrix[i, col].Type == BoardType.Void)
                    {
                        continue;
                    }
                    if (matrix[i, col].Type == BoardType.Wall)
                    {
                        return -1;
                    }
                    if (matrix[i, col].Type == BoardType.Open)
                    {
                        return i;
                    }
                }
                return -1;
            }

            int GetNextOpenRowLeft(BoardSquare[,] matrix, int row, int col)
            {
                for (int i = matrix.GetLength(1) - 1; i >= 0; i--)
                {
                    if (matrix[row, i].Type == BoardType.Void)
                    {
                        continue;
                    }
                    if (matrix[row, i].Type == BoardType.Wall)
                    {
                        return -1;
                    }
                    if (matrix[row, i].Type == BoardType.Open)
                    {
                        return i;
                    }
                }
                return -1;
            }

            int GetNextOpenRowRight(BoardSquare[,] matrix, int row, int col)
            {
                for (int i = col; i < matrix.GetLength(1); i++)
                {
                    if (matrix[row, i].Type == BoardType.Void)
                    {
                        continue;
                    }
                    if (matrix[row, i].Type == BoardType.Wall)
                    {
                        return -1;
                    }
                    if (matrix[row, i].Type == BoardType.Open)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }
    }
}

public record Move(int? Length, Turn? Direction);

public enum TileDirection
{
    Right,
    Left,
    Up,
    Down
}

public enum Turn
{
    Left,
    Right
}

public enum BoardType
{
    Open,
    Wall,
    Void
}

public class BoardSquare
{
    public char? Value { get; set; }
    public BoardType Type { get; set; }
    public override string ToString() => Value?.ToString() ?? " ";
}

