namespace AoC2015.Tests;

public class Day18Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.ParseInput(filename);

        //Then
        Assert.True(6 == result.GetLength(0), $"Expeced 6 but was {result.GetLength(0)}");
        Assert.True(6 == result.GetLength(1), $"Expeced 6 but was {result.GetLength(0)}");
        Assert.True('.' == result[0,0], $"Expeced . but was {result[0,0]}");
        Assert.True('#' == result[2,5], $"Expeced # but was {result[0,0]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("4" == result.Result, $"Expected 4 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("17" == result.Result, $"Expected 17 but was {result.Result}");
    }

}