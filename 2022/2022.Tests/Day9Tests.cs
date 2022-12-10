namespace AoC2022.Tests;
public class Day9Tests
{
    private readonly ITestOutputHelper _output;

    public Day9Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day9-test.txt";

        //When
        var result = Day9.ParseInput(filename);

        //Then
        Assert.Equal(8, result.Count());
        Assert.Equal('R', result.First().Direction);
        Assert.Equal(4, result.First().Length);
        Assert.Equal('R', result.Last().Direction);
        Assert.Equal(2, result.Last().Length);

    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day9-test.txt";

        //When
        var result = Day9.SolvePart1(filename);

        //Then
        Assert.Equal(13, result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day9-test2.txt";

        //When
        var result = Day9.SolvePart2(filename);

        //Then
        Print(result);
        Assert.Equal(36, result.Count());
    }

    private void Print(Dictionary<(int x, int y), int> visited)
    {
        var minRow = visited.Keys.Min(_ => _.y);
        var maxRow = visited.Keys.Max(_ => _.y);
        var minCol = visited.Keys.Min(_ => _.x);
        var maxCol = visited.Keys.Max(_ => _.y);
        for (int row = maxRow; row >= minRow; row--)
        {
            var sb = new StringBuilder();
            for (int col = minCol; col < maxCol; col++)
            {
                
                if (visited.ContainsKey((row, col)))
                {
                    var value = visited[(row, col)];
                    sb.Append((col == 0 && row ==0) ? "s" : "#");
                }
                else
                {
                    sb.Append(".");
                }
            }
            _output.WriteLine(sb.ToString());
        }
        _output.WriteLine("");
    }
}
