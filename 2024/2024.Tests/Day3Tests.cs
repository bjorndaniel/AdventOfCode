namespace AoC2024.Tests;
public class Day3Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename);

        //Then
        Assert.NotNull(result);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("161", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test2.txt";

        //When
        var result = Day3.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("48", result.Result);
    }

}