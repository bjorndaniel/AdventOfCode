namespace AoC2023;
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

public class Solveable : Attribute
{
    public Solveable(string filename, string name)
    {
        Filename = filename;
        Name = name;
    }

    public string Filename { get; private set; }
    public string Name { get; private set; }
}