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