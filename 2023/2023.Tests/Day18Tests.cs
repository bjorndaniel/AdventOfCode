namespace AoC2023.Tests;
public class Day18Tests(ITestOutputHelper output)
{
    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.ParseInput(filename);

        //Then
        //Assert.True(14 == result.Count, $"Expected 14 but was {result.Count}");
        //Assert.True(DigDirection.East == result[0].Direction, $"Expected East but was {result[0].Direction}");
        //Assert.True(6 == result[0].Meters, $"Expected 6 but was {result[0].Meters}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("62" == result.Result, $"Expected 62 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("952408144115" == result.Result, $"Expected 952408144115 but was {result.Result}");
    }

}