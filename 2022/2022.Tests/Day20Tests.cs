namespace AoC2022.Tests;
public class Day20Tests
{
    private readonly ITestOutputHelper _output;

    public Day20Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.ParseInput(filename);

        //Then
        Assert.True(7 == result.Count, $"Expected 7, got {result.Count}");
        Assert.True(
            "1, 2, -3, 3, -2, 0, 4" ==
            result.Print(),
            $"Expected 1,2,-3,3,-2,0,4, got {result.Print()}"
        );
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.SolvePart1(filename, new TestPrinter(_output));

        //Then
        Assert.True(3 == result, $"Expected 3, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day20-test.txt";

        //When
        var result = Day20.SolvePart2(filename, new TestPrinter(_output));

        //Then
        Assert.True(1623178306 == result, $"Expected 1623178306, got {result}");
    }
}
