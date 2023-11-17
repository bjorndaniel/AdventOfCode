namespace AoC2022.Tests;
public class Day24Tests
{

    private readonly ITestOutputHelper _output;

    public Day24Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var (result, row, column) = Day24.ParseInput(filename);

        //Then
        Assert.True(row == 4, $"Expected row to be 4 but was {row}");
        Assert.True(column == 6, $"Expected column to be 6 but was {column}");
        Assert.True(6 == result['>'].Count(), $"Expected key > to have 6 entries but was {result['>'].Count()}");
        Assert.True(7 == result['<'].Count(), $"Expected key < to have 7 entries but was {result['<'].Count()}");
        Assert.True(4 == result['^'].Count(), $"Expected key ^ to have 4 entries but was {result['^'].Count()}");
        Assert.True(2 == result['v'].Count(), $"Expected key v to have 2 entries but was {result['v'].Count()}");
        var left = result['<'];
        Assert.Contains((2,4), left);
        Assert.Contains((1,5), left);
        Assert.Contains((1,1), left);
        Assert.Contains((0,3), left);
        Assert.Contains((1,4), left);
        Assert.Contains((3,0), left);
        Assert.Contains((0,5), left);
        var right = result['>'];    
        Assert.Contains((0,0), right);
        Assert.Contains((0,1), right);
        Assert.Contains((2,0), right);
        Assert.Contains((2,3), right);
        Assert.Contains((2,5), right);
        Assert.Contains((3,5), right);
        var up = result['^'];
        Assert.Contains((3,1), up);
        Assert.Contains((3,3), up);
        Assert.Contains((0,4), up);
        Assert.Contains((3,4), up);
        var down = result['v'];
        Assert.Contains((3,2), down);
        Assert.Contains((2,1), down);

        var printer = new TestPrinter(_output);
        foreach(var key in result)
        {
            printer.Print($"{key.Key}: {string.Join(", ", key.Value)}");
            printer.Flush();
        }
    }

    [Fact]
    public void Can_solve_part1_for_Test()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";
        
        //When
        var result = Day24.SolvePart1(filename, new TestPrinter(_output));
        
        //Then
        Assert.True(18 == result, $"Expected 18, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_Test()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.SolvePart2(filename, new TestPrinter(_output));

        //Then
        Assert.True(54 == result, $"Expected 54, got {result}");
    }
}
