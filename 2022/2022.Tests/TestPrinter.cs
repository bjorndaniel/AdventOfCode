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
}