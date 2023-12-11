namespace AoC2023.Tests;
public class Day9Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
        Assert.True(0 == result[0][0], $"Expected 0 but was {result[0][0]}");
        Assert.True(15 == result[0].Last(), $"Expected 15 but was {result[0].Last()}");
        Assert.True(10 == result.Last()[0], $"Expected 10 but was {result.Last()[0]}");
        Assert.True(45 == result.Last().Last(), $"Expected 45 but was {result.Last().Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("114" == result.Result, $"Exptected 114 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

}