namespace AoC2015.Tests;
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
        Assert.True(10 == result.Count, $"Expected 10 but was {result.Count}");
        Assert.True('^' == result[0], $"Expected ^ but was {result[0]}");
        Assert.True('v' == result.Last(), $"Expected ^ but was {result.Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("11" == result.Result, $"Expected 11 but was {result.Result}");
    }

}