using static Advent2021.Day6;

namespace Advent2021.Tests;
public class Day6Tests
{
    [Fact]
    public void Can_count_lanternfish()
    {
        //Given
        var filename = $"{Helpers.DirectoryPath}Day6-test.txt";

        //When
        var result = Day6.CountLanternfish(filename, 18);
        var result1 = Day6.CountLanternfish(filename, 80);
        //var result2 = Day6.CountLanternfish(filename, 256);

        //Then
        Assert.Equal(26, result);
        Assert.Equal(5934, result1);
        //Assert.Equal(26984457539, result1);
    }
}
