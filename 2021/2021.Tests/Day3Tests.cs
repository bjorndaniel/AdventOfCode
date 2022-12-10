namespace Advent2021.Tests;
public class Day3Tests
{
    [Fact]
    public void Can_calculate_power_consumtption()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day3-test.txt";

        //When
        var result = Day3.CalculatePowerConsumption(filename);

        //Then
        Assert.Equal(198, result);
    }

    [Fact]
    public void Can_calculate_life_support_rating()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}day3-test.txt";

        //When
        var result = Day3.CalculateLifeSupportRating(filename);

        //Then
        Assert.Equal(230, result);
    }
}
