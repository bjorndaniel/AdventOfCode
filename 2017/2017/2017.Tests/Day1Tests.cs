namespace AoC2017.Tests;
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
        Assert.True("91212129" == result.First(), $"Expected 91212129 but was {result.First()}");
    }

    [Theory]
    [InlineData("Day1-test1.txt", "3")]
    [InlineData("Day1-test2.txt", "4")]
    [InlineData("Day1-test3.txt", "0")]
    [InlineData("Day1-test.txt", "9")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day1.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

    [Theory]
    [InlineData("Day1-test4.txt", "6")]
    [InlineData("Day1-test5.txt", "0")]
    [InlineData("Day1-test6.txt", "4")]
    [InlineData("Day1-test7.txt", "12")]
    [InlineData("Day1-test8.txt", "4")]
    public void Can_solve_part2_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day1.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

}