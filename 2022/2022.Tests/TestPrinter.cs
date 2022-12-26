namespace AoC2022.Tests;
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