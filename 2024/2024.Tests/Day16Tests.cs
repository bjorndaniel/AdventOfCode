namespace AoC2024.Tests;
public class Day16Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var (grid, start, end) = Day16.ParseInput(filename);

        //Then
        Assert.Equal(15, grid.GetLength(0));
        Assert.Equal(15, grid.GetLength(1));
        Assert.Equal('>', grid[13, 1]);
        Assert.Equal('E', grid[1, 13]);
        Assert.Equal((1,13), start);
        Assert.Equal((13, 1), end);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("7036", result.Result);
    }

    [Fact]
    public void Can_solve_part1_for_test2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test2.txt";

        //When
        var result = Day16.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("11048", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("45", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test_2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test2.txt";

        //When
        var result = Day16.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("64", result.Result);
    }

}