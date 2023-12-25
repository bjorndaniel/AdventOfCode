namespace AoC2023;
public class Day25
{
    public static Dictionary<string, HashSet<string>> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new Dictionary<string, HashSet<string>>();
        foreach (var line in lines)
        {
            var parts = line.Split(":");
            var name = parts[0].Trim();
            var connections = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
            result.Add(name, [.. connections]);
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day25.txt", "Day 25 part 1", 25)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var wires = ParseInput(filename);
        var visited = new HashSet<string>();
        var toAdd = FixConnections(wires);
        wires = toAdd;
        var graph = new AdjacencyGraph<string, Edge<string>>();
        foreach (var node in wires)
        {
            graph.AddVertex(node.Key);
            foreach (var edge in node.Value)
            {
                graph.AddEdge(new Edge<string>(node.Key, edge));
            }
        }
        var graphviz = new GraphvizAlgorithm<string, Edge<string>>(graph);
        graphviz.FormatEdge += (sender, args) =>
        {
            args.EdgeFormatter.Label.Value = args.Edge.Source + " -> " + args.Edge.Target;
            args.EdgeFormatter.Font = new GraphvizFont("Arial", 7);
        };
        var output = graphviz.Generate();
        File.WriteAllText("c:\\Temp\\day25.dot", output);
        //Install graphviz from https://graphviz.org/download/
        //Run dot -Tsvg c:\Temp\day25.dot -o c:\Temp\day25.svg -K neato and open the svg file
        //In that file it should be pretty obvious which 3 nodes should be disconnected
        //disconnect them below and run the code again
        Disconnect(wires, "", "");
        Disconnect(wires, "" , "");
        Disconnect(wires, "", "");
        DFS(wires, wires.First().Key, visited);
        if (visited.Count != wires.Count)
        {
            var product = visited.Count * (wires.Count - visited.Count);
            return new SolutionResult(product.ToString());
        }
        return new SolutionResult("Error");

        static Dictionary<string, HashSet<string>> FixConnections(Dictionary<string, HashSet<string>> wires)
        {
            var toAdd = new Dictionary<string, HashSet<string>>();
            foreach (var w in wires)
            {
                if (!toAdd.ContainsKey(w.Key))
                {
                    toAdd.Add(w.Key, []);
                }
                foreach (var c in w.Value)
                {
                    if (!toAdd.ContainsKey(c))
                    {
                        toAdd.Add(c, []);
                    }
                }
            }
            foreach (var w in wires)
            {
                foreach (var c in w.Value)
                {
                    toAdd[w.Key].Add(c);
                    toAdd[c].Add(w.Key);
                }
            }

            return toAdd;
        }

        static void Disconnect(Dictionary<string, HashSet<string>> wires, string w1, string w2)
        {
            wires[w1].Remove(w2);
            wires[w2].Remove(w1);
        }

        static HashSet<string> DFS(Dictionary<string, HashSet<string>> wires, string start, HashSet<string> visited)
        {
            visited.Add(start);
            if (wires.TryGetValue(start, out HashSet<string>? value))
            {
                foreach (var wire in value)
                {
                    if (!visited.Contains(wire))
                    {
                        DFS(wires, wire, visited);
                    }
                }
            }
            return visited;
        }
    }
}