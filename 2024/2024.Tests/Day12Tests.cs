namespace AoC2024.Tests;
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
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

}