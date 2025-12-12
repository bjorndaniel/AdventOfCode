namespace AoC2025.Tests;

public class Day12Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.ParseInput(filename);

        //Then
        Assert.True(6 == result.presents.Count, $"Expected 5 but was {result.presents.Count}");
        Assert.True(0 == result.presents.First().Index, $"Expected 0 but was {result.presents.First().Index}");
        Assert.True(3 == result.presents.First().Shape.GetLength(0), $"Expected 3 but was {result.presents.First().Shape.GetLength(0)}");
        Assert.True(3 == result.presents.First().Shape.GetLength(1), $"Expected 3 but was {result.presents.First().Shape.GetLength(1)}");
        Assert.True(3 == result.regions.Count, $"Expected 3 but was {result.regions.Count}");
        Assert.True(2 == result.regions.First().Shapes[4], $"Expected 2 but was {result.regions.First().Shapes[4]}");
        Assert.True(12 == result.regions.Last().Size.X, $"Expected 12 but was {result.regions.Last().Size.X}");
        Assert.True(5 == result.regions.Last().Size.Y, $"Expected 12 but was {result.regions.Last().Size.Y}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day12-test.txt";

        //When
        var result = Day12.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

}