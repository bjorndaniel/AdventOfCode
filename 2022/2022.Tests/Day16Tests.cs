namespace AoC2022.Tests;
public class Day16Tests
{
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
}
