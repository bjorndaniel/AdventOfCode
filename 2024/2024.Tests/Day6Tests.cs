namespace AoC2024.Tests;
public class Day6Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.ParseInput(filename);

        //Then
        var printer = new TestPrinter(output);
        printer.PrintMatrixYX(result.grid);
        printer.Flush();
        
        Assert.Equal(10, result.grid.GetLength(0));
        Assert.Equal(10, result.grid.GetLength(1));
        Assert.Equal('.', result.grid[0, 0]);
        Assert.Equal('#', result.grid[1, 9]);
        Assert.Equal('#', result.grid[9, 6]);
        Assert.Equal('#', result.grid[8, 0]);
        Assert.Equal((6,4), result.start);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("41", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("6", result.Result);
    }

}