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
        var result = Day14.ParseInput(filename);

        //Then
        Print(result);
        Assert.True(9 == result.GetLength(0), $"Expected 9, got {result.GetLength(0)}");
        Assert.True(503 == result.GetLength(1), $"Expected 9, got {result.GetLength(1)}");
    }

    private void Print(char?[,] matrix)
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            var sb = new StringBuilder();
            for (int col = 494; col < matrix.GetLength(1); col++)
            {
                sb.Append(matrix[row, col] ?? '.');
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}
