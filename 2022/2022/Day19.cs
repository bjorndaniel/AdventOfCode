namespace AoC2022;
public static class Day19
{
    public static IEnumerable<Blueprint> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var costs = l.Split(':')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
            var oreRobot = new Cost(int.Parse(costs[0].Replace("Each ore robot costs ", "").Replace(" ore", "").Trim()), 0, 0, RobotType.Ore);
            var clayRobot = new Cost(int.Parse(costs[1].Replace("Each clay robot costs ", "").Replace(" ore", "").Trim()), 0, 0, RobotType.Clay);
            var obsidianROre = int.Parse(costs[2].Split("ore and")[0].Replace("Each obsidian robot costs ", "").Trim());
            var obsidianRClay = int.Parse(costs[2].Split("ore and")[1].Replace(" clay", "").Trim());
            var obsidianRobot = new Cost(obsidianROre, obsidianRClay, 0, RobotType.Obsidian);
            var geodeOre = int.Parse(costs[3].Split("ore and")[0].Replace("Each geode robot costs ", "").Trim());
            var geodeObs = int.Parse(costs[3].Split("ore and")[1].Replace(" obsidian", "").Trim());
            var geodeRobot = new Cost(geodeOre, 0, geodeObs, RobotType.Geode);
            var name = l.Split(':').First();
            var id =  int.Parse(name.Split(' ').Last());
            yield return new Blueprint(name, id, oreRobot, clayRobot, obsidianRobot, geodeRobot);
        }
    }

    public static int SolvePart1(string filename)
    {
        //var maxGeodes = 0;
        var maxTime = 24;
        var blueprints = ParseInput(filename);
        foreach (var blueprint in blueprints)
        {
            DFS(blueprint, 1, 0, 0);
        }
        return blueprints.Select(_ => _.Id * _.MaxGeodes).Sum();

        void DFS(Blueprint blueprint, int time, int geodes, int skipCount)
        {
            if (time > maxTime)
            {
                return;
            }

        }

    }

    
}
public class Blueprint
{
    public Blueprint(string name, int id, Cost oreRobotCost, Cost clayRobotCost, Cost obsidianRobotCost, Cost geodeRobotCost)
    {
        Name = name;
        Id = id;
        OreRobotCost = oreRobotCost;
        ClayRobotCost = clayRobotCost;
        ObsidianRobotCost = obsidianRobotCost;
        GeodeRobotCost = geodeRobotCost;
    }

    public string Name { get; }
    public int Id { get; }
    public Cost OreRobotCost { get; }
    public Cost ClayRobotCost { get; }
    public Cost ObsidianRobotCost { get; }
    public Cost GeodeRobotCost { get; }
    public int MaxGeodes { get; set; }

}

public record Cost(int Ore, int Clay, int Obsidian, RobotType Type);
public enum RobotType
{
    Ore,
    Clay,
    Obsidian,
    Geode
}
