namespace Advent2021.Tests;
public class Day2Tests
{
    [Fact]
    public void TestNavigate()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day2-test.txt";

        //When
        var result = Day2.Navigate(filename);

        //Then
        Assert.Equal(150, result);
    }

    [Fact]
    public void TestAim()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPath}day2-test.txt";

        //When
        var result = Day2.Aim(filename);

        //Then
        Assert.Equal(900, result);
    }
}
