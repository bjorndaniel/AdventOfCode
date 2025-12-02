namespace AoC2025.Tests;

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
        Assert.True(11 == result.Count, $"Expected 11 but was {result.Count}");
        Assert.True(11 == result.First().from, $"Expected first from to be 11 but was {result.First().from}");
        Assert.True(22 == result.First().to, $"Expected first to to be 14 but was {result.First().to}");
        Assert.True(2121212118 == result.Last().from, $"Expected last from to be 2121212118 but was {result.Last().from}");
        Assert.True(2121212124 == result.Last().to, $"Expected last to to be 2121212124 but was {result.Last().to}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1227775554" == result.Result, $"Exptected 1227775554 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("4174379265" == result.Result, $"Exptected 4174379265 but was {result.Result}");
    }

}