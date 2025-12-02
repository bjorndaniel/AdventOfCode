namespace AoC2016.Tests;

public class Day6Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.ParseInput(filename);

        //Then
        Assert.True(16 == result.Count, $"Expected 16 but was {result.Count}");
        Assert.True("eedadn" == result.First(), $"Expected eedadn but was {result.First()}");
        Assert.True("enarar" == result.Last(), $"Expected 16 but was {result.Last()}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("easter" == result.Result, $"Expected easter but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("advent" == result.Result, $"Expected advent but was {result.Result}");
    }

}