namespace AoC2017.Tests;
public class Day4Tests
{
    private readonly ITestOutputHelper _output;

    public Day4Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test2.txt";

        //When
        var result = Day4.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("3" == result.Result, $"Expected 3 but was {result.Result}");
    }
}