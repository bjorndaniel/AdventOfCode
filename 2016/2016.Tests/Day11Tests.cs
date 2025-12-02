namespace AoC2016.Tests;

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
        Assert.True(1 == result.Elevator, $"Expected 1 but was {result.Elevator}");
        Assert.True(2 == result.Pairs.Length, $"Expected 2 but was {result.Pairs.Length}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("11" == result.Result, $"Expected 11 but was {result.Result}");
    }

}