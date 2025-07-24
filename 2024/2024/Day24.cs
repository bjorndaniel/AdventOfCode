namespace AoC2024;

/// <summary>
/// Day 24: Crossed Wires - Binary Adder Circuit Analysis
/// 
/// This solution analyzes a circuit of logic gates that should implement binary addition.
/// The circuit takes two binary numbers (x and y wires) and produces their sum (z wires).
/// However, some gate outputs have been swapped, causing incorrect results.
/// 
/// Part 1: Simulate the circuit and get the decimal output
/// Part 2: Find which wires are involved in swaps by validating ripple-carry adder patterns
/// </summary>
public class Day24
{
    /// <summary>
    /// Parses the input file to extract initial wire values and gate definitions.
    /// 
    /// Input format:
    /// - First section: wire_name: initial_value (e.g., "x00: 1")
    /// - Empty line separator
    /// - Second section: gate definitions (e.g., "x00 AND y00 -> z00")
    /// </summary>
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

    /// <summary>
    /// Solves Part 2: Finds swapped wires in a binary adder circuit.
    /// 
    /// The circuit should implement a ripple-carry adder that adds two binary numbers:
    /// - X wires (x00, x01, x02, ...) represent the first binary number
    /// - Y wires (y00, y01, y02, ...) represent the second binary number  
    /// - Z wires (z00, z01, z02, ...) represent the sum output
    /// 
    /// In a correct ripple-carry adder:
    /// - Each bit position has specific gate patterns for addition and carry propagation
    /// - Some gates have had their output wires swapped, causing incorrect results
    /// - We need to identify exactly 8 wires (4 pairs) that are involved in swaps
    /// </summary>
    [Solveable("2024/Puzzles/Day24.txt", "Day 24 part 2", 24)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (wires, gates) = ParseInput(filename);
        
        // Collection to store all wire names that violate ripple-carry adder rules
        // These violations indicate gates with swapped outputs
        var problematicWires = new HashSet<string>();
        
        // Analyze each gate in the circuit to find violations of ripple-carry adder structure
        foreach (var gate in gates)
        {
            /*
             * RULE 1: Z-output validation (Final result bits)
             * 
             * In a ripple-carry adder, each z wire represents a bit of the final sum.
             * - z00 is the least significant bit (LSB)
             * - z01, z02, z03... are subsequent bits
             * - z[MAX] is the most significant bit (MSB) and represents the final carry-out
             * 
             * For correct operation:
             * - All z outputs (except the highest) should come from XOR gates
             * - The highest z output should come from an OR gate (final carry)
             */
            if (gate.Output.Name.StartsWith("z") && gate.Instruction != Op.XOR)
            {
                // Extract the bit number from the wire name (e.g., "z05" -> 5)
                var bitNum = int.Parse(gate.Output.Name[1..]);
                
                // Find the highest bit number in the circuit
                var maxBit = gates.Where(g => g.Output.Name.StartsWith("z"))
                                 .Max(g => int.Parse(g.Output.Name[1..]));
                
                // Only the final carry bit (highest z) should be OR, all others should be XOR
                if (bitNum != maxBit)
                {
                    problematicWires.Add(gate.Output.Name);
                }
            }
            
            /*
             * RULE 2: Intermediate XOR gate validation
             * 
             * XOR gates that don't take direct x/y inputs are intermediate gates
             * that combine partial sums with carry bits to produce final z outputs.
             * 
             * Example: If we have (x01 XOR y01) -> temp1 and carry_in -> temp2,
             * then (temp1 XOR temp2) should produce z01.
             * 
             * These intermediate XOR gates should ALWAYS output to z wires.
             */
            if (gate.Instruction == Op.XOR && 
                !gate.First.Name.StartsWith("x") && !gate.First.Name.StartsWith("y") &&
                !gate.Second.Name.StartsWith("x") && !gate.Second.Name.StartsWith("y"))
            {
                // If this XOR gate has intermediate inputs, it should output to a z wire
                if (!gate.Output.Name.StartsWith("z"))
                {
                    problematicWires.Add(gate.Output.Name);
                }
            }
            
            /*
             * RULE 3: Direct x,y AND gate validation (Carry generation)
             * 
             * AND gates that take direct x[n] and y[n] inputs generate carry bits.
             * These carry bits must feed into OR gates for carry propagation.
             * 
             * Example: (x05 AND y05) generates a carry that should be input to an OR gate
             * Exception: x00 AND y00 might have special handling for the initial carry
             */
            if (gate.Instruction == Op.AND && IsDirectXYPair(gate.First.Name, gate.Second.Name))
            {
                var bitNum = int.Parse(gate.First.Name[1..]);
                
                // Skip validation for the first bit position (special case)
                if (bitNum != 0) 
                {
                    // Check if this AND gate's output feeds into any OR gate
                    var isInputToOR = gates.Any(g => g.Instruction == Op.OR && 
                                                   (g.First.Name == gate.Output.Name || g.Second.Name == gate.Output.Name));
                    
                    // If the carry doesn't feed into an OR gate, it's problematic
                    if (!isInputToOR)
                    {
                        problematicWires.Add(gate.Output.Name);
                    }
                }
            }
            
            /*
             * RULE 4: Direct x,y XOR gate validation (Partial sum generation)
             * 
             * XOR gates that take direct x[n] and y[n] inputs generate partial sums.
             * These partial sums need to be combined with carry-in bits in subsequent XOR gates.
             * 
             * Special cases:
             * - x00 XOR y00 should go directly to z00 (no carry-in for first bit)
             * - All other x[n] XOR y[n] should feed into another XOR gate (not directly to z)
             */
            if (gate.Instruction == Op.XOR && IsDirectXYPair(gate.First.Name, gate.Second.Name))
            {
                var bitNum = int.Parse(gate.First.Name[1..]);
                
                if (bitNum == 0)
                {
                    // First bit: x00 XOR y00 should go directly to z00 (no carry-in)
                    if (gate.Output.Name != "z00")
                    {
                        problematicWires.Add(gate.Output.Name);
                    }
                }
                else
                {
                    // Higher bits: x[n] XOR y[n] should be input to another XOR gate
                    // This other XOR gate will combine the partial sum with carry-in
                    var isInputToXOR = gates.Any(g => g.Instruction == Op.XOR && 
                                                    (g.First.Name == gate.Output.Name || g.Second.Name == gate.Output.Name));
                    
                    // If the partial sum doesn't feed into another XOR, it's problematic
                    if (!isInputToXOR)
                    {
                        problematicWires.Add(gate.Output.Name);
                    }
                }
            }
        }
        
        // Sort the problematic wire names alphabetically as required by the problem
        var sortedWires = problematicWires.OrderBy(x => x).ToList();
        
        // Return the comma-separated list of wire names involved in swaps
        return new SolutionResult(string.Join(",", sortedWires));
    }
    
    /// <summary>
    /// Helper method to check if two wire names represent a direct x,y input pair.
    /// 
    /// A direct pair means:
    /// - One wire starts with 'x' and the other starts with 'y'
    /// - Both have the same bit number (e.g., x05 and y05)
    /// 
    /// This identifies gates that operate directly on input bits rather than intermediate values.
    /// </summary>
    /// <param name="input1">First wire name</param>
    /// <param name="input2">Second wire name</param>
    /// <returns>True if the wires form a direct x,y pair for the same bit position</returns>
    private static bool IsDirectXYPair(string input1, string input2)
    {
        return (input1.StartsWith("x") && input2.StartsWith("y") && input1[1..] == input2[1..]) ||
               (input1.StartsWith("y") && input2.StartsWith("x") && input1[1..] == input2[1..]);
    }

    /// <summary>
    /// Helper method used internally for swapping gate outputs.
    /// This method isn't used in the current solution but could be useful
    /// for implementing alternative approaches that actually perform swaps.
    /// </summary>
    private static void SwapGateOutputs(Gate gate1, Gate gate2)
    {
        var temp = gate1.Output;
        gate1.Output = gate2.Output;
        gate2.Output = temp;
    }

    /// <summary>
    /// Evaluates all gates in the circuit to determine their output values.
    /// Uses recursive evaluation to handle dependencies between gates.
    /// </summary>
    private static void EvaluateGates(List<Gate> gates)
    {
        foreach (var gate in gates)
        {
            EvaluateGate(gate, gates);
        }
    }

    /// <summary>
    /// Recursively evaluates a single gate and its dependencies.
    /// 
    /// Process:
    /// 1. If gate is already evaluated, return immediately
    /// 2. Recursively evaluate any input gates that haven't been calculated yet
    /// 3. Once both inputs are available, calculate the gate's output based on its operation
    /// 
    /// This handles the dependency graph where some gates depend on outputs of other gates.
    /// </summary>
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

/// <summary>
/// Represents a wire in the circuit that can carry a binary value (0 or 1).
/// 
/// Wires are named with a prefix and number:
/// - x## wires: Input bits for the first binary number
/// - y## wires: Input bits for the second binary number  
/// - z## wires: Output bits for the sum result
/// - Other names: Intermediate wires for internal circuit connections
/// 
/// The ## represents the bit position, with 00 being the least significant bit.
/// </summary>
public class Wire(string name, int? value) : IComparable
{
    /// <summary>
    /// The wire's identifier (e.g., "x00", "y05", "z12", "abc")
    /// </summary>
    public string Name { get; init; } = name;
    
    /// <summary>
    /// The binary value carried by this wire (0, 1, or null if not yet calculated)
    /// </summary>
    public int? Value { get; set; } = value;

    /// <summary>
    /// Implements comparison for sorting wires.
    /// Sorts first by the first character, then by the numeric part.
    /// This ensures x00, x01, x02... are ordered correctly.
    /// </summary>
    public int CompareTo(object? obj)
    {
        if (obj is not Wire other)
        {
            throw new ArgumentException("Object is not a Wire");
        }

        // First compare by the first character (x, y, z, etc.)
        int charComparison = string.Compare(Name[0].ToString(), other.Name[0].ToString(), StringComparison.Ordinal);
        if (charComparison != 0)
        {
            return charComparison;
        }

        // Then compare by the numeric part
        int thisNumber = int.Parse(Name[1..]);
        int otherNumber = int.Parse(other.Name[1..]);
        return thisNumber.CompareTo(otherNumber);
    }
}

/// <summary>
/// Represents a logic gate with two inputs, one output, and an operation.
/// 
/// In the context of binary addition:
/// - Gates process binary signals (0 or 1) 
/// - Each gate waits for both inputs before producing output
/// - Gates implement AND, OR, or XOR logic operations
/// </summary>
public class Gate(Wire first, Wire second, Op instruction, Wire output)
{
    /// <summary>First input wire to the gate</summary>
    public Wire First { get; init; } = first;
    
    /// <summary>Second input wire to the gate</summary>
    public Wire Second { get; init; } = second;
    
    /// <summary>The logical operation this gate performs</summary>
    public Op Instruction { get; init; } = instruction;
    
    /// <summary>
    /// The output wire where this gate sends its result.
    /// Note: This can be modified to simulate wire swapping.
    /// </summary>
    public Wire Output { get; set; } = output;
}

/// <summary>
/// The three types of logic operations supported by gates in the circuit.
/// 
/// - AND: Output is 1 only if both inputs are 1
/// - OR:  Output is 1 if either input is 1  
/// - XOR: Output is 1 if inputs are different (exclusive or)
/// 
/// These three operations are sufficient to build a complete binary adder.
/// </summary>
public enum Op
{
    AND,
    OR,
    XOR
}