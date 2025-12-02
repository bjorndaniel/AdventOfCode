namespace AoC2016.Tests;

public class Day10Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.True(3 == result.valueInstructions.Count,$"Expected 3 but was {result.valueInstructions.Count}");
        Assert.True(3 == result.valueInstructions.Count,$"Expected 3 but was {result.botInstructions.Count}");
        Assert.True(5 == result.valueInstructions.First().Value,$"Expected 5 but was {result.valueInstructions.First().Value}");
        Assert.True(2 == result.valueInstructions.First().BotId,$"Expected 2 but was {result.botInstructions.First().BotId}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Exptected 2 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("30" == result.Result, $"Expected 30 but was {result.Result}");
    }

}