namespace AoC2022.Tests;
public class Day22Tests
{
    private readonly ITestOutputHelper _output;

    public Day22Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var (matrix, moves) = Day22.ParseInput(filename);

        //Then
        Print(matrix);
        Assert.True(12 == matrix.Count, $"Expected 12, got {matrix.Count}");
        Assert.True(16 == matrix.First().Squares.Count, $"Expected 16, got {matrix.First().Squares.Count}");
        Assert.True('#' == matrix.First().Squares.ElementAt(11).Value, $"Expected #, got {matrix.First().Squares.ElementAt(11).Value}");
        Assert.True(null == matrix.First().Squares.ElementAt(12).Value, $"Expected null, got {matrix.First().Squares.ElementAt(12).Value}");
        Assert.True(null == matrix.First().Squares.ElementAt(0).Value, $"Expected null, got {matrix.First().Squares.ElementAt(0).Value}");
        Assert.True(BoardType.Void == matrix.First().Squares.ElementAt(0).Type, $"Expected Void, got {matrix.First().Squares.ElementAt(0).Type}");
        Assert.True(13 == moves.Count(), $"Expected 13 got {moves.Count()}");
        Assert.True(10 == moves.ElementAt(6).Length!.Value, $"Expected 10, got {moves.ElementAt(6).Length!.Value}");
        Assert.True(Turn.Right == moves.ElementAt(1).Direction!.Value, $"Expected Right, got {moves.ElementAt(6).Direction}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var (result, matrix) = Day22.SolvePart1(filename, new TestPrinter(_output));

        //Then
        Print(matrix);
        Assert.True(6032 == result, $"Expected 6032, got {result}");

    }

    private void Print(List<BoardRow> matrix)
    {
        foreach (var row in matrix)
        {
            var sb = new StringBuilder();
            foreach (var square in row.Squares)
            {
                sb.Append(square.Value);
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}
