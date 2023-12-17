namespace AoC2023.Tests;
public class Day17Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.ParseInput(filename);

        //Then
        var printer = new TestPrinter(output);
        printer.PrintMatrixXY(result);
        printer.Flush();

        Assert.True(13 == result.GetLength(1), $"Expected 13 but was {result.GetLength(1)}");
        Assert.True(13 == result.GetLength(0), $"Expected 13 but was {result.GetLength(0)}");
        Assert.True(2 == result[0, 0], $"Expected 2 but was {result[0, 0]}");
        Assert.True(3 == result[12, 12], $"Expected 3 but was {result[12, 12]}");
        Assert.True(9 == result[5, 5], $"Expected 9 but was {result[5, 5]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("102" == result.Result, $"Expected 102 but was {result.Result}");
    }

    [Theory]
    [InlineData("Day17-test.txt", "94")]
    [InlineData("Day17-test2.txt", "71")]
    public void Can_solve_part2_for_test(string input, string expected)
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}{input}";

        //When
        var result = Day17.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True(expected == result.Result, $"Expected {expected} but was {result.Result}");
    }

}