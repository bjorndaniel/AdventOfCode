namespace AoC2022.Tests;
public class Day19Tests
{
    [Fact]
    public void Can_parse_input()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.ParseInput(filename);

        //Then
        Assert.True(2 == result.Count(), $"Expected 2 blueprints, got {result.Count()}");
        Assert.True("Blueprint 1" == result.First().Name, $"Expected Blueprint 1, got {result.First().Name}");
        Assert.True(4 == result.First().OreRobotCost.Ore, $"Expected 4 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().OreRobotCost.Clay, $"Expected 0 ore, got {result.First().OreRobotCost.Clay}");
        Assert.True(2 == result.First().ClayRobotCost.Ore, $"Expected 2 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().ClayRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(0 == result.First().GeodeRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(2 == result.First().GeodeRobotCost.Ore, $"Expected 2 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().GeodeRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(7 == result.First().GeodeRobotCost.Obsidian, $"Expected 7 obsidian, got {result.First().OreRobotCost.Obsidian}");
        Assert.True("Blueprint 2" == result.Last().Name, $"Expected Blueprint 2, got {result.Last().Name}");
        Assert.True(12 == result.Last().GeodeRobotCost.Obsidian, $"Expected 12 obsidian, got {result.Last().OreRobotCost.Obsidian}");


    }
}
