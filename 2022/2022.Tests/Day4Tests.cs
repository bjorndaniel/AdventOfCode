namespace AoC2022.Tests;
public class Day4Tests
{

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When
        var result = Day4.ParseInput(filename);

        //Then
        Assert.Equal(6, result.Count());
        Assert.Equal(2, result.First().First.Low);
        Assert.Equal(4, result.First().First.High);
        Assert.Equal(8, result.First().Second.High);
        Assert.Equal(8, result.Last().Second.High);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When 
        var result = Day4.SolvePart1(filename);

        //Then
        Assert.Equal(2, result);
    }


    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day4-test.txt";

        //When 
        var result = Day4.SolvePart2(filename);

        //Then
        Assert.Equal(4, result);
    }
}
