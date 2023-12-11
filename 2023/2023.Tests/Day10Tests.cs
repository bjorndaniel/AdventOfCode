namespace AoC2023.Tests;
public class Day10Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day10-test.txt";

        //When
        var (pipes, start) = Day10.ParseInput(filename);

        //Then
        Assert.True(5 == pipes.GetLength(0), $"Expected 5 but was {pipes.GetLength(0)}");
        Assert.True(5 == pipes.GetLength(1), $"Expected 5 but was {pipes.GetLength(1)}");
        Assert.True('7' == pipes[3, 0], $"Expected 7 but was {pipes[3, 0]}");
        Assert.True('L' == pipes[0, 4], $"Expected L but was {pipes[0, 4]}");
        Assert.True((0, 2) == start, $"Expected (0,2) but was {start}");
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
        Assert.True("10" == result.Result, $"Expected 10 but was {result.Result}");
    }

}