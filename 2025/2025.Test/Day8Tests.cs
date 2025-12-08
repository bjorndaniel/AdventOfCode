namespace AoC2025.Tests;

public class Day8Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.True(20 == result.Count, $"Expected 20 but was {result.Count}");
        Assert.True(162 == result.First().X, $"Expected 162 but was {result.First().X}");
        Assert.True(817 == result.First().Y, $"Expected 162 but was {result.First().Y}");
        Assert.True(812 == result.First().Z, $"Expected 162 but was {result.First().Z}");
        Assert.True(425 == result.Last().X, $"Expected 425 but was {result.Last().X}");
        Assert.True(690 == result.Last().Y, $"Expected 690 but was {result.Last().Y}");
        Assert.True(689 == result.Last().Z, $"Expected 689 but was {result.Last().Z}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("40" == result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("25272" == result.Result, $"Expected 25272 but was {result.Result}");
    }

}