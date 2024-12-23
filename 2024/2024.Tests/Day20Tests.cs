namespace AoC2024.Tests;
public class Day20Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.ParseInput(filename);

        //Then
        var p = new TestPrinter(output);
        p.PrintMatrixYX(result.grid);
        p.Flush();
        Assert.Equal(15, result.grid.GetLength(0));
        Assert.Equal(15, result.grid.GetLength(1));
        Assert.Equal((1, 3), result.start);
        Assert.Equal((5, 7), result.end);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("3", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("285", result.Result);
    }

}