namespace AoC2022;
public static class Day19
{
    public static IEnumerable<Blueprint> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var (maxOre, maxClay, maxObs) = (0, 0, 0);
        var result = new List<Blueprint>();
        foreach (var l in lines)
        {
            var costs = l.Split(':')[1].Split('.', StringSplitOptions.RemoveEmptyEntries);
            var oreRobot = new Robot(int.Parse(costs[0].Replace("Each ore robot costs ", "").Replace(" ore", "").Trim()), 0, 0, OreType.Ore);
            var clayRobot = new Robot(int.Parse(costs[1].Replace("Each clay robot costs ", "").Replace(" ore", "").Trim()), 0, 0, OreType.Clay);
            var obsidianROre = int.Parse(costs[2].Split("ore and")[0].Replace("Each obsidian robot costs ", "").Trim());
            var obsidianRClay = int.Parse(costs[2].Split("ore and")[1].Replace(" clay", "").Trim());
            var obsidianRobot = new Robot(obsidianROre, obsidianRClay, 0, OreType.Obsidian);
            var geodeOre = int.Parse(costs[3].Split("ore and")[0].Replace("Each geode robot costs ", "").Trim());
            var geodeObs = int.Parse(costs[3].Split("ore and")[1].Replace(" obsidian", "").Trim());
            var geodeRobot = new Robot(geodeOre, 0, geodeObs, OreType.Geode);
            var name = l.Split(':').First();
            var id = int.Parse(name.Split(' ').Last());
            result.Add(new Blueprint(name, id, oreRobot, clayRobot, obsidianRobot, geodeRobot));
            maxOre = Math.Max(Math.Max(Math.Max(Math.Max(maxOre, oreRobot.Ore), clayRobot.Ore), obsidianRobot.Ore), geodeRobot.Ore);
            maxClay = Math.Max(Math.Max(Math.Max(Math.Max(maxClay, oreRobot.Clay), clayRobot.Clay), obsidianRobot.Clay), geodeRobot.Clay);
            maxObs = Math.Max(Math.Max(Math.Max(Math.Max(maxObs, oreRobot.Obsidian), clayRobot.Obsidian), obsidianRobot.Obsidian), geodeRobot.Obsidian);
        }
        result.ForEach(_ =>
        {
            _.MaxSpend.Add(OreType.Ore, maxOre);
            _.MaxSpend.Add(OreType.Clay, maxClay);
            _.MaxSpend.Add(OreType.Obsidian, maxObs);
        });
        return result;
    }

    public static int SolvePart1(string filename)
    {
        //var maxGeodes = 0;
        var blueprints = ParseInput(filename);
        var total = 0;
        foreach (var blueprint in blueprints)
        {
            var amount = DFS(blueprint, new(), 24, new List<Robot> { blueprint.OreRobotCost }, new Dictionary<OreType, int> { { OreType.Ore, 0 }, { OreType.Clay, 0 }, { OreType.Obsidian, 0 }, { OreType.Geode, 0 } });
            total += blueprint.Id * amount;

        }
        return total;

    }

    public static long SolvePart2(string filename)
    {
        //var maxGeodes = 0;
        var blueprints = ParseInput(filename).Take(3);
        var total = 1L;
        foreach (var blueprint in blueprints)
        {
            var amount = DFS(blueprint, new(), 32, new List<Robot> { blueprint.OreRobotCost }, new Dictionary<OreType, int> { { OreType.Ore, 0 }, { OreType.Clay, 0 }, { OreType.Obsidian, 0 }, { OreType.Geode, 0 } });
            total *= amount;

        }
        return total;
    }

    private static int DFS(Blueprint blueprint, Dictionary<string, int> state, int time, List<Robot> robots, Dictionary<OreType, int> resources)
    {
        if (time < 1)
        {
            return resources.ContainsKey(OreType.Geode) ? resources[OreType.Geode] : 0;
        }
        var key = GetKey(time, robots, resources);
        if (state.ContainsKey(key))
        {
            return state[key];
        }

        var maxGeodes = resources.ContainsKey(OreType.Geode) ? resources[OreType.Geode] + (robots.Count(_ => _.Type == OreType.Geode) * time) : 0;

        foreach (var robot in blueprint.Robots)
        {
            if (robot.Type != OreType.Geode && robots.Count(_ => _.Type == robot.Type) >= blueprint.GetMaxSpend(robot.Type))
            {
                continue;
            }
            var wait = 0;
            var noBreak = true;
            foreach (var resource in robot.Resources)
            {
                if (resource.amount == 0)
                {
                    continue;
                }

                if (robots.Count(_ => _.Type == resource.type) == 0)
                {
                    noBreak = false;
                    break;
                }
                wait = int.Max(wait, (int)Math.Ceiling((decimal)(resource.amount - (resources.ContainsKey(resource.type) ? resources[resource.type] : 0)) / robots.Count(_ => _.Type == resource.type)));
                //wait = int.Max(wait, resource.amount - (resources.ContainsKey(resource.type) ? resources[resource.type] : 0)) / robots.Count(_ => _.Type == resource.type);
            }
            if (noBreak)
            {
                var remTime = time - wait - 1;
                if (remTime <= 0)
                {
                    continue;
                }
                else
                {
                    var bots = robots.ToList();
                    var amount = resources.Select(_ => (_.Key, (wait + 1) * robots.Count(r => r.Type == _.Key) + _.Value)).ToDictionary(_ => _.Key, _ => _.Item2);
                    foreach (var resource in resources)
                    {
                        var res = robot.Resources.FirstOrDefault(_ => _.type == resource.Key);
                        amount[resource.Key] -= res != default ? res.amount : 0;
                    }
                    bots.Add(new Robot(robot.Ore, robot.Clay, robot.Obsidian, robot.Type));
                    foreach (var type in Enum.GetValues(typeof(OreType)))
                    {
                        var resource = (OreType)type;
                        if (resource != OreType.Geode)
                        {
                            amount[resource] = int.Min(amount[resource], blueprint.MaxSpend[resource] * remTime);
                        }
                    }
                    maxGeodes = int.Max(maxGeodes, DFS(blueprint, state, remTime, bots, amount));
                }

            }
        }


        state[key] = maxGeodes;
        return maxGeodes;

        string GetKey(int time, List<Robot> robots, Dictionary<OreType, int> resources)
        {
            var oreRobots = robots.Count(_ => _.Type == OreType.Ore);
            var clayRobots = robots.Count(_ => _.Type == OreType.Clay);
            var obsRobots = robots.Count(_ => _.Type == OreType.Obsidian);
            var geodeRobots = robots.Count(_ => _.Type == OreType.Geode);
            var ore = resources.ContainsKey(OreType.Ore) ? resources[OreType.Ore] : 0;
            var clay = resources.ContainsKey(OreType.Clay) ? resources[OreType.Clay] : 0;
            var obs = resources.ContainsKey(OreType.Obsidian) ? resources[OreType.Obsidian] : 0;
            var geode = resources.ContainsKey(OreType.Geode) ? resources[OreType.Geode] : 0;
            return $"{time}-{oreRobots}-{clayRobots}-{obsRobots}-{geodeRobots}-{ore}-{clay}-{obs}-{geode}";
        }

    }
}
public class Blueprint
{
    public Blueprint(string name, int id, Robot oreRobotCost, Robot clayRobotCost, Robot obsidianRobotCost, Robot geodeRobotCost)
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
    public Robot OreRobotCost { get; }
    public Robot ClayRobotCost { get; }
    public Robot ObsidianRobotCost { get; }
    public Robot GeodeRobotCost { get; }
    public Dictionary<OreType, int> MaxSpend { get; } = new();
    public int MaxGeodes { get; set; }
    public IEnumerable<Robot> Robots => new List<Robot> { OreRobotCost, ClayRobotCost, ObsidianRobotCost, GeodeRobotCost };

    public int GetMaxSpend(OreType ore)
    {
        if (ore == OreType.Geode)
        {
            return int.MaxValue;
        }
        return MaxSpend[ore];
    }
}

public record Robot(int Ore, int Clay, int Obsidian, OreType Type)
{
    public List<(OreType type, int amount)> Resources => new() { (OreType.Ore, Ore), (OreType.Clay, Clay), (OreType.Obsidian, Obsidian) };
}

public enum OreType
{
    Ore,
    Clay,
    Obsidian,
    Geode
}
