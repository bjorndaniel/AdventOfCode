namespace AoC2022.Tests;
public class Day4Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.Equal(6, result.Count());
        Assert.Equal(2, result.First().First.Low);
        Assert.Equal(4, result.First().First.High);
        Assert.Equal(8, result.First().Second.High);
        Assert.Equal(8, result.Last().Second.High);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When 
        var result = Day4.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("2", result.Result);
    }


    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When 
        var result = Day4.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("4", result.Result);
    }
}
