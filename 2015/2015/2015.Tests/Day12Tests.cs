namespace AoC2015.Tests;
public class Day12Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.ParseInput(filename);

        //Then
        Assert.True(result.Length == 188, $"Expected 188 but was {result.Length}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("27" == result.Result, $"Expected 27 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test-2.txt";

        //When
        var result = Day12.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("10" == result.Result, $"Expected 10 but was {result.Result}");
    }

}