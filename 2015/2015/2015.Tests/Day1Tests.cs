namespace AoC2015.Tests;
public class Day1Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.True(5 == result.Count, $"Expected 7 but was {result.Count}");
        Assert.True(')' == result.Last(), $"Expected ) but was {result.Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("-1" == result.Result, $"Expected -1 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("5" == result.Result, $"Expected 5 but was {result.Result}");
    }

}