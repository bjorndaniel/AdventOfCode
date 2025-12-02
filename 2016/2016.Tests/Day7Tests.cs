namespace AoC2016.Tests;

public class Day7Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test2.txt";

        //When
        var result = Day7.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("3" == result.Result, $"Expected 3 but was {result.Result}");
    }

}