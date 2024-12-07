namespace AoC.Shared;
public record SolutionResult(string Result) { }

public interface IPrinter
{
    void Flush();
    void Print(string s);

    void PrintMatrix<T>(T[,] matrix);
    void PrintMatrixXY<T>(T[,] matrix);
    void PrintMatrix(List<(int x, int y)> coords);
    void PrintMatrix(List<(int x, int y, char mark)> coords);
    void PrintMatrixYX<T>(T[,] matrix);
}

public class DebugPrinter : IPrinter
{
    public void Flush() { }

    public void Print(string s) => Debug.WriteLine(s);

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Debug.Write(matrix[row, col]?.ToString() ?? ".");
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    Debug.Write("#");
                }
                else
                {
                    Debug.Write(".");
                }
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrix(List<(int x, int y, char mark)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                var coord = coords.FirstOrDefault(_ => _.x == j + minX && _.y == i + minY);
                if (coord != default)
                {
                    Debug.Write(coord.mark);
                }
                else
                {
                    Debug.Write('.');
                }
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                Debug.Write(matrix[col, row]?.ToString() ?? ".");
            }
            Debug.WriteLine("");
        }
    }

    public void PrintMatrixYX<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Debug.Write(matrix[row, col]?.ToString() ?? ".");
            }
            Debug.WriteLine("");
        }
    }

}

public class Printer : IPrinter
{
    public void Flush() { }

    public void Print(string s) =>
        Console.WriteLine(s);

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(matrix[row, col]);
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrix(List<(int x, int y, char mark)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                var coord = coords.FirstOrDefault(_ => _.x == j + minX && _.y == i + minY);
                if (coord != default)
                {
                    Console.Write(coord.mark);
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                Console.Write(matrix[col, row]);
            }
            Console.WriteLine();
        }
    }

    public void PrintMatrixYX<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(matrix[row, col]);
            }
            Console.WriteLine();
        }
    }
}

public class TestPrinter(ITestOutputHelper output) : IPrinter
{
    
    private readonly StringBuilder _sb = new();

    public void Print(string s) => _sb.Append(s);

    public void Flush()
    {
        Debug.WriteLine(_sb.ToString());
        output.WriteLine(_sb.ToString());
        _sb.Clear();
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                _sb.Append(matrix[row, col]?.ToString() ?? ".");
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrixXY<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(1); row++)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                _sb.Append(matrix[col, row]?.ToString() ?? ".");
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrixYX<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                _sb.Append(matrix[row, col]?.ToString() ?? ".");
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrix(List<(int x, int y)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for(int i = 0;i <= maxY - minY; i++)
        {
            for(int j = 0; j <= maxX - minX; j++)
            {
                if (coords.Contains((j + minX, i + minY)))
                {
                    _sb.Append('#');
                }
                else
                {
                    _sb.Append('.');
                }
            }
            _sb.AppendLine();
        }
    }

    public void PrintMatrix(List<(int x, int y, char mark)> coords)
    {
        var maxX = coords.Max(c => c.x);
        var maxY = coords.Max(c => c.y);
        var minX = coords.Min(c => c.x);
        var minY = coords.Min(c => c.y);
        for (int i = 0; i <= maxY - minY; i++)
        {
            for (int j = 0; j <= maxX - minX; j++)
            {
                var coord = coords.FirstOrDefault(_ => _.x == j + minX && _.y == i + minY);
                if (coord != default)
                {
                    _sb.Append(coord.mark);
                }
                else
                {
                    _sb.Append('.');
                }
            }
            _sb.AppendLine();
        }
    }
}

[AttributeUsage(AttributeTargets.All)]
public class Solveable(string filename, string name, int day = 0, bool skip = false) : Attribute
{
    public string Filename { get; } = filename;
    public string Name { get;  } = name;
    public int Day { get; } = day;
    public bool Skip { get; } = skip;
}

