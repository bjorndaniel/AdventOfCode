namespace AoC2023.Tests;
public class Day11Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("374" == result.Result, $"Expected 374 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("8410" == result.Result, $"Expected 8410 but was {result.Result}");
    }

}