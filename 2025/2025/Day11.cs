namespace AoC2025;

public class Day11
{
    public static List<Device> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Device>();
        foreach (var line in lines)
        {
            var deviceName = line.Split(':')[0];
            var outputs = line.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            result.Add(new Device(deviceName, outputs.ToList()));
        }

        return result;
    }

    [Solveable("2025/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var devices = ParseInput(filename);
        var graph = BuildGraph(devices);
        
        // Count paths from "you" to "out" (no required nodes)
        var requiredNodes = new HashSet<string>();
        var count = CountPathsWithRequired(graph, "you", "out", requiredNodes);
        
        return new SolutionResult(count.ToString());
    }

    [Solveable("2025/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var devices = ParseInput(filename);
        var graph = BuildGraph(devices);
        
        // Count paths from "svr" to "out" that visit both "dac" and "fft"
        var requiredNodes = new HashSet<string> { "dac", "fft" };
        var count = CountPathsWithRequired(graph, "svr", "out", requiredNodes);

        return new SolutionResult(count.ToString());
    }

    private static long CountPathsWithRequired(
        Dictionary<string, List<string>> graph,
        string start,
        string target,
        HashSet<string> requiredNodes)
    {
        // State: (current node, set of required nodes visited so far)
        // We use a bitmask to represent which required nodes have been visited
        var requiredList = requiredNodes.ToList();
        var requiredIndex = requiredList.Select((node, idx) => (node, idx)).ToDictionary(x => x.node, x => x.idx);
        var requiredCount = requiredList.Count;
        var fullMask = (1 << requiredCount) - 1; // All required nodes visited

        // Memoization: (node, visitedMask) -> count of paths
        var memo = new Dictionary<(string, int), long>();

        return CountPaths(start, 0, new HashSet<string>());

        long CountPaths(string current, int visitedMask, HashSet<string> pathNodes)
        {
            // If we reached the target
            if (current == target)
            {
                // Check if we've visited all required nodes
                return visitedMask == fullMask ? 1 : 0;
            }

            // Check memoization (only safe if we're not in a cycle context)
            var key = (current, visitedMask);
            if (memo.ContainsKey(key) && !pathNodes.Contains(current))
            {
                return memo[key];
            }

            // If current node has no outgoing edges, return 0
            if (!graph.ContainsKey(current))
            {
                return 0;
            }

            // Update visited mask if current is a required node
            var newMask = visitedMask;
            if (requiredIndex.ContainsKey(current))
            {
                newMask |= (1 << requiredIndex[current]);
            }

            // Avoid cycles
            if (pathNodes.Contains(current))
            {
                return 0;
            }

            pathNodes.Add(current);
            long totalPaths = 0;

            // Explore all neighbors
            foreach (var neighbor in graph[current])
            {
                totalPaths += CountPaths(neighbor, newMask, pathNodes);
            }

            pathNodes.Remove(current);

            // Memoize (only if not in current path to avoid cycle issues)
            if (!pathNodes.Contains(current))
            {
                memo[key] = totalPaths;
            }

            return totalPaths;
        }
    }

    private static Dictionary<string, List<string>> BuildGraph(List<Device> devices)
    {
        var graph = new Dictionary<string, List<string>>();
        foreach (var device in devices)
        {
            if (!graph.ContainsKey(device.Name))
            {
                graph[device.Name] = new List<string>();
            }
            graph[device.Name].AddRange(device.Outputs);
        }
        return graph;
    }
}

public record Device(string Name, List<string> Outputs);