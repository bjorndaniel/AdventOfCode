namespace AoC2015.Tests;
public class Day5Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.ParseInput(filename);

        //Then
        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-part2-test.txt";

        //When
        var result = Day5.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2", result.Result);
    }

}