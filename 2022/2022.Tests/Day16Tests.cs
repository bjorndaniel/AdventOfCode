namespace AoC2022.Tests;
public class Day16Tests
{
    private readonly ITestOutputHelper _output;

    public Day16Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.ParseInput(filename);

        //Then
        Assert.True(10 == result.Count(), $"Expected 10 valves, got {result.Count()}");
        var first = result.First();
        Assert.True("AA" == first.Key, $"Expected AA, got {first.Key}");
        Assert.True(0 == first.Value.FlowRate, $"Expected {20}, got {first.Value.FlowRate}");
        Assert.True(3 == first.Value.Adjacent.Count(), $"Expected {3}, got {first.Value.Adjacent.Count()}");
        Assert.Contains(first.Value.Adjacent, _ => _.Name == "DD" && _.FlowRate == 20);
        Assert.True(result["HH"].Adjacent.Any(_ => _.Name == "GG"), $"Expected HH to have GG as adjacent");
        Assert.True(result["JJ"].Adjacent.Any(_ => _.Name == "II"), $"Expected JJ to have II as adjacent");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.SolvePart1(filename, new TestPrinter(_output));

        //Then
        Assert.Equal(1651, result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.SolvePart2(filename, new TestPrinter(_output));

        //Then
        Assert.Equal(1707, result);
    }
}
