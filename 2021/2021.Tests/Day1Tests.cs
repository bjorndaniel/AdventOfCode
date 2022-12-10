namespace Advent2021.Tests;
public class Day1Tests
{
    [Fact]
    public void TestInput()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day1-test.json";

        //When
        var result = Day1.CountIncreases(filename);

        //Then
        Assert.Equal(7, result);
    }

    [Fact]
    public void TestInputGroups()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day1-test.json";

        //When
        var result = Day1.CountIncreasesBy3(filename);

        //Then
        Assert.Equal(5, result);
    }

}
