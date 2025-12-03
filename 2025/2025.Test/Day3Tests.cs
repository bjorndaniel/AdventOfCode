namespace AoC2025.Tests;

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
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True(9 == result.First().Batteries.First(), $"Expected 9 but was {result.First().Batteries.First()}");
        Assert.True(1 == result.Last().Batteries.Last(), $"Expected 9 but was {result.Last().Batteries.Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("357" == result.Result, $"Expected 357 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("3121910778619" == result.Result, $"Expected 3121910778619 but was {result.Result}");
    }

}