namespace AoC2023.Tests;
public class Day12Tests
{
    private readonly ITestOutputHelper _output;

    public Day12Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.ParseInput(filename);
        var result2 = Day12.ParseInput(filename, true);

        //Then
        Assert.True(6 == result.Count, $"Expected 6 but was {result.Count}");
        Assert.True("???.###" == result.First().Records, $"Expected ???.### but was {result.First().Records}");
        Assert.True(3 == result.First().Groups.Count(), $"Expected 3 but was {result.First().Groups.Count()}");
        Assert.True("?###????????" == result.Last().Records, $"Expected ?###???????? but was {result.Last().Records}");
        Assert.True(6 == result[4].Groups[1], $"Expected 6 but was {result[4].Groups[1]}");
        Assert.True(6 == result2.Count, $"Expected 6 but was {result2.Count}");
        Assert.True("???.###????.###????.###????.###????.###" == result2.First().Records, $"Expected ???.###????.###????.###????.###????.### but was {result2.First().Records}");
        Assert.True(15 == result2.First().Groups.Count(), $"Expected 15 but was {result2.First().Groups.Count()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("21" == result.Result, $"Expected 21 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("525152" == result.Result, $"Expected 525152 bur was {result.Result}");
    }

}