namespace AoC2025.Tests;

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
        Assert.True(10 == result.GetLength(0), $"Expected 10 but was {result.GetLength(0)}");
        Assert.True(10 == result.GetLength(1), $"Expected 10 but was {result.GetLength(1)}");
        Assert.True('.' == result[0, 0], $"Expected . but was {result[0, 0]}");
        Assert.True('@' == result[0, 9], $"Expected @ but was {result[0, 0]}");
        Assert.True('.' == result[5, 2], $"Expected @ but was {result[0, 0]}");
        Assert.True('.' == result[2, 4], $"Expected @ but was {result[0, 0]}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("13" == result.Result, $"Expected 13 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("43" == result.Result, $"Expected 43 but was {result.Result}");
    }

}