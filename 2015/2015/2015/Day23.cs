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
        for (int i = 0; i < registers.Count; i++)
        {
            var instruction = program[i];
            switch (instruction.Type)
            {
                case InstructionType.hlf:
                    registers[instruction.Register] /= 2;
                    break;
                case InstructionType.tpl:
                    registers[instruction.Register] *= 3;
                    break;
                case InstructionType.inc:
                    registers[instruction.Register] += 1;
                    break;
                case InstructionType.jmp:
                    i += instruction.Offset;
                    break;
                case InstructionType.jie:
                    var jieValue = registers[instruction.Register];
                    if (long.IsEvenInteger(jieValue))
                    {
                        i += instruction.Offset - 1;
                    }
                    break;
                case InstructionType.jio:
                    var jioValue = registers[instruction.Register];
                    if (jioValue == 1)
                    {
                        i += instruction.Offset - 1;
                    }
                    break;
            }

        }
        printer.Print($"Register a: {registers["a"]}");
        printer.Flush();
        return new SolutionResult($"A: {registers["a"]}, B: {registers["b"]}");
    }

    [Solveable("2015/Puzzles/Day23.txt", "Day 23 part 2", 23)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
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