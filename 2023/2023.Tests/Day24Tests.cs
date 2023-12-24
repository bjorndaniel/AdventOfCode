namespace AoC2023.Tests;
public class Day24Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.ParseInput(filename);

        //Then
        Assert.True(5 == result.Count, $"Expected 5 but was {result.Count}");
        var first = result.First();
        Assert.True((19,13,30) == (first.X, first.Y, first.Z), $"Expected (19,13,30) but was {(first.X, first.Y, first.Z)}");
        Assert.True((-2,1,-2) == (first.Vx, first.Vy, first.Vz), $"Expected (19,13,30) but was {(first.Vx, first.Vy, first.Vz)}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("2" == result.Result, $"Expected 2 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day24-test.txt";

        //When
        var result = Day24.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("47" == result.Result, $"Expected 47 but was {result.Result}");
    }
}