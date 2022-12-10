namespace Advent2021.Tests;
public class Day8Tests
{
    [Fact]
    public void Can_get_number_of_occurrences()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day8-test.txt";

        //When
        var result = Day8.CountDigits(filename);

        Assert.Equal(26, result);

    }

    [Fact]
    public void Can_get_total_sum()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day8-test.txt";

        //When
        var result = Day8.GetTotal(filename);

        Assert.Equal(61229, result);

    }

    [Fact]
    public void Can_get_digit_patterns()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day8-test2.txt";

        //When
        var result = Day8.GetAllDigitPatterns(filename);

        //Then
        Assert.Equal(1, result[(0, "ab")]);
        Assert.Equal(4, result[(0, "abef")]);
        Assert.Equal(7, result[(0, "abd")]);
        Assert.Equal(8, result[(0, "abcdefg")]);
        Assert.Equal(9, result[(0, "abcdef")]);
        Assert.Equal(0, result[(0, "abcdeg")]);
        Assert.Equal(6, result[(0, "bcdefg")]);
        Assert.Equal(5, result[(0, "bcdef")]);
        Assert.Equal(2, result[(0, "acdfg")]);
        Assert.Equal(3, result[(0, "abcdf")]);

        Assert.Equal(4, result[(1, "cefg")]);
        Assert.Equal(8, result[(1, "abcdefg")]);
        Assert.Equal(7, result[(1, "bfg")]);
        Assert.Equal(3, result[(1, "abefg")]);

        Assert.Equal(8, result[(2, "abcdefg")]);
        Assert.Equal(3, result[(2, "bcdef")]);
        Assert.Equal(9, result[(2, "bcdefg")]);
        Assert.Equal(4, result[(2, "bceg")]);
    }
}
