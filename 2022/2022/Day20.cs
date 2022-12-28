namespace AoC2022;
public static class Day20
{
    public static IEnumerable<Node> ParseInput(string filename, bool part2 = false)
    {
        var lines = File.ReadAllLines(filename);
        var list = new List<Node>();
        var counter = 0;
        foreach (var l in lines)
        {
            list.Add(new Node(long.Parse(l) * (part2 ? 811589153 : 1), counter));
            counter++;
        }
        return list;
    }

    public static long SolvePart1(string filename, IPrinter printer)
    {
        var keys = ParseInput(filename);
        //printer.Print(keys.Print());
        //printer.Flush();
        //printer.Flush();
        return Solver(printer, keys.ToList());
    }

    private static long Solver(IPrinter printer, List<Node> keys, int nrOfRounds = 1)
    {
        for (int mix = 0; mix < nrOfRounds; mix++)
        {
            for (int i = 0; i < keys.Count(); i++)
            {
                var k = keys.First(_ => _.OriginalPosition == i);
                var current = k.CurrentPosition;//keys.GetPosition(k);
                var mod = keys.Count() - 1;
                if (k.Value == 0)
                {
                    continue;
                }
                var value = k.Value + current;
                value = value % mod;
                if (value < 0)
                {
                    value = keys.Count() + value - 1;
                }
                keys.Remove(k);
                keys.Insert((int)value, k);
                for (int m = 0; m < keys.Count(); m++)
                {
                    keys[m].CurrentPosition = m;
                }
            }
            //printer
            //.Print(keys.Select(_ => _.Value.ToString()).Aggregate((a, b) => $"{a}, {b}"));
            //printer.Flush();
        }
        var index = keys.First(_ => _.Value == 0).CurrentPosition;

        var index1 = ((1000 + index + 1) % keys.Count() - 1);
        var index2 = ((2000 + index + 1) % keys.Count() - 1);
        var index3 = ((3000 + index + 1) % keys.Count() - 1);
        var k1 = keys.ElementAt(index1).Value;
        var k2 = keys.ElementAt(index2).Value;
        var k3 = keys.ElementAt(index3).Value;
        return k1 + k2 + k3;
    }

    public static long SolvePart2(string filename, IPrinter printer)
    {
        var keys = ParseInput(filename, true);
        return Solver(printer, keys.ToList(), 10);
    }
}

public class Node
{
    public Node(long value, int originalPosition)
    {
        Value = value;
        OriginalPosition = originalPosition;
        CurrentPosition = originalPosition;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; }
    public Node? Next { get; set; }
    public Node? Previous { get; set; }
    public long Value { get; }
    public int OriginalPosition { get; }
    public int CurrentPosition { get; set; }
}