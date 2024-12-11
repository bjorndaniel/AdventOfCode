namespace AoC2024.Tests;
public class Day11Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        Assert.Equal(2, result.Count);
        Assert.Equal("125", result[0]);
        Assert.Equal("17", result[1]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("55312", result.Result);
    }
}