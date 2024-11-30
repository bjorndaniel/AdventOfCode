namespace AoC2015;
public class Day7
{
    private static UInt16 _part1;

    public static List<Instruction> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Instruction>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            if (parts.Length == 3)
            {
                result.Add(new(Operation.SET, parts[2], parts[0], null));
            }
            else if (parts.Length == 4)
            {
                result.Add(new(Operation.NOT, parts[3], parts[1], null));
            }
            else
            {
                result.Add(new(parts[1] switch
                {
                    "AND" => Operation.AND,
                    "OR" => Operation.OR,
                    "LSHIFT" => Operation.LSHIFT,
                    "RSHIFT" => Operation.RSHIFT,
                    _ => throw new InvalidOperationException()
                }, parts[4], parts[0], parts[2]));
            }
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day7.txt", "Day 7 part 1", 7)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var register = new Dictionary<string, Gate>();
        var value = Evaluate(register, instructions, "a");
        return new SolutionResult(value.ToString());

        
    }

    [Solveable("2015/Puzzles/Day7.txt", "Day 7 part 2", 7)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var register = new Dictionary<string, Gate>();
        var value = Evaluate(register, instructions, "a");
        _part1 = value;
        register = new Dictionary<string, Gate>();
        var result = Evaluate(register,instructions, "a", true);
        return new SolutionResult(result.ToString());
    }

    private static UInt16 GetValue(Dictionary<string, Gate> register, List<Instruction> instructions, string source, bool isPart2 = false)
    {
        if(source == "b" && isPart2)
        {
            return _part1;
        }

        if (UInt16.TryParse(source, out var value))
        {
            return value;
        }

        return Evaluate(register, instructions, source, isPart2);
    }

    private static UInt16 Evaluate(Dictionary<string, Gate> register, List<Instruction> instructions, string wire, bool isPart2 = false)
    {
        if (register.TryGetValue(wire, out Gate? retVal))
        {
            return retVal.Value;
        }

        var instruction = instructions.First(i => i.Target == wire);
        var value = instruction.Operation switch
        {
            Operation.SET => GetValue(register, instructions, instruction.Source1, isPart2),
            Operation.NOT => (UInt16)~GetValue(register, instructions, instruction.Source1, isPart2),
            Operation.AND => (UInt16)(GetValue(register, instructions, instruction.Source1, isPart2) & GetValue(register, instructions, instruction.Source2!, isPart2)),
            Operation.OR => (UInt16)(GetValue(register, instructions, instruction.Source1, isPart2) | GetValue(register, instructions, instruction.Source2!, isPart2)),
            Operation.LSHIFT => (UInt16)(GetValue(register, instructions, instruction.Source1, isPart2) << GetValue(register, instructions, instruction.Source2!, isPart2)),
            Operation.RSHIFT => (UInt16)(GetValue(register, instructions, instruction.Source1, isPart2) >> GetValue(register, instructions, instruction.Source2!, isPart2)),
            _ => throw new InvalidOperationException(),
        };
        register[wire] = new Gate(wire, value);
        return value;
    }
}

public record Instruction(Operation Operation, string Target, string Source1, string? Source2);

public record Gate(string Name, UInt16 Value);

public enum Operation
{
    SET,
    AND,
    OR,
    LSHIFT,
    RSHIFT,
    NOT
}
