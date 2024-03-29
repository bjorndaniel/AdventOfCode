﻿namespace AoC2022.Tests;
public class Day17Tests
{

    private readonly ITestOutputHelper _output;

    public Day17Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.ParseInput(filename);

        //Then
        Assert.True(40 == result.Count(), $"Expected 40, got {result.Count()}");
        Assert.True(21 == result.Count(_ => _ == Day17.Direction.Right), $"Expected 21, got {result.Count(_ => _ == Day17.Direction.Right)}");
        Assert.True(19 == result.Count(_ => _ == Day17.Direction.Left), $"Expected 19, got {result.Count(_ => _ == Day17.Direction.Left)}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";
        var printer = new TestPrinter(_output);

        //When
        var result = Day17.Solve(filename, 2023, printer);

        //Then
        Assert.True(3068 == result, $"Expected 3068, got {result}");
        //result.Print(printer);
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";
        var printer = new TestPrinter(_output);

        //When
        var result = Day17.Solve(filename, 1000000000001, printer);

        //Then
        Assert.True(1514285714288 == result, $"Expected 1514285714288, got {result}");
        //result.Print(printer);
    }
}