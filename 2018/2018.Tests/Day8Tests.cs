namespace AoC2018.Tests;
public class Day8Tests
{
    private readonly ITestOutputHelper _output;

    public Day8Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.True(1 == result.Count(), $"Expected 1 but was {result.Count}");
        Assert.True("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2" == result.First(), $"Expected 2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2 but was {result.First()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("138" == result.Result, $"Expected 138 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("66" == result.Result, $"Expected 66 but was {result.Result}");
    }

}