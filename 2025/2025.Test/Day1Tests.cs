namespace AoC2025.Tests;

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
        Assert.True(10 == result.Count, $"Expected 10 but was {result.Count}");
        Assert.True(68 == result.First().Steps, $"Expected 68 but was {result.First().Steps}");
        Assert.True(82 == result.Last().Steps, $"Expected 82 but was {result.First().Steps}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("3" == result.Result, $"Expected 3 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day1-test.txt";

        //When
        var result = Day1.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("6" == result.Result, $"Expected 6 but was {result.Result}");
    }

}