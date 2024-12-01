namespace AoC2015.Tests;
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
        Assert.Equal(4, result.Count);
        Assert.Equal("\"\"", result[0]);
        Assert.Equal("\"abc\"", result[1]);

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("12", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("19", result.Result);
    }

}