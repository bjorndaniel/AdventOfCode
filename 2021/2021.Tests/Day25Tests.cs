namespace Advent2021.Tests;
public class Day25Tests
{
    private readonly ITestOutputHelper _output;

    public Day25Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_read_board()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day25-test.txt";

        //When
        var result = Day25.ReadSeaBottom(filename);
        Print(result);

        //Then
        Assert.Equal(10, result.GetLength(0));
        Assert.Equal(9, result.GetLength(1));
        Assert.Equal('v', result[0, 0]);
    }

    [Fact]
    public void Can_make_one_move()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day25-test.txt";
        var field = Day25.ReadSeaBottom(filename);


        //When
        var (success, newField) = Day25.MoveOne(field);
        Print(newField);

        //Then
        Assert.True(success);
        Assert.Equal('>', newField[4, 0]);
        Assert.Equal('>', newField[6, 0]);
        Assert.Equal('.', newField[0, 0]);
        Assert.Equal('v', newField[newField.GetLength(0) - 1, newField.GetLength(1) - 1]);

    }


    [Fact]
    public void Can_count_moves()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day25-test.txt";

        //When
        var result = Day25.CountMoves(filename);

        //Then
        Assert.Equal(58, result);
    }

    private void Print(char[,] field)
    {
        for (int row = 0; row < field.GetLength(1); row++)
        {
            var sb = new StringBuilder();
            for (int col = 0; col < field.GetLength(0); col++)
            {
                sb.Append(field[col, row]);
            }
            _output.WriteLine(sb.ToString());
            _output.WriteLine("");
        }
    }
}
