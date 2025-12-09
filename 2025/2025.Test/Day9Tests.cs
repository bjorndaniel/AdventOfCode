namespace AoC2025.Tests;

public class Day9Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.True(11 == result.Max(_ => _.x), $"Expected 11 but was {result.Max(_ => _.x)}");
        Assert.True(7 == result.Max(_ => _.y), $"Expected 7 but was {result.Max(_ => _.y)}");
        Assert.Contains(result, _ => _.x == 2 && _.y == 5);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("50" == result.Result, $"Expected 50 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day9-test.txt";

        //When
        var result = Day9.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("24" == result.Result,$"Expected 24 but was {result.Result}");
    }

}