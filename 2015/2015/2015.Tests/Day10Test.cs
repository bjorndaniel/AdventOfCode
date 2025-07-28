namespace AoC2015.Tests;
public class Day10Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.True(new List<int> { 2, 1, 1 }.SequenceEqual(result), $"Expected 2,1,1 but was {result.Select(_ => _.ToString()).Aggregate((a,b) => $"{a},{b}")}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("4".Equals(result.Result), $"Expected 4 but was {result.Result}");
    }
}