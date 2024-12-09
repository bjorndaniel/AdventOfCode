namespace AoC2024.Tests;
public class Day9Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.Equal("00...111...2...333.44.5555.6666.777.888899".Length, result.Count);
        var s = result.Select(_ => _.Number == -1 ? "." : _.Number.ToString()).Aggregate((a, b) => $"{a}{b}");
        Assert.Equal("00...111...2...333.44.5555.6666.777.888899", s);
        //Assert.Equal(0, result.First().Key);
        //Assert.Equal("00...", result.First().Value);
        //Assert.Equal(9, result.Last().Key);
        //Assert.Equal("99", result.Last().Value);
        //Assert.Equal("00...111...2...333.44.5555.6666.777.888899", result);
        //Assert.Equal(2, result.Last().Size);
        //Assert.Equal(0, result.Last().FreeSpace);
        //Assert.Equal(9, result.Last().Id);
        //Assert.Equal(2, result.First().Size);
        //Assert.Equal(3, result.First().FreeSpace);
        //Assert.Equal(0, result.First().Id);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test2.txt";

        //When
        var result = Day9.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2132", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test3.txt";

        //When
        var result = Day9.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("169", result.Result);
    }

}