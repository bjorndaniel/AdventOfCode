namespace AoC2023.Tests;
public class Day6Tests(ITestOutputHelper output)
{
    

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.ParseInput(filename);

        //Then
        Assert.True(3 == result.Count, $"Expected 3 but was {result.Count}");
        Assert.True(7 == result.First().Time, $"Expected 7 but was {result.First().Time}");
        Assert.True(9 == result.First().Record, $"Expected 9 but was {result.First().Record}");
        Assert.True(30 == result.Last().Time, $"Expected 30 but was {result.Last().Time}");
        Assert.True(200 == result.Last().Record, $"Expected 200 but was {result.Last().Record}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("288" == result.Result, $"Expected 288 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day6-test.txt";

        //When
        var result = Day6.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("71503" == result.Result, $"Expected 71503 but was {result.Result}");
    }

}