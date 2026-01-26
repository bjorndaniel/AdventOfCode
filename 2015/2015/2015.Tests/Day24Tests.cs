namespace AoC2015.Tests;

public class Day24Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.ParseInput(filename);

        //Then
        Assert.True(10 == result.Count(), $"Expected 10 but was {result.Count()}");
        Assert.True(1 == result.First(), $"Expected first element to be 1 but was {result.First()}");
        Assert.True(11 == result.Last(), $"Expected last element to be 11 but was {result.Last()}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("99" == result.Result, $"Expected 99 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("44" == result.Result, $"Expected 44 but was {result.Result}");
    }

}