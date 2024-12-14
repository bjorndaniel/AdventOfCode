namespace AoC2024.Tests;
public class Day14Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.ParseInput(filename);

        //Then
        Assert.Equal(12, result.robots.Count);
        Assert.Equal(0, result.robots.First().StartX);
        Assert.Equal(4, result.robots.First().StartY);
        Assert.Equal(3, result.robots.First().VelocityX);
        Assert.Equal(-3, result.robots.First().VelocityY);

        Assert.Equal(9, result.robots.Last().StartX);
        Assert.Equal(5, result.robots.Last().StartY);
        Assert.Equal(-3, result.robots.Last().VelocityX);
        Assert.Equal(-3, result.robots.Last().VelocityY);

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("12", result.Result);
    }
}