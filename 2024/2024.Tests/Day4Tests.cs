namespace AoC2024.Tests;
public class Day4Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        var p = new TestPrinter(output);
        p.PrintMatrix(result);
        p.Flush();
        Assert.Equal(10, result.GetLength(0));
        Assert.Equal(10, result.GetLength(1));
        Assert.Equal('M', result[0, 0]);
        Assert.Equal('X', result[9, 9]);
        Assert.Equal('M', result[3, 0]);
        Assert.Equal('A', result[3, 2]);
        
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(output));

        //Then
        Assert.Equal("18", result.Result);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part2(filename, new TestPrinter(output));

        //Then
        Assert.Equal("9", result.Result);
    }

}