namespace AoC2015.Tests;
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
        Assert.True(2 == result.Count, $"Expected 2 but was {result.Count}");
        Assert.True(2 == result.Last().Length, $"Expected 2 but was {result.Last().Length}");
        Assert.True(3 == result.Last().Width, $"Expected 3 but was {result.Last().Width}");
        Assert.True(4 == result.Last().Height, $"Expected 4 but was {result.Last().Height}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("101" == result.Result, $"Expected 101 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("48" == result.Result, $"Expected 48 but was {result.Result}");
    }

}