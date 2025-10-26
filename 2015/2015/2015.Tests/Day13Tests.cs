namespace AoC2015.Tests;
public class Day13Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.ParseInput(filename);

        //Then
        Assert.True(result.Count == 4, $"Expected 4 but got {result.Count}");
        Assert.Contains(result, g => g.Name == "Alice");
        Assert.True(result.First(_ => _.Name == "Alice").Happiness.Count == 3, "Alice should have 3 happiness entries");
        Assert.True(result.First(_ => _.Name == "Alice").GetHappiness("Bob") == 54);
        Assert.True(result.First(_ => _.Name == "Bob").GetHappiness("Alice") == 83);
        Assert.True(result.First(_ => _.Name == "Carol").GetHappiness("Bob") == 60);
        Assert.True(result.First(_ => _.Name == "David").GetHappiness("Bob") == -7);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("330" == result.Result, $"Expected 330 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day13-test.txt";

        //When
        var result = Day13.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("286" == result.Result, $"Expected 286 but was {result.Result}");
    }

}