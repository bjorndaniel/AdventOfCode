namespace AoC2022.Tests;
public class Day14Tests
{
    private readonly ITestOutputHelper _output;

    public Day14Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var (result, floorLevel) = Day14.ParseInput(filename);
        

        //Then
        Print(result);
        Assert.True(10 == result.GetLength(0), $"Expected 10, got {result.GetLength(0)}");
        Assert.True(504 == result.GetLength(1), $"Expected 504, got {result.GetLength(1)}");
        Assert.True(result[4, 498] == '#', $"Expected #, got {result[4, 498]}");
        Assert.True(result[5, 498] == '#', $"Expected #, got {result[5, 498]}");
        Assert.True(result[6, 498] == '#', $"Expected #, got {result[6, 498]}");
        Assert.True(result[6, 497] == '#', $"Expected #, got {result[6, 497]}");
        Assert.True(result[6, 496] == '#', $"Expected #, got {result[6, 496]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var (result, matrix) = Day14.SolvePart1(filename);

        //Then
        Print(matrix);
        Assert.True(24 == result, $"Expected 24, got {result}");

    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day14-test.txt";

        //When
        var (result, matrix) = Day14.SolvePart2(filename);

        //Then
        Print(matrix);
        Assert.True(93 == result, $"Expected 93, got {result}");

    }

    private void Print(char?[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            var sb = new StringBuilder();
            for (int col = 480; col < matrix.GetLength(1); col++)
            {
                sb.Append(matrix[row, col] ?? '.');
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}
