namespace AoC2025.Tests;

public class Day7Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        var printer = new TestPrinter(output);
        printer.PrintMatrixXY(result.manifold);
        printer.Flush();
        Assert.True(15 == result.manifold.GetLength(0), $"Expected 15 but was {result.manifold.GetLength(0)}");
        Assert.True(16 == result.manifold.GetLength(1), $"Expected 15 but was {result.manifold.GetLength(1)}");
        Assert.True((7,0) == result.start, $"Expected (7,0) but was {result.start}");
        Assert.True('^' == result.manifold[1,14], $"Expected ^ but was {result.manifold[1,13]}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("21" == result.Result, $"Expected 21 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("40" == result.Result, $"Expected 40 but was {result.Result}");
    }

}