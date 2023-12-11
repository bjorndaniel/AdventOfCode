namespace AoC2023.Tests;
public class Day4Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.True(6 == result.Count, $"Expected 6 but was {result.Count}");
        Assert.True(result.First().WinningNumbers.Contains(17), $"Expected 17 to win but was missing");
        Assert.True(result.First().PlayerNumbers.Contains(9), $"Expected 9 to be a player number but was missing");
        Assert.True(result.Last().WinningNumbers.Contains(31), $"Expected 31 to win but was missing");
        Assert.True(result.Last().PlayerNumbers.Contains(10), $"Expected 10 to be a player number but was missing");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("13" == result.Result, $"Expected 13 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("30" == result.Result, $"Exptected 30 but was {result.Result}");
    }

}