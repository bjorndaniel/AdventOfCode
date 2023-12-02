namespace AoC.Shared;
public record SolutionResult(string Result) { }

public interface IPrinter
{
    void Flush();
    void Print(string s);

    void PrintMatrix<T>(T[,] matrix);
}

public class DebugPrinter : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Debug.WriteLine(s);
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Debug.Write(matrix[row, col]);
            }
            Debug.WriteLine("");
        }
    }
}

public class Printer : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Console.WriteLine(s);
    }

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
}

public class TestPrinter : IPrinter
{
    private readonly ITestOutputHelper _output;
    private readonly StringBuilder _sb = new();

    public TestPrinter(ITestOutputHelper output)
    {
        _output = output;
    }

    public void Print(string s)
    {
        _sb.Append(s);
    }

    public void Flush()
    {
        Debug.WriteLine(_sb.ToString());
        _output.WriteLine(_sb.ToString());
        _sb.Clear();
    }

    public void PrintMatrix<T>(T[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                _sb.Append(matrix[row, col]);
            }
            _sb.AppendLine();
        }
    }
}

public class Solveable : Attribute
{
    public Solveable(string filename, string name, int day = 0)
    {
        Filename = filename;
        Name = name;
        Day = day;
    }

    public string Filename { get; private set; }
    public string Name { get; private set; }
    public int Day { get; private set; }
}