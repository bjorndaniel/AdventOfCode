namespace AoC2023.Tests;
public class Day7Tests
{
    private readonly ITestOutputHelper _output;

    public Day7Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(false);
    }

}