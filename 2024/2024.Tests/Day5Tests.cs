namespace AoC2024.Tests;
public class Day5Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.ParseInput(filename);

        //Then
        Assert.Equal(21, result.rules.Count);
        Assert.Equal(6, result.instructions.Count);
        Assert.Equal((47, 53), result.rules.First());
        Assert.Equal((53, 13), result.rules.Last());
        Assert.Equal(new List<int> { 75, 47, 61, 53, 29 }, result.instructions.First());
        Assert.Equal(new List<int> { 97, 13, 75, 29, 47 }, result.instructions.Last());
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("143", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day5-test.txt";

        //When
        var result = Day5.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("123", result.Result);
    }

}