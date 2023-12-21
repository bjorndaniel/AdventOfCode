namespace AoC2023.Tests;
public class Day21Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.ParseInput(filename);

        //Then
        //Assert.True(11 == result.plot.GetLength(0), $"Expected 11 but was {result.plot.GetLength(0)}");
        //Assert.True(11 == result.plot.GetLength(1), $"Expected 11 but was {result.plot.GetLength(1)}");
        //Assert.True('S' == result.plot[5,5], $"Expected 'S' but was {result.plot[5,5]}");
        //Assert.True((5,5) == result.start, $"Expected (5,5) but was {result.start}");
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("16" == result.Result, $"Expected 16 but was {result.Result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.Part2(filename, new TestPrinter(output));

        //Then
        Assert.True("1594" == result.Result, $"Expected 1594 but was {result.Result}");
    }

}