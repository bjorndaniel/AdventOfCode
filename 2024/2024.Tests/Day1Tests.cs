namespace AoC2024.Tests;

public class Day1Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.Equal(6, result.Count);
        Assert.Equal((3, 4), result[0]);
        Assert.Equal((3, 3), result[5]);

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("11", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("31", result.Result);

    }
}
