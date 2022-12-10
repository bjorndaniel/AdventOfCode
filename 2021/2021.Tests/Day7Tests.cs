namespace Advent2021.Tests;
public class Day7Tests
{
	[Fact]
	public void Can_calculate_least_fuel_consumption()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day7-test.txt";

        //When
        var result = Day7.CalculateFuelConsumption(filename);

        //Then
        Assert.Equal(37, result);

    }


    [Fact]
    public void Can_calculate_least_fuel_consumption_2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day7-test.txt";

        //When
        var result = Day7.CalculateFuelConsumption2(filename);

        //Then
        Assert.Equal(168, result);

    }
}

