


namespace AoC2015.Tests;

public class Day23Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count,$"Expected 4 but was {result.Count}");
        Assert.True(InstructionType.inc == result[0].Type, $"Expected inc but was {result[0].Type}");
        Assert.True("a" == result[0].Register, $"Expected a but was {result[0].Register}");
        Assert.True(2 == result[1].Offset, $"Expected 2 but was {result[0].Offset}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("A: 2, B: 0" == result.Result, $"Expected A: 2, B: 0 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(false);
    }

}