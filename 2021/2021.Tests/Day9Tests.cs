namespace Advent2021.Tests;
public class Day9Tests
{
    [Fact]
    public void Can_calculate_low_point()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day9-test.txt";

        //When
        var result = Day9.CalculateLowpoint(filename);

        //Then
        Assert.Equal(15, result);
    }

    [Fact]
    public void Can_calculate_largest_basins()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day9-test.txt";

        //When
        var result = Day9.CalculateLargestBasins(filename);

        //Then
        Assert.Equal(1134, result);
    }
}

