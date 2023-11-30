namespace AoC2018.Tests;
public class Day7Tests
{
    private readonly ITestOutputHelper _output;

    public Day7Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.ParseInput(filename);

        //Then
        Assert.True(7 == result.Count, $"Expected 7 but was {result.Count}");
        Assert.True('C' == result.First().step, $"Expected C but was {result.First().step}");
        Assert.True('A' == result.First().neighbor, $"Expected A but was {result.First().neighbor}");
        Assert.True('F' == result.Last().step, $"Expected F but was {result.Last().step}");
        Assert.True('E' == result.Last().neighbor, $"Expected E but was {result.Last().neighbor}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("CABDFE" == result.Result, $"Expected CABDFE but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day7-test.txt";

        //When
        var result = Day7.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("15" == result.Result, $"Expected 15 but was {result.Result}");
    }

}