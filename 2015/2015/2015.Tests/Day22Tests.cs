namespace AoC2015.Tests;

public class Day22Tests(ITestOutputHelper output)
{

    [Fact]
    public void Can_parse_input()
    {
        //Given 
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.ParseInput(filename);

        //Then
        Assert.True(13 == result.HitPoints, $"Expected 13 but was {result.HitPoints}");
        Assert.True(8 == result.Damage, $"Expected 8 but was {result.Damage}");
        Assert.True(0 == result.Armor, $"Expected 0 but was {result.Armor}");
    }


    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day22-test.txt";

        //When
        var result = Day22.Part1(filename, new TestPrinter(output));

        //Then
        Assert.True("226".Equals(result.Result), $"Expected 24 but was {result.Result}");
    }
}