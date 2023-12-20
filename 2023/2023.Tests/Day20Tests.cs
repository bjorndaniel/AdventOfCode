namespace AoC2023.Tests;
public class Day20Tests(ITestOutputHelper output)
{

    [Theory]
    [InlineData("Day20-test.txt", 5)]
    [InlineData("Day20-test2.txt", 5)]
    public void Can_parse_input(string input, int expected)
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day20.ParseInput(filename);

        //Then
        Assert.True(expected == result.Count,$"Expected {expected} but was {result.Count}");

        if (input.Contains("test2"))
        {
            Assert.True(2 == result["con"].Inputs.Count, $"Expected 2 but was {result["con"].Inputs.Count}");
            Assert.True("a" == result["broadcaster"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}"), $"Expected a but was {result["broadcaster"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}")}");
        }
        else 
        {
            Assert.True(ModuleType.Broadcaster == result["broadcaster"].Type, $"Expected Broadcaster but was {result["broadcaster"].Type}");
            Assert.True("abc" == result["broadcaster"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}"), $"Expected abc but was {result["broadcaster"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}")}");
            Assert.True(ModuleType.FlipFlop == result["a"].Type, $"Expected FlipFlop but was {result["a"].Type}");
            Assert.True("b" == result["a"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}"), $"Expected abc but was {result["a"].Outputs.Select(_ => _.module).Aggregate((a, b) => $"{a}{b}")}");
            Assert.True(ModuleType.Conjuction == result["inv"].Type, $"Expected Conjunction but was {result["inv"].Type}");
        }
        
    }

    [Theory]
    [InlineData("Day20-test.txt", "32000000")]
    [InlineData("Day20-test2.txt", "11687500")]
    public void Can_solve_part1_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day20.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }
    

}