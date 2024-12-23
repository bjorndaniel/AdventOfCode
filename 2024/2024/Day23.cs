namespace AoC2024;
public class Day23
{
    public static List<Connection> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Connection>();
        foreach (var line in lines)
        {
            var parts = line.Split("-");
            result.Add(new Connection(parts[0], parts[1]));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day23.txt", "Day 23 part 1", 23)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var connections = ParseInput(filename);
        var graph = BuildGraph(connections);
        var groups = FindAllConnections(graph);

        var resultGroups = new List<List<string>>();
        foreach (var group in groups)
        {
            if (group.Count >= 3)
            {
                for (int i = 0; i < group.Count; i++)
                {
                    for (int j = i + 1; j < group.Count; j++)
                    {
                        for (int k = j + 1; k < group.Count; k++)
                        {
                            if (graph[group[i]].Contains(group[j]) && graph[group[j]].Contains(group[k]) && graph[group[k]].Contains(group[i]))
                            {
                                resultGroups.Add(new List<string> { group[i], group[j], group[k] });
                            }
                        }
                    }
                }
            }
        }

        return new SolutionResult(resultGroups.Count(g => g.Any(c => c.StartsWith("t"))).ToString());
    }

    [Solveable("2024/Puzzles/Day23.txt", "Day 23 part 2", 23)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var connections = ParseInput(filename);
        var graph = BuildGraph(connections);
        var largestComponent = FindLargestConnectedGroup(graph);
        largestComponent.Sort();
        return new SolutionResult(largestComponent.Aggregate((a,b) => $"{a},{b}"));
    }

    private static List<string> FindLargestConnectedGroup(Dictionary<string, List<string>> graph)
    {
        var largestGroup = new List<string>();
        var nodes = graph.Keys.ToList();

        for (int i = 0; i < nodes.Count; i++)
        {
            var currentGroup = new List<string> { nodes[i] };
            for (int j = 0; j < nodes.Count; j++)
            {
                if (i != j && currentGroup.All(node => graph[node].Contains(nodes[j])))
                {
                    currentGroup.Add(nodes[j]);
                }
            }
            if (currentGroup.Count > largestGroup.Count)
            {
                largestGroup = currentGroup;
            }
        }

        return largestGroup;
    }

    private static Dictionary<string, List<string>> BuildGraph(List<Connection> connections)
    {
        var graph = new Dictionary<string, List<string>>();
        foreach (var connection in connections)
        {
            if (graph.ContainsKey(connection.First) is false)
            {
                graph[connection.First] = [];
            }
            if (graph.ContainsKey(connection.Second) is false)
            {
                graph[connection.Second] = [];
            }
            graph[connection.First].Add(connection.Second);
            graph[connection.Second].Add(connection.First);
        }

        return graph;
    }

    private static List<List<string>> FindAllConnections(Dictionary<string, List<string>> graph)
    {
        var groups = new List<List<string>>();
        var visited = new HashSet<string>();
        foreach (var node in graph.Keys)
        {
            if (visited.Contains(node) is false)
            {
                var group = new List<string>();
                DFS(node, graph, visited, group);
                if (group.Count >= 3)
                {
                    groups.Add(group);
                }
            }
        }
        return groups;
    }

    private static void DFS(string node, Dictionary<string, List<string>> graph, HashSet<string> visited, List<string> group)
    {
        visited.Add(node);
        group.Add(node);
        foreach (var neighbor in graph[node])
        {
            if (visited.Contains(neighbor) is false)
            {
                DFS(neighbor, graph, visited, group);
            }
        }
    }
}

public record Connection(string First, string Second);