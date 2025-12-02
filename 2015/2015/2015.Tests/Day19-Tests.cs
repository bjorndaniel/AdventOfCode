namespace AoC2015.Tests;

public class Day19Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var (replacements, molecule) = Day19.ParseInput(filename);

        //Then
        Assert.True(3 == replacements.Count(), $"Expected 3 but was {replacements.Count()}");
        Assert.Equal("HOH", molecule);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("4" == result.Result, $"Expected 4 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test2.txt";

        //When
        var result = Day19.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("6" == result.Result, $"Expected 6 but was {result.Result}");
    }

}