namespace AoC2016.Tests;

public class Day9Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.True(6 == result.Count, $"Expected 9 but was {result.Count}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("57" == result.Result, $"Expected 57 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("56" == result.Result, $"Expected x but was {result.Result}");
    }

}