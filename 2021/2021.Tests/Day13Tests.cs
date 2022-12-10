namespace Advent2021.Tests;
public class Day13Tests
{
    [Fact]
    public void Can_count_after_first_fold()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day13-test.txt";

        //When
        var (result, _) = Day13.CountDots(filename);

        //Then
        Assert.Equal(17, result);
    }

}

