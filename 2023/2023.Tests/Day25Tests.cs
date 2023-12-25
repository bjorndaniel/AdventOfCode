namespace AoC2023.Tests;
public class Day25Tests()
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day25-test.txt";

        //When
        var result = Day25.ParseInput(filename);

        //Then
        Assert.True(13 == result.Count, $"Expectedd 13 but was {result.Count}");
        Assert.True("jqt" == result.First().Key, $"Expectedd jqt but was {result.First().Key}");
        Assert.True(3 == result.First().Value.Count, $"Expectedd 3 but was {result.First().Value.Count}");
        Assert.True("rhnxhknvd" == result.First().Value.Aggregate((a, b) => a + b), 
            $"Expectedd rhnxhknvd but was {result.First().Value.Aggregate((a, b) => a + b)}");
    }
}