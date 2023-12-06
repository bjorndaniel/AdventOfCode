namespace AoC2017.Tests;
public class Day2Tests
{
    private readonly ITestOutputHelper _output;

    public Day2Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
        Assert.True(5 == result.First()[0], $"Expected 5 but was {result.First()[0]}");
        Assert.True(8 == result.Last()[3], $"Expected 8 but was {result.Last()[3]}");
        Assert.True(3 == result[1].Count(), $"Expected 3 but was {result[1].Count()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("18" == result.Result, $"Expected 18 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test2.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("9" == result.Result, $"Expected 9 but was {result.Result}");
    }

}