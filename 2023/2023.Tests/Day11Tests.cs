namespace AoC2023.Tests;
public class Day11Tests
{
    private readonly ITestOutputHelper _output;

    public Day11Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        var printer = new TestPrinter(_output);
        printer.PrintMatrixXY(result);
        printer.Flush();
        
        Assert.True(13 == result.GetLength(0), $"Exptected 13 cols but was {result.GetLength(0)}");
        Assert.True(12 == result.GetLength(1), $"Exptected 12 rows but was {result.GetLength(1)}");
        Assert.True('#' == result[9, 1], $"Expected # but was {result[9, 1]}");
        Assert.True('#' == result[1, 6], $"Expected # but was {result[1, 6]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("374" == result.Result, $"Exptected 374 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("8410" == result.Result, $"Expected 8410 but was {result.Result}");
    }

}