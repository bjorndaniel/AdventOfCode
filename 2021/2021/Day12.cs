namespace Advent2021;
public class Day12
{
    private static int _pathCount;
    private static Stack<string> path = new Stack<string>();   // the current path
    private static List<string> onPath = new List<string>();     // the set of vertices on the path
    private static bool _oneSmall;

    //Adapted from https://stackoverflow.com/questions/16534657/how-to-find-an-index-of-a-string-in-a-list/16534702
    //and https://introcs.cs.princeton.edu/java/45graph/AllPaths.java.html
    public static int FindAllPaths(string filename, bool oneSmall = false)
    {
        _oneSmall = oneSmall;
        var graph = new Graph();
        AddEdges(filename, graph);
        return CountAllPaths(graph, "start", "end");
    }

    private static void AddEdges(string filename, Graph g)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var from = line.Split("-")[0];
            var to = line.Split("-")[1];
            g.AddEdge(from, to);
        }

    }

    private static int CountAllPaths(Graph g, string s, string d)
    {
        _pathCount = 0;
        Enumerate(g, s, d);
        return _pathCount;
    }

    private static void Enumerate(Graph g, string v, string t)
    {

        // add node v to current path from s
        path.Push(v);
        onPath.Add(v);

        // found path from s to t - currently prints in reverse order because of stack
        if (v.Equals(t))
        {
            _pathCount++;
        }
        // consider all neighbors that would continue path with repeating a node
        else
        {
            foreach (var w in g.GetAdjacent(v))
            {
                if (_oneSmall)
                {
                    var isSmallCave = w.All(c => char.IsLower(c)) && w != "start" && w != "end";

                    if (isSmallCave)
                    {
                        var smallCaves = onPath.Where(p => p.All(p => char.IsLower(p)) && p != "start" && p != "end");
                        var groups = smallCaves.GroupBy(c => c).Select(_ => new { _.Key, Count = _.Count() });
                        if (groups.All(g => g.Count == 1) || !groups.Any(_ => _.Key == w) || w.All(p => char.IsUpper(p)))
                        {
                            Enumerate(g, w, t);
                        }
                    }
                    else
                    {
                        if (!onPath.Contains(w) || w.All(p => char.IsUpper(p)))
                            Enumerate(g, w, t);
                    }
                }
                else
                {
                    if (!onPath.Contains(w) || w.All(p => char.IsUpper(p)))
                    {
                        Enumerate(g, w, t);
                    }
                }
            }
        }
        path.Pop();
        onPath.Remove(v);
        // done exploring from v, so remove from path

    }
}

public class Graph
{
    private Dictionary<string, List<string>> _adjList;

    public Graph()
    {
        _adjList = new Dictionary<string, List<string>>();
    }

    public void AddEdge(string u, string v)
    {
        if (_adjList.ContainsKey(u))
        {
            _adjList[u].Add(v);
        }
        else
        {
            _adjList.Add(u, new List<string> { v });
        }

        if (_adjList.ContainsKey(v))
        {
            _adjList[v].Add(u);
        }
        else
        {
            _adjList.Add(v, new List<string> { u });
        }
    }

    public List<string> GetAdjacent(string x) =>
        _adjList.ContainsKey(x) ? _adjList[x] : new List<string>();
}