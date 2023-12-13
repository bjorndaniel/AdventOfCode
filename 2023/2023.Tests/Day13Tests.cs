namespace AoC2023.Tests;
public class Day13Tests
{
    private readonly ITestOutputHelper _output;

    public Day13Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.ParseInput(filename);

        //Then
        Assert.True(2 == result.Count, $"Expected 2 but was {result.Count}");
        Assert.True('.' == result.First()[8, 6], $"Expected . but was {result.First()[8, 6]}");
        Assert.True('#' == result.Last()[8, 6], $"Expected # but was {result.Last()[8, 6]}");
        Assert.True("##......#" == Helpers.GetRow(result.First(), 2), $"Expected ##......# but was {Helpers.GetRow(result.First(), 2)}");
        Assert.True("..##..###" == Helpers.GetRow(result.Last(), 2), $"Expected ..##..### but was {Helpers.GetRow(result.Last(), 2)}");
        Assert.True("..##..." == Helpers.GetColumn(result.First(), 8), $"Expected ..##... but was {Helpers.GetColumn(result.First(), 8)}");
        Assert.True("##.##.#" == Helpers.GetColumn(result.Last(), 0), $"Expected ##.##.# but was {Helpers.GetColumn(result.Last(), 0)}");
    }

    [Fact]
    public void Can_get_reflection()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test2.txt";

        //When
        var pattern = Day13.ParseInput(filename);
        var result = Day13.GetReflection(pattern.First(), (-1, -1));
        var result2 = Day13.GetReflection(pattern.First(), (result.col, result.row), true);

        //Then
        Assert.True(14 == result.sum, $"Expected 14 but was {result.sum}");
        Assert.True(16 == result2.sum, $"Expected 16 but was {result2.sum}");
        Assert.True(result.sum != result2.sum);
        Assert.True(result2.sum == 16, $"Expected 16 but was {result2.sum}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("405" == result.Result, $"Expected 405 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("400" == result.Result, $"Expected 400 but was {result.Result}");
    }

}