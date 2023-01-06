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
        foreach (var move in moves)
        {
            if (move.Length.HasValue)
            {
                matrix[y, x].Value = GetMarker(currentDirection);
                printer.PrintMatrix(matrix);
                printer.Flush();
                switch (currentDirection)
                {
                    case TileDirection.Up:

                        break;
                    case TileDirection.Down:
                        y = Move(matrix, (x, y), currentDirection, move.Length.Value);
                        break;
                    case TileDirection.Left:

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
        }
        return (0, new BoardSquare[0, 0]);
        //var start = matri
        //var index = matrix.First().Squares.IndexOf(start);
        //var currentRow = matrix.First();
        //var currentDirection = TileDirection.Right;
        //foreach (var move in moves)
        //{
        //    if (move.Length.HasValue)
        //    {
        //        currentRow.Squares[index].Value = GetMarker(currentDirection);
        //        var end = 0;
        //        var nrToMove = 0;
        //        //switch (currentDirection)
        //        //{
        //        //    case TileDirection.Right:
        //        //        nrToMove = (index + move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open);
        //        //        end = ((index + 1 + move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open));
        //        //        break;
        //        //    case TileDirection.Left:
        //        //        nrToMove = (index - move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open);
        //        //        end = ((index - 1 - move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open));
        //        //        break;
        //        //    case TileDirection.Down:
        //        //        var start = matrix.IndexOf(currentRow);
        //        //        var end = start + move.Length;
        //        //        break;
        //        //    case TileDirection.Up:

        //        //        break;

        //        //}
        //        //end += index + 1;
        //        //index = end;
        //    }
        //    else
        //    {
        //        currentDirection = GetDirection(move.Direction!.Value, currentDirection);
        //        currentRow.Squares[index].Value = GetMarker(currentDirection);

        //    }
        //    //Day22.Print(matrix, printer);
        //}

        //var result = 0;
        //return (result, new List<BoardRow>());


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
            var result = 0;
            switch (tileDirection)
            {
                case TileDirection.Up:
                    break;
                case TileDirection.Down:
                    var row = start.y;
                    for (int i = 0; i < steps; i++)
                    {
                        row++;
                        if (row > matrix.GetLength(0))
                        {
                            row = 0;
                        }
                        if (matrix[row, start.x].Type == BoardType.Wall)
                        {
                            return row - 1;
                        }
                        if (matrix[row, start.x].Type == BoardType.Void)
                        {
                            continue;
                        }
                    }
                    return row;
                case TileDirection.Left:
                    break;
                case TileDirection.Right:
                    var col = start.x;
                    for (int i = 0; i < steps; i++)
                    {
                        var prev = col;
                        col++;
                        if (col > matrix.GetLength(1))
                        {
                            col = 0;
                        }
                        if (matrix[start.y, col].Type == BoardType.Void)
                        {
                            col = 0;
                        }
                        if (matrix[start.y, col].Type == BoardType.Wall)
                        {
                            return prev;
                        }
                    }
                    return col;
            }
            return result;
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

