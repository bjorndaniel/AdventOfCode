namespace AoC2018.Tests;
public class Day9Tests
{
    private readonly ITestOutputHelper _output;

    public Day9Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.True(10 == result.Players, $"Expect 10 but was {result.Players}");
        Assert.True(1618 == result.MarbleScore, $"Expect 1618 but was {result.MarbleScore}");
    }

    [Theory]
    [InlineData("Day9-test.txt", "8317")]
    [InlineData("Day9-test1.txt", "146373")]
    [InlineData("Day9-test2.txt", "2764")]
    [InlineData("Day9-test3.txt", "54718")]
    [InlineData("Day9-test4.txt", "37305")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day9.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

}