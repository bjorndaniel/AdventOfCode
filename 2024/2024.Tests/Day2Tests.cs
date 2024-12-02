namespace AoC2024.Tests;
public class Day2Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.ParseInput(filename);

        //Then
        Assert.Equal(6, result.Count);
        Assert.Equal(5, result[0].Count);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("4", result.Result);
    }

}