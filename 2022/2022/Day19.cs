namespace AoC2022;
public static class Day19
{
    public static IEnumerable<Blueprint> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var costs = l.Split(':')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
            var oreRobot = new Cost(int.Parse(costs[0].Replace("Each ore robot costs ", "").Replace(" ore", "").Trim()), 0, 0);
            var clayRobot = new Cost(int.Parse(costs[1].Replace("Each clay robot costs ", "").Replace(" ore", "").Trim()), 0, 0);
            var obsidianROre = int.Parse(costs[2].Split("ore and")[0].Replace("Each obsidian robot costs ", "").Trim());
            var obsidianRClay = int.Parse(costs[2].Split("ore and")[1].Replace(" clay", "").Trim());
            var obsidianRobot = new Cost(oreRobot.Ore, oreRobot.Clay, 0);
            var geodeOre = int.Parse(costs[3].Split("ore and")[0].Replace("Each geode robot costs ", "").Trim());
            var geodeObs = int.Parse(costs[3].Split("ore and")[1].Replace(" obsidian", "").Trim());
            var geodeRobot = new Cost(geodeOre, 0, geodeObs);
            var name = l.Split(':')[0];
            yield return new Blueprint(name, oreRobot, clayRobot, obsidianRobot, geodeRobot);
        }
    }
}
public class Blueprint
{
    public Blueprint(string name, Cost oreRobotCost, Cost clayRobotCost, Cost obsidianRobotCost, Cost geodeRobotCost)
    {
        Name = name;
        OreRobotCost = oreRobotCost;
        ClayRobotCost = clayRobotCost;
        ObsidianRobotCost = obsidianRobotCost;
        GeodeRobotCost = geodeRobotCost;
    }

    public string Name { get; }
    public Cost OreRobotCost { get; }
    public Cost ClayRobotCost { get; }
    public Cost ObsidianRobotCost { get; }
    public Cost GeodeRobotCost { get; }
}

public record Cost(int Ore, int Clay, int Obsidian);