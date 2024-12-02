namespace AoC2015.Tests;
public class Day9Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.Equal(3, result.Count);
        Assert.Equal("London", result[0].From);
        Assert.Equal("Dublin", result[0].To);
        Assert.Equal(464, result[0].Distance);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("605", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("982", result.Result);
    }

}