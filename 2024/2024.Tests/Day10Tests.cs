namespace AoC2024.Tests;
public class Day10Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.Equal(8, result.GetLength(0));
        Assert.Equal(8, result.GetLength(1));
        Assert.Equal(9, result[0, 1]);
        Assert.Equal(2, result[7, 7]);
        Assert.Equal(5, result[3, 2]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("36", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("81", result.Result);
    }

}