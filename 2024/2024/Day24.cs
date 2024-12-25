namespace AoC2024;
public class Day24
{
    public static (List<Wire> wires, List<Gate> ops) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var wires = new List<Wire>();
        var gates = new List<Gate>();
        bool isWire = true;
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                break;
            }
            if (isWire)
            {
                var parts = l.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                wires.Add(new Wire(parts[0][..^1], int.Parse(parts[1])));
            }
        }
        foreach (var l in lines)
        {
            if (string.IsNullOrEmpty(l))
            {
                isWire = false;
                continue;
            }
            if (isWire)
            {
                continue;
            }
            var instr = l.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var left = instr[0];
            var right = instr[2];
            var output = instr[4];
            var op = GetOperation(instr[1]);
            var first = wires.FirstOrDefault(_ => _.Name == left);
            var second = wires.FirstOrDefault(_ => _.Name == right);
            var outWire = wires.FirstOrDefault(_ => _.Name == output);
            first = first ?? new Wire(left, null);
            second = second ?? new Wire(right, null);
            outWire = outWire ?? new Wire(output, null);
            gates.Add(new Gate(first, second, op, outWire));
        }

        return (wires, gates);

        Op GetOperation(string op)
        {
            return op switch
            {
                "AND" => Op.AND,
                "OR" => Op.OR,
                "XOR" => Op.XOR,
                _ => throw new Exception("Unknown operation")
            };
        }
    }

    [Solveable("2024/Puzzles/Day24.txt", "Day 24 part 1", 24)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (wires, gates) = ParseInput(filename);
        EvaluateGates(gates);
        var zGates = gates.Where(g => g.Output.Name.StartsWith("z")).ToList();
        var ordered = zGates.OrderByDescending(g => g.Output.Name).ToList();
        var result = ordered.Select(g => g.Output.Value!.ToString()).Aggregate((a, b) => $"{a}{b}");
        var names = ordered.Select(g => g.Output.Name).Aggregate((a, b) => $"{a}{b}");
        return new SolutionResult(Convert.ToInt64(result, 2).ToString());
    }

    [Solveable("2024/Puzzles/Day24.txt", "Day 24 part 2", 24)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (wires, gates) = ParseInput(filename);
        EvaluateGates(gates);

        var xWires = wires.Where(w => w.Name.StartsWith("x")).OrderByDescending(_ => _.Name).ToList();
        var yWires = wires.Where(w => w.Name.StartsWith("y")).OrderByDescending(_ => _.Name).ToList();
        var xDecimal = Convert.ToInt64(xWires.Select(w => w.Value!.Value.ToString()).Aggregate((a, b) => $"{a}{b}"), 2);
        var yDecimal = Convert.ToInt64(yWires.Select(w => w.Value!.Value.ToString()).Aggregate((a, b) => $"{a}{b}"), 2);
        var expectedResult = xDecimal + yDecimal;

        var zGates = gates.Where(g => g.Output.Name.StartsWith("z")).ToList();
        var ordered = zGates.OrderByDescending(g => g.Output.Name).ToList();
        var result = ordered.Select(g => g.Output.Value!.ToString()).Aggregate((a, b) => $"{a}{b}");
        var actualResult = Convert.ToInt64(result, 2);

        var swappedGates = new List<string>();

        if (actualResult == expectedResult)
        {
            return new SolutionResult("No swaps needed");
        }

        var visited = new HashSet<string>();
        var memo = new Dictionary<string, bool>();
        var stack = new Stack<(List<Gate> gates, List<string> swaps)>();
        stack.Push((gates, new List<string>()));

        while (stack.Count > 0)
        {
            var (currentGates, currentSwaps) = stack.Pop();
            EvaluateGates(currentGates);

            result = ordered.Select(g => g.Output.Value!.ToString()).Aggregate((a, b) => $"{a}{b}");
            actualResult = Convert.ToInt64(result, 2);

            if (actualResult == expectedResult)
            {
                swappedGates = currentSwaps;
                break;
            }

            var stateKey = string.Join(",", currentSwaps.OrderBy(s => s));
            if (memo.ContainsKey(stateKey) && !memo[stateKey])
            {
                continue;
            }

            for (int i = 0; i < currentGates.Count; i++)
            {
                for (int j = i + 1; j < currentGates.Count; j++)
                {
                    var newGates = currentGates.Select(g => new Gate(g.First, g.Second, g.Instruction, g.Output)).ToList();
                    SwapGateOutputs(newGates[i], newGates[j]);

                    var newSwaps = new List<string>(currentSwaps) { newGates[i].Output.Name, newGates[j].Output.Name };
                    var swapKey = string.Join(",", newSwaps.OrderBy(s => s));

                    if (!visited.Contains(swapKey))
                    {
                        visited.Add(swapKey);
                        stack.Push((newGates, newSwaps));
                    }
                }
            }

            memo[stateKey] = false;
        }

        swappedGates = swappedGates.Distinct().OrderBy(name => name).ToList();
        return new SolutionResult(string.Join(", ", swappedGates));
    }

    private static void SwapGateOutputs(Gate gate1, Gate gate2)
    {
        var temp = gate1.Output;
        gate1.Output = gate2.Output;
        gate2.Output = temp;
    }

    private static void EvaluateGates(List<Gate> gates)
    {
        foreach (var gate in gates)
        {
            EvaluateGate(gate, gates);
        }
    }

    private static void EvaluateGate(Gate gate, List<Gate> gates)
    {
        if (gate.Output.Value.HasValue && gate.First.Value.HasValue && gate.Second.Value.HasValue)
        {
            return;
        }

        if (!gate.First.Value.HasValue)
        {
            var firstGate = gates.FirstOrDefault(g => g.Output.Name == gate.First.Name);
            if (firstGate != null)
            {
                EvaluateGate(firstGate, gates);
                gate.First.Value = firstGate.Output.Value;
            }
        }

        if (!gate.Second.Value.HasValue)
        {
            var secondGate = gates.FirstOrDefault(g => g.Output.Name == gate.Second.Name);
            if (secondGate != null)
            {
                EvaluateGate(secondGate, gates);
                gate.Second.Value = secondGate.Output.Value;
            }
        }

        if (gate.First.Value.HasValue && gate.Second.Value.HasValue)
        {
            gate.Output.Value = gate.Instruction switch
            {
                Op.AND => gate.First.Value.Value & gate.Second.Value.Value,
                Op.OR => gate.First.Value.Value | gate.Second.Value.Value,
                Op.XOR => gate.First.Value.Value ^ gate.Second.Value.Value,
                _ => throw new Exception("Unknown operation")
            };
        }
    }
}

public class Wire(string name, int? value) : IComparable
{
    public string Name { get; init; } = name;
    public int? Value { get; set; } = value;

    public int CompareTo(object? obj)
    {
        if (obj is not Wire other)
        {
            throw new ArgumentException("Object is not a Wire");
        }

        int charComparison = string.Compare(Name[0].ToString(), other.Name[0].ToString(), StringComparison.Ordinal);
        if (charComparison != 0)
        {
            return charComparison;
        }

        int thisNumber = int.Parse(Name[1..]);
        int otherNumber = int.Parse(other.Name[1..]);
        return thisNumber.CompareTo(otherNumber);
    }

}

public class Gate(Wire first, Wire second, Op instruction, Wire output)
{
    public Wire First { get; init; } = first;
    public Wire Second { get; init; } = second;
    public Op Instruction { get; init; } = instruction;
    public Wire Output { get; set; } = output;
}

public enum Op
{
    AND,
    OR,
    XOR
}

 