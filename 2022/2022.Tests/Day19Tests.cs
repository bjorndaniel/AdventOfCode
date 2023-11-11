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
        Assert.True(1 == result.First().Id, $"Expected Blueprint 1, got {result.First().Id}");
        Assert.True(4 == result.First().OreRobotCost.Ore, $"Expected 4 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().OreRobotCost.Clay, $"Expected 0 ore, got {result.First().OreRobotCost.Clay}");
        Assert.True(2 == result.First().ClayRobotCost.Ore, $"Expected 2 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().ClayRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(3 == result.First().ObsidianRobotCost.Ore, $"Expected 3 ore, got {result.First().ObsidianRobotCost.Ore}");
        Assert.True(14 == result.First().ObsidianRobotCost.Clay, $"Expected 14 clay, got {result.First().ObsidianRobotCost.Clay}");
        Assert.True(0 == result.First().GeodeRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(2 == result.First().GeodeRobotCost.Ore, $"Expected 2 ore, got {result.First().OreRobotCost.Ore}");
        Assert.True(0 == result.First().GeodeRobotCost.Clay, $"Expected 0 clay, got {result.First().OreRobotCost.Clay}");
        Assert.True(7 == result.First().GeodeRobotCost.Obsidian, $"Expected 7 obsidian, got {result.First().OreRobotCost.Obsidian}");
        Assert.True("Blueprint 2" == result.Last().Name, $"Expected Blueprint 2, got {result.Last().Name}");
        Assert.True(2 == result.Last().Id, $"Expected Blueprint 2, got {result.Last().Id}");
        Assert.True(12 == result.Last().GeodeRobotCost.Obsidian, $"Expected 12 obsidian, got {result.Last().OreRobotCost.Obsidian}");
        Assert.True(3 == result.Last().GeodeRobotCost.Ore, $"Expected 3 obsidian, got {result.Last().OreRobotCost.Ore}");
        Assert.True(3 == result.Last().ObsidianRobotCost.Ore, $"Expected 3 ore, got {result.Last().ObsidianRobotCost.Ore}");
        Assert.True(8 == result.Last().ObsidianRobotCost.Clay, $"Expected 8 clay, got {result.Last().ObsidianRobotCost.Clay}");
        Assert.True(4 == result.First().GetMaxSpend(OreType.Ore), $"Expected 4 ore, got {result.First().GetMaxSpend(OreType.Ore)}");
        Assert.True(14 == result.First().GetMaxSpend(OreType.Clay), $"Expected 14 clay, got {result.First().GetMaxSpend(OreType.Clay)}");
        Assert.True(12 == result.First().GetMaxSpend(OreType.Obsidian), $"Expected 12 obsidian, got {result.First().GetMaxSpend(OreType.Obsidian)}");
        Assert.True(4 == result.Last().GetMaxSpend(OreType.Ore), $"Expected 4 ore, got {result.Last().GetMaxSpend(OreType.Ore)}");
        Assert.True(14 == result.Last().GetMaxSpend(OreType.Clay), $"Expected 14 clay, got {result.Last().GetMaxSpend(OreType.Clay)}");
        Assert.True(12 == result.Last().GetMaxSpend(OreType.Obsidian), $"Expected 12 obsidian, got {result.Last().GetMaxSpend(OreType.Obsidian)}");
    }   

    [Fact]
    public void Can_solve_part1_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.SolvePart1(filename);

        //Then
        Assert.True(33 == result, $"Expected 33, got {result}");
    }

    [Fact]
    public void Can_solve_part2_for_test()
    {
        //Given
        var filename = $"{Helpers.DirectoryPathTests}Day19-test.txt";

        //When
        var result = Day19.SolvePart2(filename);

        //Then
        Assert.True(62 == result, $"Expected 62, got {result}");
    }
}
