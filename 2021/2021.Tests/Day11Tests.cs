namespace Advent2021.Tests;
public class Day11Tests
{
    [Fact]
    public void Can_calculate_flashes()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day11-test.txt";

        //When
        var result = Day11.CalculateFlashes(filename);

        //Then
        Assert.Equal(1656, result);
    }

    [Fact]
    public void Can_calculate_synchronized()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day11-test.txt";

        //When
        var result = Day11.CalculateSynchronized(filename);

        //Then
        Assert.Equal(195, result);
    }
}

