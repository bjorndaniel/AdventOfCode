namespace AoC2023.Tests;
public class Day15Tests(ITestOutputHelper output)
{
    

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.ParseInput(filename);

        //Then
        Assert.True(11 == result.Count, $"Expected 11 but was {result.Count}");
        Assert.True("rn=1" == result[0], $"Expected rn-1 but was {result[0]}");
        Assert.True("ot=7" == result[10], $"Expected ot=7 but was {result[10]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1320" == result.Result, $"Expected 1320 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day15-test.txt";

        //When
        var result = Day15.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("145" == result.Result, $"Expected 145 but was {result.Result}");
    }

}