namespace AoC2023.Tests;
public class Day2Tests
{
    private readonly ITestOutputHelper _output;

    public Day2Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.ParseInput(filename);

        //Then
        Assert.True(5 == result.Count, $"Expected 5 but was {result.Count}");
        Assert.True(3 == result.First().Grabs.Count, $"Expected 3 but was {result.First().Grabs.Count}");
        Assert.True(7 == result.First().Grabs.First().Cubes.Count, $"Expected 7 but was {result.First().Grabs.First().Cubes.Count}");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("8" == result.Result, $"Expected 8 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day2-test.txt";

        //When
        var result = Day2.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("2286" == result.Result, $"Expected 2286 but was {result.Result}");
    }

}