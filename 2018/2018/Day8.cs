namespace AoC2018;
public class Day8
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    [Solveable("2018/Puzzles/Day8.txt", "Day 8 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename).First();
        var data = new Queue<int>(Array.ConvertAll(input.Split(' '), int.Parse));
        var result = ParseTree(data);
        return new SolutionResult(SumMetadata(result).ToString());

        int SumMetadata(Node node)
        {
            return node.Metadata.Sum() + node.Children.Sum(child => SumMetadata(child));
        }
    }

    [Solveable("2018/Puzzles/Day8.txt", "Day 8 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename).First();
        var data = new Queue<int>(Array.ConvertAll(input.Split(' '), int.Parse));
        var result = ParseTree(data);

        return new SolutionResult(GetValue(result).ToString());
    }

    private static Node ParseTree(Queue<int> data)
    {
        var childCount = data.Dequeue();
        var metadataCount = data.Dequeue();
        var children = new List<Node>();
        var metadata = new List<int>();

        for (int i = 0; i < childCount; i++)
        {
            children.Add(ParseTree(data));
        }
        for (int i = 0; i < metadataCount; i++)
        {
            metadata.Add(data.Dequeue());
        }
        return new Node(children, metadata);
    }

    private static int GetValue(Node node)
    {
        if(node.Children.Count == 0)
        {
            return node.Metadata.Sum();
        }
        var sum = 0;
        foreach (var metadata in node.Metadata) 
        {
            if(node.Children.Count >= metadata)
            {
                sum += GetValue(node.Children[metadata - 1]);
            }
        }
        return sum;
    }


    public class Node
    {
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }

        public Node(List<Node> children, List<int> metadata)
        {
            Children = children;
            Metadata = metadata;
        }
    }
}