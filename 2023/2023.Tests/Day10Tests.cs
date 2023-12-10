namespace AoC2023.Tests;
public class Day10Tests
{
    private readonly ITestOutputHelper _output;

    public Day10Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.ParseInput(filename);

        //Then
        Assert.True(5 == result.pipes.GetLength(0), $"Expected 5 but was {result.pipes.GetLength(0)}");
        Assert.True(5 == result.pipes.GetLength(1), $"Expected 5 but was {result.pipes.GetLength(1)}");
        Assert.True('7' == result.pipes[3, 0], $"Expected 7 but was {result.pipes[3, 0]}");
        Assert.True('L' == result.pipes[0, 4], $"Expected L but was {result.pipes[0, 4]}");
        Assert.True((0, 2) == result.start, $"Expected (0,2) but was {result.start}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var result = Day10.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("8" == result.Result, $"Expected 8 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day10-test2.txt";

        //When
        var result = Day10.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("10" == result.Result, $"Exptected 10 but was {result.Result}");
    }

}