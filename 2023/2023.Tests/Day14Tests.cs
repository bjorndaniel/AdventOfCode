namespace AoC2023.Tests;
public class Day14Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.ParseInput(filename);

        //Then
        Assert.True(35 == result.Count, $"Expected 35 but was {result.Count}");
        Assert.True((0,0) == (result[0].X, result[0].Y), $"Expected 0 but was {(result[0].X, result[0].Y)}");
        Assert.True(RockType.Round == result[0].Type, $"Expected 0 but was {result[0].Type}");
        Assert.True((0,0) == (result[0].X, result[0].Y), $"Expected 0 but was {(result[0].X, result[0].Y)}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("136" == result.Result, $"Expected 136 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var result = Day14.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("64" == result.Result, $"Expected 64 but was {result.Result}");
    }

}