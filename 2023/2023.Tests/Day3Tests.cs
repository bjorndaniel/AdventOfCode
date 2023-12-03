namespace AoC2023.Tests;
public class Day3Tests
{
    private readonly ITestOutputHelper _output;

    public Day3Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename);

        //Then
        Assert.True(10 == result.Parts.Count, $"Expected 10 but was {result.Parts.Count}");
        Assert.True(6 == result.Symbols.Count, $"Expected 6 but was {result.Symbols.Count}");
        Assert.True(3 == result.Parts.First(_ => _.Value == 467).Width, $"Expected 3 but was {result.Parts.First(_ => _.Value == 467).Width}");
        Assert.True(0 == result.Parts.First(_ => _.Value == 467).X, $"Expected 0 but was {result.Parts.First(_ => _.Value == 467).X}");
        Assert.True(2 == result.Parts.First(_ => _.Value == 35).Width, $"Expected 2 but was {result.Parts.First(_ => _.Value == 35).Width}");
        Assert.True(2 == result.Parts.First(_ => _.Value == 35).Y, $"Expected 0 but was {result.Parts.First(_ => _.Value == 35).Y}");
        Assert.True(3 == result.Symbols.Count(_ => _.C == '*'), $"Expected 3 but was {result.Symbols.Count(_ => _.C == '*')}");
        Assert.True(6 == result.Symbols.First(_ => _.C == '#').X, $"Expected 6 but was {result.Symbols.First(_ => _.C == '#').X}");
        Assert.True(3 == result.Symbols.First(_ => _.C == '#').Y, $"Expected 3 but was {result.Symbols.First(_ => _.C == '#').Y}");
    }

    [Theory]
    [InlineData("Day3-test.txt", "4361")]
    [InlineData("Day3-test2.txt", "413")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day3.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("467835" == result.Result, $"Expected 467835 but was {result.Result}");
    }

}