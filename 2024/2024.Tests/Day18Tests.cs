namespace AoC2024.Tests;
public class Day18Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.ParseInput(filename);

        //Then
        Assert.Equal(25, result.Count);
        Assert.Equal((5,4), result.First());
        Assert.Equal((2,0), result.Last());

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("22", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("6,1", result.Result);
    }

}