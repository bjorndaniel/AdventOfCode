namespace AoC2022;
public class Day5
{
    public static (List<CrateStack> stacks, List<Movement> movements) ParseInput(string filename)
    {
        var stacks = new List<CrateStack>();
        var movements = new List<Movement>();
        var lines = File.ReadAllLines(filename);
        var parseMovements = false;
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                parseMovements = true;
                continue;
            }
            if (parseMovements)
            {
                var moves = l.Split(" ");
                var count = moves[1];
                var from = moves[3];
                var to = moves[5];
                movements.Add(new Movement(int.Parse(from), int.Parse(to), int.Parse(count)));
            }
            else if (l.Contains("["))
            {
                var columns = l.Chunk(4).ToArray();
                if (!stacks.Any())
                {
                    stacks = Enumerable.Range(1, columns.Count()).Select(_ => new CrateStack(new List<Crate>(), _)).ToList();
                }
                for (int i = 1; i < columns.Count() + 1; i++)
                {
                    if (columns[i - 1].Contains('['))
                    {
                        var value = columns[i - 1].First(_ => _ != '[' && _ != ']');
                        var stack = stacks.First(_ => _.StackNumber == i);
                        stack.Crates.Add(new Crate(i, stack.StackHeight, value));
                    }
                }
            }
        }
        return (stacks, movements);
    }

    public static string SolvePart1(string filename)
    {
        var (stacks, movements) = ParseInput(filename);
        foreach (var m in movements)
        {
            var startStack = stacks.First(_ => _.StackNumber == m.From);
            var endStack = stacks.First(_ => _.StackNumber == m.To);
            var toMove = startStack.Crates.Take(m.NrOfCrates).ToList();
            startStack.Crates.RemoveRange(0, toMove.Count());
            for (int i = 0; i < toMove.Count; i++)
            {
                endStack.Crates.Insert(0, toMove[i]);
                toMove[i].StackNumber = m.To;
                toMove[i].StackIndex = endStack.Crates.Count;
            }
        }
        return string.Concat(stacks.Select(_ => _.Crates.OrderByDescending(_ => _.StackIndex).First().Value));
    }

    public static string SolvePart2(string filename)
    {
        var (stacks, movements) = ParseInput(filename);
        stacks.ForEach(_ =>
        {
            _.Crates.Reverse();
            _.SetIndices();
        });
        foreach (var m in movements)
        {
            var startStack = stacks.First(_ => _.StackNumber == m.From);
            var endStack = stacks.First(_ => _.StackNumber == m.To);
            var toMove = startStack.Crates.OrderByDescending(_ => _.StackIndex).Take(m.NrOfCrates).ToList();
            toMove.ForEach(_ => startStack.Crates.Remove(_));
            toMove.Reverse();
            for (int i = 0; i < toMove.Count(); i++)
            {
                endStack.Crates.Add(toMove[i]);
                toMove[i].StackNumber = m.To;
            }
            stacks.ForEach(_ => _.SetIndices());
        }
        return string.Concat(stacks.Select(_ => _.Crates.OrderByDescending(_ => _.StackIndex).First().Value));
    }
}

public record Movement(int From, int To, int NrOfCrates) { }

public class Crate
{

    public Crate(int stackNumber, int stackIndex, char value)
    {
        StackNumber = stackNumber;
        StackIndex = stackIndex;
        Value = value;
    }

    public int StackNumber { get; set; }
    public int StackIndex { get; set; }
    public char Value { get; }
}


public record CrateStack(List<Crate> Crates, int StackNumber)
{
    public int StackHeight => Crates.Count;

    public void SetIndices()
    {
        foreach(var (c, i) in Crates.Select((v,i) => (v, i)))
        {
            c.StackIndex = i;
        }
    }
}
