namespace AoC2015.Tests;

public class Day21Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day21-test.txt";

        //When
        var result = Day21.ParseInput(filename);

        //Then
        Assert.True(12 == result.HitPoints, $"Expected 12 but was {result.HitPoints}");
        Assert.True(7 == result.Damage, $"Expected 7 but was {result.Damage}");
        Assert.True(2 == result.Armor, $"Expected 2 but was {result.Armor}");
    }
}