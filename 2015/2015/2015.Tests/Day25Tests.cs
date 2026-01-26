namespace AoC2015.Tests;

public class Day25Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.ParseInput(filename);

        //Then
        Assert.True(2 == result.row, $"Expected 2 but was {result.row}");
        Assert.True(1 == result.column, $"Expected 1 but was {result.column}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("31916031" == result.Result, $"Expected 31916031 but was {result.Result}");
    }
}