namespace AoC2018;
public class Day7
{
    public static List<(char step, char neighbor)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(char step, char neighbor)>();
        foreach(var line in lines) 
        {
            var step = line.Split(' ')[1][0];
            var neighbor = line.Split(' ')[7][0];
            result.Add((step, neighbor));
        }
        return result;
    }

    [Solveable("2018/Puzzles/Day7.txt", "Day 7 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var dependencies = ParseInput(filename);
        var sortedTasks = TopologicalSort(dependencies);


        return new SolutionResult(string.Join("", sortedTasks));
    }

    [Solveable("2018/Puzzles/Day7.txt", "Day 7 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    static List<char> TopologicalSort(List<(char step, char neighbor)> dependencies)
    {
        var nodes = new HashSet<char>();
        var edges = new Dictionary<char, List<char>>();
        var incomingEdges = new Dictionary<char, int>();
        foreach (var dependency in dependencies)
        {
            nodes.Add(dependency.step);
            nodes.Add(dependency.neighbor);
            if (!edges.ContainsKey(dependency.step))
            {
                edges[dependency.step] = new List<char>();
            }
            edges[dependency.step].Add(dependency.neighbor);
            if (!incomingEdges.ContainsKey(dependency.neighbor))
            {
                incomingEdges[dependency.neighbor] = 0;
            }
            incomingEdges[dependency.neighbor]++;
        }

        var result = new List<char>();
        while (nodes.Any())
        {
            var node = nodes.Where(n => !incomingEdges.ContainsKey(n)).OrderBy(n => n).First();
            nodes.Remove(node);
            result.Add(node);

            if (edges.ContainsKey(node))
            {
                foreach (var neighbor in edges[node])
                {
                    incomingEdges[neighbor]--;
                    if (incomingEdges[neighbor] == 0)
                    {
                        incomingEdges.Remove(neighbor);
                    }
                }
            }
        }
        return result;
    }

}