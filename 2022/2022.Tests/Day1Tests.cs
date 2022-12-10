namespace AoC2022.Tests;
public class Day1Tests
{

    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day1-test.txt";

        //When
        var result = Day1.ParseInput(filename);

        //Then
        Assert.Equal(5, result.Count());

    }

    [Fact]
    public void Can_solve_test_elves_part1()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day1-test.txt";

        //When
        var result = Day1.SolvePart1(filename);

        //Then
        Assert.Equal(24000, result);
    }

    [Fact]
    public void Can_solve_test_elves_part2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day1-test.txt";

        //When
        var result = Day1.SolvePart2(filename);

        //Then
        Assert.Equal(45000, result);
    }
}
