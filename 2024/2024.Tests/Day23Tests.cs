namespace AoC2024.Tests;
public class Day23Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.ParseInput(filename);

        //Then
        Assert.Equal(32, result.Count);
        Assert.Equal("kh", result.First().First);
        Assert.Equal("tc", result.First().Second);
        Assert.Equal("td", result.Last().First);
        Assert.Equal("yn", result.Last().Second);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("7", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("co,de,ka,ta", result.Result);
    }

}

