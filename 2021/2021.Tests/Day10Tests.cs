namespace Advent2021.Tests;
public class Day10Tests
{
    [Fact]
    public void Can_calculate_errors()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day10-test.txt";

        //When
        var result = Day10.CalculateErrors(filename);

        //Then
        Assert.Equal(26397, result);
    }

    [Fact]
    public void Can_calculate_completions()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day10-test.txt";

        //When
        var result = Day10.CalculateCompletions(filename);

        //Then
        Assert.Equal(288957, result);
    }
}

