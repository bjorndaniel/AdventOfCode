namespace AoC2025.Tests;

public class Day5Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var (ranges, ingredients) = Day5.ParseInput(filename);

        //Then
        Assert.True(ranges.Count == 4, $"Expected 4 but was {ranges.Count}");
        Assert.True(ingredients.Count == 6, $"Expected 6 but was {ingredients.Count}");
        Assert.True(3 == ranges.First().low, $"Expected 3 but was {ranges.First().low}");
        Assert.True(5 == ranges.First().high, $"Expected 5 but was {ranges.First().high}");
        Assert.True(1 == ingredients.First(), $"Expected 1 but was {ingredients.First()}");
        Assert.True(32 == ingredients.Last(), $"Expected 32 but was {ingredients.Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("3" == result.Result, $"Expected 3 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("14" == result.Result, $"Expected 14 but was {result.Result}");
    }

}