namespace AoC2015.Tests;
public class Day11Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day11-test.txt";

        //When
        var result = Day11.ParseInput(filename);

        //Then
        Assert.True(result == "hijklmmn", $"Expected hijklmmn but was {result}");
    }
}