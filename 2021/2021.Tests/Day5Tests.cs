namespace Advent2021.Tests;
public class Day5Tests
{
    [Fact]
    public void Can_get_overlapping_lines()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day5-test.txt";

        //When
        var result = Day5.GetOverlappingPoints(filename);

        //Then
        Assert.Equal(5, result);
    }

    [Fact]
    public void Can_get_overlapping_lines_with_diagonal()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day5-test.txt";

        //When
        var result = Day5.GetOverlappingPoints(filename, true);

        //Then
        Assert.Equal(12, result);
    }
}
