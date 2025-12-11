namespace AoC2025.Tests;

public class Day11Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        Assert.True(10 == result.Count, $"Expected 10 but was {result.Count}");
        Assert.True("aaa" == result.First().Name, $"Expected aaa but was {result.First().Name}");
        Assert.True("you" == result.First().Outputs.First(), $"Expected you but was {result.First().Outputs.First()}");
        Assert.True("hhh" == result.First().Outputs.Last(), $"Expected hhh but was {result.First().Outputs.Last()}");
        Assert.True("iii" == result.Last().Name, $"Expected aaa but was {result.Last().Name}");
        Assert.True("out" == result.Last().Outputs.First(), $"Expected you but was {result.Last().Outputs.First()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("5" == result.Result, $"Expected 5 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test2.txt";

        //When
        var result = Day11.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

}