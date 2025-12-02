namespace AoC2016.Tests;

public class Day12Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.ParseInput(filename);

        //Then
        Assert.True(6 == result.Count, $"Expected 6 but was {result.Count}");
        Assert.True(OpCode.Cpy == result.First().OpCode, $"Expected Cpy but was {result.First().OpCode}");
        Assert.True("41" == result.First().Arg1, $"Expected 41 but was {result.First().Arg1}");
        Assert.True("a" == result.First().Arg2, $"Expected a but was {result.First().Arg2}");
        Assert.True(OpCode.Dec == result.Last().OpCode, $"Expected Dec but was {result.Last().OpCode}");
        Assert.True("a" == result.Last().Arg1, $"Expected a but was {result.Last().Arg1}");
        Assert.True(null == result.Last().Arg2, $"Expected null but was {result.Last().Arg2}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("42" == result.Result, $"Expected 42 but was {result.Result}");
    }
}