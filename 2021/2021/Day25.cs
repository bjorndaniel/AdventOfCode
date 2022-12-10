namespace Advent2021;
public class Day25
{
    public static char[,] ReadSeaBottom(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.First().Length, lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                result[col, row] = lines[row][col];
            }
        }
        return result;
    }

    public static (bool success, char[,] field) MoveOne(char[,] field)
    {
        var madeAMove = false;
        //East
        for (var row = 0; row < field.GetLength(1); row++)
        {
            for (var col = 0; col < field.GetLength(0); col++)
            {
                var cucumber = field[col, row];
                if (cucumber == 'x')
                {
                    continue;
                }
                if (cucumber == '.')
                {
                    continue;
                }
                if (cucumber == 'v')
                {
                    continue;
                }
                if (cucumber == '>')
                {
                    if (col == field.GetLength(0) - 1)
                    {
                        if (field[0, row] == '.')
                        {
                            field[0, row] = '<';
                            field[col, row] = 'x';
                            madeAMove = true;
                        }
                    }
                    else
                    {
                        if (field[col + 1, row] == '.')
                        {
                            field[col, row] = 'x';
                            field[col + 1, row] = '<';
                            madeAMove = true;
                        }
                    }
                }
            }
        }
        for (var row = 0; row < field.GetLength(1); row++)
        {
            for (var col = 0; col < field.GetLength(0); col++)
            {
                if (field[col, row] == 'x')
                {
                    field[col, row] = '.';
                }
            }
        }
        //South
        for (var row = 0; row < field.GetLength(1); row++)
        {
            for (var col = 0; col < field.GetLength(0); col++)
            {
                var cucumber = field[col, row];
                if (cucumber == 'x')
                {
                    continue;
                }
                if (cucumber == '.')
                {
                    continue;
                }
                if (cucumber == '>')
                {
                    continue;
                }
                if (cucumber == 'v')
                {
                    if (row == field.GetLength(1) - 1)
                    {
                        if (field[col, 0] == '.')
                        {
                            field[col, row] = 'x';
                            field[col, 0] = '^';
                            madeAMove = true;
                        }
                    }
                    else
                    {
                        if (field[col, row + 1] == '.')
                        {
                            field[col, row] = 'x';
                            field[col, row + 1] = '^';
                            madeAMove = true;
                        }
                    }
                }
            }
        }
        for (var row = 0; row < field.GetLength(1); row++)
        {
            for (var col = 0; col < field.GetLength(0); col++)
            {
                if (field[col, row] == 'x')
                {
                    field[col, row] = '.';
                }
                if (field[col, row] == '^')
                {
                    field[col, row] = 'v';
                }
                if (field[col, row] == '<')
                {
                    field[col, row] = '>';
                }
            }
        }
        return (madeAMove, field);
    }

    public static long CountMoves(string filename)
    {
        var seaBottom = ReadSeaBottom(filename);
        var done = false;
        var moves = 1L;
        while (!done)
        {
            var (success, field) = MoveOne(seaBottom);
            if (success)
            {
                seaBottom = field;
                moves++;
            }
            else
            {
                return moves++;
            }
        }
        return moves;
    }

    private static void Print(char[,] field)
    {
        for (int row = 0; row < field.GetLength(1); row++)
        {
            var sb = new StringBuilder();
            for (int col = 0; col < field.GetLength(0); col++)
            {
                sb.Append(field[col, row]);
            }
            Console.WriteLine(sb.ToString());
            Console.WriteLine("");
        }
    }
}
