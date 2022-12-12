namespace AoC2022.Tests;
public class Day12Tests
{
    private readonly ITestOutputHelper _output;

    public Day12Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var (matrix, start, end) = Day12.ParseInput(filename);

        //Then
        Assert.True(start.X == 0 && start.Y == 0, $"Expected start to be 0,0, got {start.X},{start.Y}");
        Assert.True(end.X == 5 && end.Y == 2, $"Expected start to be 0,0, got {start.X},{start.Y}");
        Assert.True(matrix[0, 0] == 'S', $"Expected S, got {matrix[0, 0]}");
        Assert.True(matrix[4, 1] == 'b', $"Expected b, got {matrix[4, 1]}");
        Assert.True(matrix[4, 2] == 'd', $"Expected d, got {matrix[4, 2]}");
        Assert.True(matrix[2, 5] == 'E', $"Expected E, got {matrix[2, 5]}");
        Assert.True(matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1] == 'i', $"Expected i, got {matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var (result, matrix) = Day12.SolvePart1(filename);

        //Then
        Print(matrix);
        Assert.True(31 == result, $"Expected 31, got {result}");
    }

    private void Print(int[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            var sb = new StringBuilder();
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                if (row == 0 && col == 0)
                {
                    sb.Append("  S");
                }
                else if (row == 2 && col == 5)
                {
                    sb.Append("  E");
                }
                else if (matrix[row, col] > 99)
                {
                    sb.Append("  m");
                }
                else
                {
                    sb.Append(matrix[row, col].ToString().PadLeft(3, ' '));
                }
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}
