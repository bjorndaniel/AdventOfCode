namespace AoC2022;
public interface IPrinter
{
    void Flush();
    void Print(string s);
}

public class DebugPrinter : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Debug.WriteLine(s);
    }
}

public class Printer : IPrinter
{
    public void Flush() { }

    public void Print(string s)
    {
        Console.WriteLine(s);
    }
}
