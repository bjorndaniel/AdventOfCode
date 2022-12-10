namespace Advent2021.Tests;
public class Day15Tests
{
    [Fact]
    public void Can_calculate_risk()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day15-test.txt";

        //When
        var result = Day15.CalculateRisk(filename);

        //Then
        Assert.Equal(40, result);
    }

    [Fact]
    public void Can_calculate_risk_part2()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day15-test.txt";

        //When
        var result = Day15.CalculateRisk2(filename);

        //Then
        Assert.Equal(315, result);
    }

    [Fact]
    public void Can_copy_matrix()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day15-test.txt";
        var matrix = Day15.GetMatrix(filename);

        //When
        var result = Day15.RepeatMatrix(matrix);

        //Then
        Assert.Equal(1, result[0, 0]);
        Assert.Equal(2, result[0, 9]);
        Assert.Equal(2, result[9, 0]);
        Assert.Equal(1, result[9, 9]);
        Assert.Equal(8, result[8, 9]);
        Assert.Equal(9, result[4, 3]);
        Assert.Equal(2, result[0, 10]);
        Assert.Equal(3, result[0, 21]);
        Assert.Equal(4, result[0, 31]);
        Assert.Equal(5, result[0, 41]);
        Assert.Equal(6, result[0, 49]);
        Assert.Equal(3, result[9, 10]);
        Assert.Equal(4, result[9, 21]);
        Assert.Equal(5, result[9, 31]);
        Assert.Equal(6, result[9, 41]);
        Assert.Equal(5, result[9, 49]);
        Assert.Equal(9, result[49, 49]);
        Assert.Equal(9, result[49, 48]);
        Assert.Equal(8, result[43, 46]);
    }
}

