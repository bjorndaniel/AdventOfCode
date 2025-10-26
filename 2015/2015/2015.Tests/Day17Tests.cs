namespace AoC2015.Tests;

public class Day17Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day17-test.txt";

        //When
        var result = Day17.ParseInput(filename);

        //Then
        Assert.True(4 == result.Count, $"Expected 4 but was {result.Count}");
        Assert.True(11 == result[0].Liters, $"Expected 11 but was {result[0].Liters}");
        Assert.True(31 == result[3].Liters, $"Expected 31 but was {result[3].Liters}");
    }
}