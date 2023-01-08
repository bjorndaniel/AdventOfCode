namespace AoC2022.Tests;
public class Day23Tests
{
    private readonly ITestOutputHelper _output;

    public Day23Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.ParseInput(filename);

        //Then
        Assert.True(7 == result.GetLength(0), $"Expected 7, got {result.GetLength(0)}");
        Assert.True(7 == result.GetLength(1), $"Expected 7, got {result.GetLength(1)}");
        Assert.True('#' == result[0, 4], $"Expected #, got {result[0, 4]}");
        Assert.True('#' == result[6, 4], $"Expected #, got {result[0, 4]}");
        Assert.True('.' == result[2, 1], $"Expected #, got {result[0, 4]}");
        var printer = new TestPrinter(_output);
        printer.PrintMatrix(result);
        printer.Flush();

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.SolvePart1(filename, new TestPrinter(_output));

        //Then
        Assert.True(110 == result, $"Expected 110, got {result}");
    }
}
