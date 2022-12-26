namespace AoC2022.Tests;
public class Day18Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.ParseInput(filename);

        //Then
        Assert.True(13 == result.Count(), $"Expected 13 cubes, got {result.Count()}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";

        //When
        var result = Day18.SolvePart1(filename);

        //Then
        Assert.True(64 == result.result, $"Expected 64 cubes, got {result.result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day18-test.txt";
        //var filename = "C:/OneDrive/Code/AdventOfCodeInputs/2022/Puzzles/Day18.txt";

        //When
        var result = Day18.SolvePart2(filename);

        //Then
        Assert.True(58 == result, $"Expected 58 cubes, got {result}");
    }
}

