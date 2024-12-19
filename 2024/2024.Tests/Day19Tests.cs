namespace AoC2024.Tests;
public class Day19Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var (towels, designs) = Day19.ParseInput(filename);

        //Then
        Assert.Equal(8,towels.Count);
        Assert.Equal("r", towels[0].Pattern);
        Assert.Equal("bwu", towels[4].Pattern);
        Assert.Equal(8,designs.Count);
        Assert.Equal("brwrr", designs[0].Pattern);
        Assert.Equal("ubwu", designs[4].Pattern);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("6", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("16", result.Result);
    }

}