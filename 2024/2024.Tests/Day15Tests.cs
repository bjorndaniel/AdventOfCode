namespace AoC2024.Tests;
public class Day15Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.ParseInput(filename);

        //Then
        Assert.Equal(8, result.grid.GetLength(0));
        Assert.Equal(8, result.grid.GetLength(1));
        Assert.Equal(15, result.moves.Count);
        Assert.Equal('<', result.moves.First());
        Assert.Equal('<', result.moves.Last());
        Assert.Equal((2, 2), result.robotStart);
    }

    [Fact]
    public void Can_parse_input_larger()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day15-test2.txt";

        //When
        var result = Day15.ParseInput(filename);

        //Then
        Assert.Equal(10, result.grid.GetLength(0));
        Assert.Equal(10, result.grid.GetLength(1));
        Assert.Equal(700, result.moves.Count);
        Assert.Equal('<', result.moves.First());
        Assert.Equal('^', result.moves.Last());
        Assert.Equal((4,4), result.robotStart);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2028", result.Result);
    }

    [Fact]
    public void Can_solve_part1_for_test_larger()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test2.txt";

        //When
        var result = Day15.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("10092", result.Result);
    }

    [Fact]
    public void Can_scale_the_grid()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test2.txt";

        //When
        var (grid, robot) = Day15.ScaleUp(filename, new TestPrinter(output));

        //Then
        Assert.Equal(10, grid.GetLength(0));
        Assert.Equal(20, grid.GetLength(1));
        Assert.Equal('[', grid[1, 16]);
        Assert.Equal(']', grid[1, 17]);
        Assert.Equal((8,4), robot);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test2.txt";

        //When
        var result = Day15.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("9021", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test_simpler()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test3.txt";

        //When
        var result = Day15.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("618", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test_simple()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test4.txt";

        //When
        var result = Day15.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("1216", result.Result);
    }
}