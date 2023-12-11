namespace AoC2023.Tests;
public class Day8Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.True(7 == result.Nodes.Count, $"Expected 7 but was {result.Nodes.Count}");
        Assert.True(2 == result.Instructions.Count, $"Expected 2 but was {result.Instructions.Count}");
        Assert.True(result.Nodes.First(_ => _.Name == "AAA").Left.Name == "BBB", $"Expected BBB but was {result.Nodes.First(_ => _.Name == "AAA").Left.Name}");
        Assert.True(result.Nodes.First(_ => _.Name == "AAA").Right.Name == "CCC", $"Expected CCC but was {result.Nodes.First(_ => _.Name == "AAA").Right.Name}");
        Assert.True(result.Nodes.First(_ => _.Name == "BBB").Left.Name == "DDD", $"Expected EEE but was {result.Nodes.First(_ => _.Name == "BBB").Left.Name}");
        Assert.True(result.Nodes.First(_ => _.Name == "BBB").Right.Name == "EEE", $"Expected EEE but was {result.Nodes.First(_ => _.Name == "BBB").Right.Name}");
        Assert.True(result.Nodes.First(_ => _.Name == "GGG").Left.Name == "GGG", $"Expected GGG but was {result.Nodes.First(_ => _.Name == "GGG").Left.Name}");
        Assert.True(result.Nodes.First(_ => _.Name == "GGG").Right.Name == "GGG", $"Expected GGG but was {result.Nodes.First(_ => _.Name == "GGG").Right.Name}");
    }

    [Theory]
    [InlineData("Day8-test.txt", "2")]
    [InlineData("Day8-test2.txt", "6")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day8.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test3.txt";

        //When
        var result = Day8.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("6" == result.Result, $"Expected 6 but was {result.Result}");
    }

}