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

    public static int SolvePart1(string filename, IPrinter printer)
    {
        var valves = ParseInput(filename);
        var (mp, p) = GetMaxPressure(valves.First(_ => _.Name == "AA"), 30, 0, 0, new HashSet<Valve>());
        printer.Print(p.Select(_ => _.Name).Aggregate((a, b) => a + " -> " + b));
        printer.Flush();
        return mp;
    }
    public static (int max, List<Valve> path) GetMaxPressure(Valve start, int minutes, int currentTime, int currentPressure, HashSet<Valve> visited)
    {
        // Check if the current time exceeds the number of minutes allowed for traversing the valves
        if (currentTime >= minutes)
        {
            return (currentPressure, visited.ToList());
        }

        // Initialize variables to store the maximum pressure and path
        int maxPressure = currentPressure;
        List<Valve> maxPath = visited.ToList();
        // Visit the adjacent valves of the current valve
        foreach (var adjacent in start.Adjacent.OrderByDescending(v => v.FlowRate).ToList())
        {
            if (!visited.Contains(adjacent))
            {
                var pressureReleased = (minutes - currentTime - 2) * adjacent.FlowRate;
                // Add the pressure released by the adjacent valve to the current pressure
                int newPressure = currentPressure + pressureReleased;
                visited.Add(adjacent);
                // Recursively call the GetMaxPressure method to find the maximum pressure and path for the remaining minutes
                (int pressure, List<Valve> path) = GetMaxPressure(adjacent, minutes, currentTime + 2, newPressure, visited);
                // Update the maximum pressure and path if the pressure achieved by the recursive call is higher
                if (pressure >= maxPressure)
                {
                    maxPressure = pressure;
                    maxPath = path;
                    // Remove the adjacent valve from the set of visited valves
                }
                visited.Remove(adjacent);
            }
        }

        // Return the maximum pressure and path
        return (maxPressure, maxPath);
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
    public override bool Equals(object obj)
    {
        var other = (Valve)obj;
        return other?.Name == Name;
    }
}
