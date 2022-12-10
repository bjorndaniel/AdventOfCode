namespace Advent2021.Tests;
public class Day14Tests
{
    [Fact]
    public void Can_calculate_elements()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day14-test.txt";

        //When
        var result = Day14.CalculateElements(filename);

        //Then
        Assert.Equal(1588, result);
    }

    [Fact]
    public void Can_calculate_elements_40()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day14-test.txt";

        //When
        var result = Day14.CalculateElements(filename, 40);

        //Then
        Assert.Equal(2188189693529, result);
    }
}

