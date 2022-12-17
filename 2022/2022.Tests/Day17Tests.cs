namespace AoC2022.Tests;
public class Day17Tests
{

    private readonly ITestOutputHelper _output;

    public Day17Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.ParseInput(filename);

        //Then
        Assert.True(40 == result.Count(), $"Expected 40, got {result.Count()}");
        Assert.True(21 == result.Count(_ => _ == Day17.Direction.Right), $"Expected 21, got {result.Count(_ => _ == Day17.Direction.Right)}");
        Assert.True(19 == result.Count(_ => _ == Day17.Direction.Left), $"Expected 19, got {result.Count(_ => _ == Day17.Direction.Left)}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";
        var printer = new TestPrinter(_output);

        //When
        var result = Day17.SolvePart1(filename, 5, printer);

        //Then
        Assert.True(3068 == result.Height, $"Expected 3068, got {result.Height}");
        //result.Print(printer);
    }
}
