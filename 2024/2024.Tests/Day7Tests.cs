namespace AoC2024.Tests;
public class Day7Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.Equal(9, result.Count);
        Assert.Equal(190, result[0].Result);
        Assert.Equal([10,19], result[0].Operands);
        Assert.Equal(21037, result[7].Result);
        Assert.Equal([9,7,18,13], result[7].Operands);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("3749", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("11387", result.Result);
    }

}