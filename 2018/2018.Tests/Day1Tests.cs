namespace AoC2018.Tests;

public class Day1Tests
{
    private readonly ITestOutputHelper _output;

    public Day1Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.True(1 == result.First().change, $"Expected 0 but was {result.First().change}");
        Assert.True(result.First().isPositive, $"Expected true but was {result.First().isPositive}");
        Assert.True(2 == result[1].change, $"Expected 2 but was {result[2].change}");
        Assert.False(result[1].isPositive, $"Expected false but was {result[1].isPositive}");
    }

    [Theory]
    [InlineData("Day1-test.txt", "3")]
    [InlineData("Day1-test2.txt", "-6")]
    [InlineData("Day1-test3.txt", "0")]
    public void Can_solve_part1_for_test(string file, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{file}";

        //When
        var result = Day1.Part1(filename,  new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected result to be -3 but was {result.Result}");
    }

    [Theory]
    [InlineData("Day1-test.txt", "2")]
    [InlineData("Day1-test4.txt", "14")]
    public void Can_solve_part2_for_test(string file, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{file}";

        //When
        var result = Day1.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected result to be 2 but was {result.Result}");
    }
}