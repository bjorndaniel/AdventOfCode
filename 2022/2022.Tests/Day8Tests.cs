namespace AoC2022.Tests;
public class Day8Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.ParseInput(filename);

        //Then
        Assert.NotNull(result);
        Assert.Equal(5, result.GetLength(0));
        Assert.Equal(5, result.GetLength(1));
        Assert.Equal(3, result[0, 0]);
        Assert.Equal(3, result[2, 2]);
    }

    [Fact]
    public void Can_solve_part1_for_test()
    {
        {
            //Given
            var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

            //When
            var result = Day8.SolvePart1(filename);

            //Then
            Assert.Equal(21, result);
        }
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day8-test.txt";

        //When
        var result = Day8.SolvePart2(filename);

        //Then
        Assert.Equal(8, result);
    }
}
