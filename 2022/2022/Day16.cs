namespace AoC2022;
public static class Day16
{
    public static Dictionary<string, Valve> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new Dictionary<string, Valve>();
        foreach (var l in lines)
        {
            var name = l
                .Replace("Valve ", string.Empty)
                .Split(' ')[0];
            var flowRate = l[(l.IndexOf('=') + 1)..l.IndexOf(';')];
            var valves = new List<string>();
            if (l.Contains("lead to"))
            {
                valves = l[(l.IndexOf("valves ") + 7)..]
                    .Split(',')
                    .Select(_ => _.Trim()).ToList();
            }
            else
            {
                valves = l[(l.LastIndexOf("to valve ") + 9)..]
                .Split(',')
                .Select(_ => _.Trim()).ToList();

            }
            var valve = new Valve(
                name, int.Parse(flowRate),
                valves.Select(_ => new Valve(_, 0, new List<Valve>())).ToList()
            );
            result.Add(valve.Name, valve);
            //Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
        }
        foreach (var v in result)
        {
            var adjacent = v.Value.Adjacent
                .Select(_ => _.Name)
                .ToList();
            var correct = result
                .Where(_ => adjacent.Contains(_.Key))
                .ToList();
            v.Value.Adjacent = correct.Select(_ => _.Value).ToList();
        }
        return result;
    }

    public static int SolvePart1(string filename, IPrinter printer)
    {
        var valves = ParseInput(filename);
        var distances = FloydWarshall(valves);
        var result = DFS("AA", 30, new Path(Array.Empty<Valve>(), 0), new Dictionary<string, bool>(), distances, valves);
        return result.Max(_ => _.FlowRate);
    }

    public static int SolvePart2(string filename, IPrinter printer)
    {
        var valves = ParseInput(filename);
        var distances = FloydWarshall(valves);
        var result = DFS("AA", 26, new Path(Array.Empty<Valve>(), 0), new Dictionary<string, bool>(), distances, valves);
        var max = 0;
        foreach (var p in result)
        {
            if (!p.Nodes.Any())
            {
                continue;
            }
            var visit = p.Nodes.Select(_ => new KeyValuePair<string, bool>(_.Name, true));
            foreach (var p1 in result)
            {
                var flow = p.FlowRate + p1.FlowRate;
                if (flow > max && p1.Nodes.Any() && p1.Nodes.All(_ => !visit.Contains(new KeyValuePair<string, bool>(_.Name, true))))
                {
                    max = flow;
                }
            }
        }
        return max;
    }

    static Dictionary<string, Dictionary<string, int>> FloydWarshall(Dictionary<string, Valve> valves)
    {
        var dist = new Dictionary<string, Dictionary<string, int>>();
        foreach (var i in valves.Keys)
        {
            foreach (var j in valves.Keys)
            {
                if (!dist.ContainsKey(i))
                {
                    dist[i] = new Dictionary<string, int>();
                }
                if (i == j)
                {
                    dist[i][j] = 0;
                }
                else if (valves[i].Adjacent.Any(a => a.Name == j))
                {
                    dist[i][j] = 1;
                }
                else
                {
                    dist[i][j] = 999999;
                }
            }
        }

        foreach (var k in valves.Keys)
        {
            foreach (var i in valves.Keys)
            {
                foreach (var j in valves.Keys)
                {
                    dist[i][j] = Math.Min(dist[i][j], dist[i][k] + dist[k][j]);
                }
            }
        }

        return dist;
    }

    static Path[] DFS(string current, int time, Path path,
    Dictionary<string, bool> visited, Dictionary<string, Dictionary<string, int>> distances, Dictionary<string, Valve> valves)
    {
        var paths = new[] { path };
        var nonZero = valves.Where(_ => _.Value.FlowRate > 0).Select(_ => _.Key);
        foreach (var next in nonZero)
        {
            var newTime = time - distances[current][next] - 1;
            if (visited.ContainsKey(next) || newTime <= 0)
            {
                continue;
            }
            var newMap = new Dictionary<string, bool>(visited)
            {
                { next, true }
            };
            var newPath = path.Copy();
            newPath.AddToPath(newTime * valves[next].FlowRate, valves[next]);
            paths = paths.Concat(DFS(next, newTime, newPath, newMap, distances, valves)).ToArray();
        }
        return paths;
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
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        return ((Valve)obj)?.Name == Name;
    }
    public override int GetHashCode() =>
        base.GetHashCode();
}

public class Path
{
    public Path(Valve[] nodes, int flow)
    {
        Nodes = nodes;
        FlowRate = flow;
    }

    public Valve[] Nodes { get; set; }

    public int FlowRate { get; set; }

    public Path Copy() =>
        new Path(Nodes.ToArray(), FlowRate);

    public void AddToPath(int flow, Valve node)
    {
        FlowRate += flow;
        Nodes = Nodes.Append(node).ToArray();
    }
}