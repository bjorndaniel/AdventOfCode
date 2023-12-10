namespace AoC2023.Tests;
public class Day11Tests
{
    private readonly ITestOutputHelper _output;

    public Day11Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test2.txt";

        //When
        var result = Day11.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(false);
    }

}