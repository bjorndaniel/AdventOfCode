namespace AoC2023;
public class Day8
{
    public static Map ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var instructions = lines.First().ToList();
        var nodes = new Dictionary<string, Node>();
        foreach (var line in lines.Skip(2))
        {
            var name = line.Split("=").First().Trim();
            var left = line.Split("=").Last().Split(",").First().Replace("(", "").Trim();
            var right = line.Split("=").Last().Split(",").Last().Replace(")", "").Trim();
            Node lNode = null!;
            Node rNode = null!;
            if (nodes.TryGetValue(left, out Node? v))
            {
                lNode = v;
            }
            else
            {
                lNode = new Node(left, null!, null!);
                nodes.Add(left, lNode);
            }
            if (!nodes.TryGetValue(right, out Node? v1))
            {
                rNode = new Node(right, null!, null!);
                nodes.Add(right, rNode);
            }
            else
            {
                rNode = v1;
            }
            if (nodes.TryGetValue(name, out Node? v2))
            {
                v2.Left = lNode;
                v2.Right = rNode;
            }
            else
            {
                var node = new Node(name, lNode, rNode);
                nodes.Add(name, node);
            }
        }
        return new Map(instructions, nodes.Select(_ => _.Value).ToList());
    }

    [Solveable("2023/Puzzles/Day8.txt", "Day 8 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var map = ParseInput(filename);
        var currentInstruction = 0;
        var currentNode = map.Nodes.First(_ => _.Name == "AAA");
        var steps = 0;
        while (currentNode.Name != "ZZZ")
        {
            currentInstruction %= map.Instructions.Count;
            if (map.Instructions[currentInstruction] == 'L')
            {
                currentNode = currentNode.Left;
            }
            else
            {
                currentNode = currentNode.Right;
            }
            currentInstruction++;
            steps++;
        }

        return new SolutionResult(steps.ToString());
    }

    [Solveable("2023/Puzzles/Day8.txt", "Day 8 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var map = ParseInput(filename);
        var queue = new Queue<Node>();
        var visited = new HashSet<Node>();
        var startingNodes = map.Nodes.Where(_ => _.Name.EndsWith('A')).ToList();
        var stepsToEnd = new List<long>();
        foreach(var startingNode in startingNodes)
        {
            var currentNode = startingNode;
            var currentInstruction = 0;
            var steps = 0;
            while (!currentNode.Name.EndsWith('Z'))//Each A node has exactly one path to a Z that repeats
            {
                currentInstruction %= map.Instructions.Count;
                if (map.Instructions[currentInstruction] == 'L')
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }
                currentInstruction++;
                steps++;
            }
            stepsToEnd.Add(steps);
        }
        return new SolutionResult(Helpers.CalculateLCM(stepsToEnd).ToString());
    }

    public record Map(List<char> Instructions, List<Node> Nodes) { }
    public class Node(string name, Node left, Node right)
    {
        public string Name { get; set; } = name;
        public Node Left { get; set; } = left;
        public Node Right { get; set; } = right;
    }


}