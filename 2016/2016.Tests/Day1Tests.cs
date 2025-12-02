namespace AoC2016.Tests;

public class Day1Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True(result.First().Direction == Direction.Right && result.First().Distance == 5, $"Expected (1,2) but was ({result.First().Direction},{result.First().Distance})");
        Assert.True(result.Last().Direction == Direction.Right && result.Last().Distance == 3, $"Expected (1,2) but was ({result.First().Direction},{result.First().Distance})");

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("12" == result.Result, $"Expected 12 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test2.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("4" == result.Result, $"Expected 4 but was {result.Result}");
    }

}