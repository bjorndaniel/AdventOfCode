namespace AoC2023.Tests;
public class Day23Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.ParseInput(filename);

        //Then
        Assert.True(23 == result.GetLength(0), $"Expected 22 but was {result.GetLength(0)}");
        Assert.True(23 == result.GetLength(1), $"Expected 22 but was {result.GetLength(1)}");
        Assert.True('.' == result[1,01], $"Expected . but was {result[1, 0]}");
        Assert.True('.' == result[result.GetLength(0) - 2,result.GetLength(1) -1], $"Expected . but was {result[result.GetLength(0) - 1, result.GetLength(1)-1]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("94" == result.Result, $"Expected 94 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day23-test.txt";

        //When
        var result = Day23.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("154" == result.Result, $"Expected 154 but was {result.Result}");
    }

}