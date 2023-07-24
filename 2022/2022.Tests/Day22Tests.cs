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
        var printer = new TestPrinter(_output);
        printer.PrintMatrix(matrix);
        printer.Flush();
        Assert.True(12 == matrix.GetLength(0), $"Expected 12 rows, got {matrix.GetLength(0)}");
        Assert.True(16 == matrix.GetLength(1), $"Expected 16 cols, got {matrix.GetLength(1)}");
        Assert.True('#' == matrix[0, 11].Value, $"Expected #, got {matrix[0, 11].Value}");
        Assert.True(null == matrix[0, 12].Value, $"Expected null, got {matrix[0, 12]}");
        Assert.True(null == matrix[0, 0].Value, $"Expected null, got {matrix[0, 0].Value}");
        Assert.True(BoardType.Void == matrix[0, 0].Type, $"Expected Void, got {matrix[0, 0].Type}");
        Assert.True(13 == moves.Count(), $"Expected 13 got {moves.Count()}");
        Assert.True(10 == moves.ElementAt(6).Length!.Value, $"Expected 10, got {moves.ElementAt(6).Length!.Value}");
        Assert.True(Turn.Right == moves.ElementAt(1).Direction!.Value, $"Expected Right, got {moves.ElementAt(6).Direction}");
    }

    [Theory]
    [InlineData("Day22-test.txt", 6032)]
    [InlineData("Day22-test1.txt", 1036)]
    [InlineData("Day22-test2.txt", 3045)]
    [InlineData("Day22-test3.txt", 1047)]
    [InlineData("Day22-test4.txt", 12049)]
    [InlineData("Day22-test5.txt", 11056)]
    [InlineData("Day22-test6.txt", 4049)]
    [InlineData("Day22-test7.txt", 2047)]
    [InlineData("Day22-test8.txt", 8034)]
    [InlineData("Day22-test9.txt", 2037)]
    [InlineData("Day22-test10.txt", 5022)]
    [InlineData("Day22-test11.txt", 4049)]
    [InlineData("Day22-test12.txt", 1038)]
    [InlineData("Day22-test13.txt", 1038)]
    [InlineData("Day22-test14.txt", 1041)]
    [InlineData("Day22-test15.txt", 1045)]
    [InlineData("Day22-test16.txt", 1045)]
    [InlineData("Day22-test17.txt", 1043)]
    [InlineData("Day22-test18.txt", 1047)]
    public void Can_solve_part1_for_test(string input, int expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";
        //var filename = $"{Helpers.DirectoryPath}Day22.txt";

        //When
        var (result, matrix) = Day22.SolvePart1(filename, new TestPrinter(_output));

        //Then
        var printer = new TestPrinter(_output);
        printer.PrintMatrix(matrix);
        printer.Flush();
        Assert.True(expected == result, $"Expected {expected}, got {result}");

    }

}
