namespace AoC2017.Tests;
public class Day5Tests
{
    private readonly ITestOutputHelper _output;

    public Day5Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.ParseInput(filename);

        //Then
        Assert.True(5 == result.Length, $"Expected 5 but was {result.Length}");
        Assert.True(-3 == result[4], $"Expected 5 but was {result[4]}");
        Assert.True(0 == result[2], $"Expected 5 but was {result[2]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("5" == result.Result, $"Expected 5 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("10" == result.Result, $"Expected 10 but was {result.Result}");
    }

}