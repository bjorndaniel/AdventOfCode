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
        var first = result.First(_ => _.Name == "AA");
        Assert.True(0 == first.FlowRate, $"Expected {20}, got {first.FlowRate}");
        Assert.True(3 == first.Adjacent.Count(), $"Expected {3}, got {first.Adjacent.Count()}");
        Assert.Contains(first.Adjacent, _ => _.Name == "DD" && _.FlowRate == 20);
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
}
