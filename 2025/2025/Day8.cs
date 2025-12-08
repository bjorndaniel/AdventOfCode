namespace AoC2025;

public class Day8
{
    public static List<Vector3> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Vector3>();
        foreach(var line in lines)
        {
            var parts = line.Split(',');
            var x = int.Parse(parts[0]);
            var y = int.Parse(parts[1]);
            var z = int.Parse(parts[2]);
            result.Add(new Vector3(x, y, z));
        }
        return result;
    }

    [Solveable("2025/Puzzles/Day8.txt", "Day 8 part 1", 8)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var boxes = ParseInput(filename);
        var n = boxes.Count;
        var (parent, rank) = InitUnionFind(n);
        var pairs = BuildPairs(boxes);
        pairs.Sort((a, b) => a.d.CompareTo(b.d));
        var maxConnections = filename.Contains("test") ? 10 : 1000;
        var connections = 0;
        for (var k = 0; k < pairs.Count && connections < maxConnections; k++)
        {
            var (i, j, _) = pairs[k];
            var rootI = Find(i, parent);
            var rootJ = Find(j, parent);
            if (rootI != rootJ)
            {
                Union(rootI, rootJ, parent, rank);
            }
            connections++;
        }
        var sizeByRoot = new Dictionary<int, int>();
        for (var i = 0; i < n; i++)
        {
            var root = Find(i, parent);
            if (!sizeByRoot.ContainsKey(root))
            {
                sizeByRoot[root] = 0;
            }
            sizeByRoot[root]++;
        }
        var largestThree = sizeByRoot.Values.OrderByDescending(x => x).Take(3).ToList();
        while (largestThree.Count < 3)
        {
            largestThree.Add(1);
        }
        var answer = largestThree.Aggregate(1, (acc, v) => acc * v);
        return new SolutionResult(answer.ToString());
    }

    [Solveable("2025/Puzzles/Day8.txt", "Day 8 part 2", 8)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var boxes = ParseInput(filename);
        var n = boxes.Count;
        var (parent, rank) = InitUnionFind(n);
        var pairs = BuildPairs(boxes);
        pairs.Sort((a, b) => a.d.CompareTo(b.d));
        var components = n;
        var lastConnectedI = -1;
        var lastConnectedJ = -1;
        for (var k = 0; k < pairs.Count && components > 1; k++)
        {
            var (i, j, _) = pairs[k];
            var rootI = Find(i, parent);
            var rootJ = Find(j, parent);
            if (rootI != rootJ)
            {
                Union(rootI, rootJ, parent, rank);
                components--;
                if (components == 1)
                {
                    lastConnectedI = i;
                    lastConnectedJ = j;
                }
            }
        }
        if (lastConnectedI == -1 || lastConnectedJ == -1)
        {
            return new SolutionResult("0");
        }
        var product = (long)boxes[lastConnectedI].X * (long)boxes[lastConnectedJ].X;
        return new SolutionResult(product.ToString());
    }

    private static int Find(int x, int[] parent)
    {
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x], parent);
        }
        return parent[x];
    }

    private static void Union(int a, int b, int[] parent, int[] rank)
    {
        var rootA = Find(a, parent);
        var rootB = Find(b, parent);
        if (rootA == rootB)
        {
            return;
        }
        if (rank[rootA] < rank[rootB])
        {
            parent[rootA] = rootB;
        }
        else if (rank[rootA] > rank[rootB])
        {
            parent[rootB] = rootA;
        }
        else
        {
            parent[rootB] = rootA;
            rank[rootA]++;
        }
    }

    private static (int[] parent, int[] rank) InitUnionFind(int n)
    {
        var parent = new int[n];
        var rank = new int[n];
        for (var i = 0; i < n; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
        return (parent, rank);
    }

    private static List<(int i, int j, double d)> BuildPairs(List<Vector3> boxes)
    {
        var n = boxes.Count;
        var pairs = new List<(int i, int j, double d)>(n * (n - 1) / 2);
        for (var i = 0; i < n; i++)
        {
            for (var j = i + 1; j < n; j++)
            {
                var d = Helpers.VectorDistance(boxes[i], boxes[j]);
                pairs.Add((i, j, d));
            }
        }
        return pairs;
    }
}