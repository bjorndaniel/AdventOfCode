namespace AoC2022;
public static class Day16
{
    public static IEnumerable<Valve> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Valve>();
        foreach (var l in lines)
        {
            var name = l
                .Replace("Valve ", "")
                .Split(' ')[0];
            var flowRate = l[(l.IndexOf('=') + 1)..l.IndexOf(';')];
            var valves = l[(l.IndexOf("valves ") + 7)..]
                .Split(',')
                .Select(_ => _.Trim());
            var valve = new Valve(
                name, int.Parse(flowRate),
                valves.Select(_ => new Valve(_, 0, new List<Valve>())).ToList()
            );
            result.Add(valve);
            //Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
        }
        foreach (var v in result)
        {
            var adjacent = v.Adjacent
                .Select(_ => _.Name)
                .ToList();
            var correct = result
                .Where(_ => adjacent.Contains(_.Name))
                .ToList();
            v.Adjacent = correct;
            yield return v;
        }
    }

    public static int SolvePart1(string filename)
    {
        var valves = ParseInput(filename);
        var result = 0;

        return result;
    }
}

public class Valve
{
    public Valve(string name, int flowRate, List<Valve> adjacent)
    {
        Name = name;
        FlowRate = flowRate;
        Adjacent = adjacent;
    }

    public string Name { get; }
    public int FlowRate { get; }
    public List<Valve> Adjacent { get; set; }
}
