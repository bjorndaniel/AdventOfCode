namespace AoC2018;
public class Day7
{
    public static List<(char step, char neighbor)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(char step, char neighbor)>();
        foreach (var line in lines)
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
        var dependencies = ParseInput(filename);
        var isTest = filename.Contains("test", StringComparison.OrdinalIgnoreCase);
        var sorted = TopologicalSortWithWorkers(dependencies,  isTest ? 2 : 5, isTest ? 0 : 60, printer);
        return new SolutionResult(sorted.ToString());
    }

    private static List<char> TopologicalSort(List<(char step, char neighbor)> dependencies)
    {
        var (nodes, edges, incomingEdges) = Initialize(dependencies);
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

    private static int TopologicalSortWithWorkers(List<(char step, char neighbor)> dependencies, int numWorkers, int stepTime, IPrinter printer)
    {
        var (nodes, edges, incomingEdges) = Initialize(dependencies);
        var availableTasks = new List<(char task, int time)>();
        var ongoingTasks = new List<(char task, int time)>();
        var time = 0;
        while (nodes.Any() || ongoingTasks.Any())
        {
            var newAvailableTasks = nodes.Where(n => !incomingEdges.ContainsKey(n))
                                         .Select(n => (n, (n - 'A') + 1 + stepTime))
                                         .ToList();
            foreach (var (task, tTime) in newAvailableTasks.OrderBy(_ => _.n))
            {
                nodes.Remove(task);
                availableTasks.Add((task, tTime));
            }

            while (ongoingTasks.Count < numWorkers && availableTasks.Any())
            {
                var task = availableTasks.OrderBy(_ => _.task).First();
                availableTasks.Remove(task);
                ongoingTasks.Add(task);
            }

            var completedTask = ongoingTasks.OrderBy(_ => _.time).First();
            time += completedTask.time;
            ongoingTasks.Remove(completedTask);
            ongoingTasks = ongoingTasks.Select(t => (t.task, t.time - completedTask.time)).ToList();
            if (edges.ContainsKey(completedTask.task))
            {
                foreach (var neighbor in edges[completedTask.task])
                {
                    if(incomingEdges.ContainsKey(neighbor))
                    {
                        incomingEdges[neighbor]--;
                        if (incomingEdges[neighbor] == 0)
                        {
                            incomingEdges.Remove(neighbor);
                        }
                    }
                }
            }
        }
        return time;
    }

    private static (HashSet<char> nodes, Dictionary<char, List<char>> edges, Dictionary<char, int> incomingEdges) Initialize(List<(char step, char neighbor)> dependencies)
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
        return (nodes, edges, incomingEdges);
    }

}