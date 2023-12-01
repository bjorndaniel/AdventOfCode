namespace AoC2023.Tests;
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
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True("1abc2" == result.First(), $"Expected  but was {result.First()}");
        Assert.True("treb7uchet" == result.Last(), $"Expected 4 but was {result.Last()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("142" == result.Result, $"Expected 142 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test2.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("281" == result.Result, $"Expected 281 but was {result.Result}");
    }

}