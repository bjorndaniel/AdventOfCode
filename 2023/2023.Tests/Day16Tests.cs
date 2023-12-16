namespace AoC2023.Tests;
public class Day16Tests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.ParseInput(filename);

        //Then
        Assert.True(10 == result.GetLength(0), $"Expected 10 but was {result.GetLength(0)}");
        Assert.True(10 == result.GetLength(1), $"Expected 10 but was {result.GetLength(1)}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.Part1(filename, new TestPrinter(_output));

        //Then
        Assert.True("46" == result.Result, $"Expected 46 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.Part2(filename, new TestPrinter(_output));

        //Then
        Assert.True("51" == result.Result, $"Expected 51 but was {result.Result}");
    }

}