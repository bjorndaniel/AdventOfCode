namespace AoC2024.Tests;
public class Day12Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.ParseInput(filename);

        //Then
        Assert.Equal(4, result.GetLength(0));
        Assert.Equal(4, result.GetLength(1));
        Assert.Equal('A', result[1, 0]);
        Assert.Equal('E', result[0, 3]);
        var p = new TestPrinter(output);
        p.PrintMatrixXY(result);
        p.Flush();
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test2.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("1930", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test_simple()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("80", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test2.txt";

        //When
        var result = Day12.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("1206", result.Result);
    }

}