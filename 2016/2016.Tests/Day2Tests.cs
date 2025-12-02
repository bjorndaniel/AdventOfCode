namespace AoC2016.Tests;

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
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True('U' == result.First().First(), $"Expected 'U' but was '{result.First().First()}'");
        Assert.True('D' == result.Last().Last(), $"Expected 'D' but was '{result.Last().Last()}'");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1985" == result.Result, $"Expected 1985 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("5DB3" == result.Result, $"Expected 5DB3 but was {result}");
    }

}