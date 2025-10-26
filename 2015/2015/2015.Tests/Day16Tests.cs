namespace AoC2015.Tests;

public class Day16Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day16-test.txt";

        //When
        var result = Day16.ParseInput(filename);

        //Then
        Assert.True(5 == result.Count, $"Exptected 5 but was {result.Count}");
        Assert.True(result[0].Number == 1, $"Expected Aunt 1 but was {result[0].Number}");
        Assert.True(result[0].Properties["cars"] == 9, $"Expected 9 children but was {result[0].Properties["cars"]}");

        Assert.True(result[4].Number == 5, $"Expected Aunt 5 but was {result[4].Number}");
        Assert.True(result[4].Properties["vizslas"] == 7, $"Expected 7 viszlas but was {result[4].Properties["vizslas"]}");

    }
}