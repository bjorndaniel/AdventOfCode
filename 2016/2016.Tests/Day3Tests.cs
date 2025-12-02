namespace AoC2016.Tests;

public class Day3Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.ParseInput(filename);

        //Then
        Assert.True((5,10,25) == result.First(), $"Exptected 5,10,25 but was {result.First()}" );
        Assert.True((10,10,15) == result.Last(), $"Exptected 10,10,15 but was {result.Last()}" );
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test.txt";

        //When
        var result = Day3.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("1" == result.Result, $"Expected 1 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day3-test2.txt";

        //When
        var result = Day3.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("6" == result.Result, $"Expected 6 but was {result.Result}");
    }

}