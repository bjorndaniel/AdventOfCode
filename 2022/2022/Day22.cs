namespace AoC2022;
public static class Day22
{
    public static (List<BoardRow> rows, IEnumerable<Move> moves) ParseInput(string filename)
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
        return (ParseRows(lines, matrixWidth), ParseMoves(lines[matrixHeight + 1]));

        static List<BoardRow> ParseRows(string[] lines, int matrixWidth)
        {
            var rows = new List<BoardRow>();
            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                {
                    break;
                }
                var row = new BoardRow();
                for (int col = 0; col < matrixWidth; col++)
                {
                    if (l.Length <= col)
                    {
                        row.Squares.Add(new BoardSquare { Type = BoardType.Void, Value = null });
                    }
                    else if (l[col] != ' ')
                    {
                        row.Squares.Add(new BoardSquare { Type = l[col] == '#' ? BoardType.Wall : BoardType.Open, Value = l[col] });
                    }
                    else
                    {
                        row.Squares.Add(new BoardSquare { Type = BoardType.Void, Value = null });
                    }
                }
                rows.Add(row);
            }
            return rows;
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

    public static (int result, List<BoardRow> matrix) SolvePart1(string filename, IPrinter printer)
    {
        var (matrix, moves) = ParseInput(filename);
        var start = matrix.First().Squares.First(s => s.Type == BoardType.Open);
        var index = matrix.First().Squares.IndexOf(start);
        var currentRow = matrix.First();
        var currentDirection = TileDirection.Right;
        foreach (var move in moves)
        {
            if (move.Length.HasValue)
            {
                currentRow.Squares[index].Value = GetMarker(currentDirection);
                var end = 0;
                var nrToMove = 0;
                //switch (currentDirection)
                //{
                //    case TileDirection.Right:
                //        nrToMove = (index + move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open);
                //        end = ((index + 1 + move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open));
                //        break;
                //    case TileDirection.Left:
                //        nrToMove = (index - move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open);
                //        end = ((index - 1 - move.Length.Value) % currentRow.Squares.Count(_ => _.Type == BoardType.Open));
                //        break;
                //    case TileDirection.Down:
                //        var start = matrix.IndexOf(currentRow);
                //        var end = start + move.Length;
                //        break;
                //    case TileDirection.Up:

                //        break;

                //}
                //end += index + 1;
                //index = end;
            }
            else
            {
                currentDirection = GetDirection(move.Direction!.Value, currentDirection);
                currentRow.Squares[index].Value = GetMarker(currentDirection);

            }
            //Day22.Print(matrix, printer);
        }

        var result = 0;
        return (result, new List<BoardRow>());
        char GetMarker(TileDirection current) =>
            current switch
            {
                TileDirection.Up => '^',
                TileDirection.Down => 'v',
                TileDirection.Left => '<',
                TileDirection.Right => '>',
                _ => throw new NotImplementedException()
            };
        TileDirection GetDirection(Turn turn, TileDirection current)
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
    }

    private static void Print(List<BoardRow> matrix, IPrinter printer)
    {
        foreach (var row in matrix)
        {
            var sb = new StringBuilder();
            foreach (var square in row.Squares)
            {
                sb.Append(square.Value);
            }
            printer.Print(sb.ToString());
            printer.Flush();
            //_output.WriteLine(sb.ToString());
        }
        printer.Print("");
        printer.Flush();
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

public class BoardRow
{
    public List<BoardSquare> Squares { get; set; } = new List<BoardSquare>();
}

public class BoardSquare
{
    public char? Value { get; set; }
    public BoardType Type { get; set; }
}

