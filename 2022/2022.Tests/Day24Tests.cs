namespace AoC2022.Tests;
public class Day24Tests
{

    private readonly ITestOutputHelper _output;

    public Day24Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.ParseInput(filename);

        //Then
        Assert.Equal(6, result.GetLength(0));
        Assert.Equal(8, result.GetLength(1));
        var printer = new TestPrinter(_output);
        printer.PrintMatrix(result);
        printer.Flush();
    }

    [Fact]
    public void Can_solve_part1_for_Test()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";
        
        //When
        var result = Day24.SolvePart1(filename, new TestPrinter(_output));
        
        //Then
        Assert.True(18 == result, $"Expected 18, got {result}");
    }
}
