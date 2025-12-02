namespace AoC2016.Tests;

public class Day8Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True(result.First().Command is Command.Rect, $"Expected Rect but was {result.First().Command}");
        Assert.True(result.First().First == 3, $"Expected 3 but was {result.First().Command}");
        Assert.True(result.First().Second == 2, $"Expected 2 but was {result.First().Second}");
        Assert.True(result.Last().Command is Command.RotateColumn, $"Expected Column but was {result.Last().Command}");
        Assert.True(result.Last().First == 1, $"Expected 1 but was {result.Last().Command}");
        Assert.True(result.Last().Second == 1, $"Expected 1 but was {result.Last().Second}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("6" == result.Result, $"Expected 6 but was {result.Result}");
    }
}