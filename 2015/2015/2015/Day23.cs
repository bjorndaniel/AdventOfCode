namespace AoC2015;

public class Day23
{
    public static List<Instruction> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Instruction>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length > 0)
            {
                switch (parts[0])
                {
                    case "hlf":
                        result.Add(new Instruction(InstructionType.hlf, parts[1]));
                        break;
                    case "tpl":
                        result.Add(new Instruction(InstructionType.tpl, parts[1]));
                        break;
                    case "inc":
                        result.Add(new Instruction(InstructionType.inc, parts[1]));
                        break;
                    case "jmp":
                        result.Add(new Instruction(InstructionType.jmp, Offset: int.Parse(parts[1])));
                        break;
                    case "jie":
                        result.Add(new Instruction(InstructionType.jie, parts[1].TrimEnd(','), int.Parse(parts[2])));
                        break;
                    case "jio":
                        result.Add(new Instruction(InstructionType.jio, parts[1].TrimEnd(','), int.Parse(parts[2])));
                        break;
                }
            }
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day23.txt", "Day 23 part 1", 23)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var program = ParseInput(filename);
        var registers = new Dictionary<string, long>
        {
            { "a", 0 },
            { "b", 0 }
        };

        ExecuteProgram(program, registers);
        return new SolutionResult($"A: {registers["a"]}, B: {registers["b"]}");
    }

    [Solveable("2015/Puzzles/Day23.txt", "Day 23 part 2", 23)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var program = ParseInput(filename);
        var registers = new Dictionary<string, long>
        {
            { "a", 1 }, // part 2 starts with register a set to 1
            { "b", 0 }
        };

        ExecuteProgram(program, registers);

        return new SolutionResult($"A: {registers["a"]}, B: {registers["b"]}");
    }

    private static void ExecuteProgram(List<Instruction> program, Dictionary<string, long> registers)
    {
        var ip = 0;
        while (ip >= 0 && ip < program.Count)
        {
            var instruction = program[ip];
            switch (instruction.Type)
            {
                case InstructionType.hlf:
                {
                    registers[instruction.Register] /= 2;
                    ip++;
                    break;
                }
                case InstructionType.tpl:
                {
                    registers[instruction.Register] *= 3;
                    ip++;
                    break;
                }
                case InstructionType.inc:
                {
                    registers[instruction.Register] += 1;
                    ip++;
                    break;
                }
                case InstructionType.jmp:
                {
                    ip += instruction.Offset;
                    break;
                }
                case InstructionType.jie:
                {
                    var value = registers[instruction.Register];
                    if (long.IsEvenInteger(value))
                    {
                        ip += instruction.Offset;
                    }
                    else
                    {
                        ip++;
                    }
                    break;
                }
                case InstructionType.jio:
                {
                    var value = registers[instruction.Register];
                    if (value == 1)
                    {
                        ip += instruction.Offset;
                    }
                    else
                    {
                        ip++;
                    }
                    break;
                }
                default:
                {
                    ip++;
                    break;
                }
            }
        }
    }

    public record Instruction(InstructionType Type, string Register = "", int Offset = 0);

    public enum InstructionType
    {
        hlf,
        tpl,
        inc,
        jmp,
        jie,
        jio
    }
}