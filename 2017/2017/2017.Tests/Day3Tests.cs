namespace AoC2017.Tests;
public class Day3Tests
{
    private readonly ITestOutputHelper _output;

    public Day3Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename);

        //Then
        Assert.Equal(12, result);
    }

    [Theory]
    [InlineData(17, -2, 2)]
    [InlineData(13, 2, 2)]
    [InlineData(1, 0, 0)]
    public static void Can_get_coordinates(int number, int expectedX, int expectedY)
    {
        //When
        var (x,y) = Day3.SpiralCoords(number);

        //Then
        Assert.True(expectedX == x, $"Expected: {expectedX}, Actual: {x}");
        Assert.True(expectedY == y, $"Expected: {expectedY}, Actual: {y}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.SolvePart1(filename, new TestPrinter(_output));

        //Then
        Assert.True("12" == result.Result, $"Expected: {12}, Actual: {result}"); 
    }
}