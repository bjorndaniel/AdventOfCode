namespace AoC2024.Tests;
public class Day8Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.Equal(12, result.GetLength(0));
        Assert.Equal(12, result.GetLength(1));
        Assert.Equal(12, result.GetLength(1));
        Assert.Equal('0', result[4,4]);
        Assert.Equal('A', result[9,9]);
        var p = new TestPrinter(output);
        p.PrintMatrixYX(result);
        p.Flush();
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("14", result.Result.ToString());
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("34", result.Result.ToString());
    }

}