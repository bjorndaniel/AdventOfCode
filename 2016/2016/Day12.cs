namespace AoC2016;

public class Day12
{
    public static List<AssembunnyInstr> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var results = new List<AssembunnyInstr>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            switch (parts[0])
            {
                case "cpy":
                    results.Add(new AssembunnyInstr(OpCode.Cpy, parts[1], parts.Length > 2 ? parts[2] : null));
                    break;
                case "inc":
                    results.Add(new AssembunnyInstr(OpCode.Inc, parts[1], parts.Length > 2 ? parts[2] : null));
                    break;
                case "dec":
                    results.Add(new AssembunnyInstr(OpCode.Dec, parts[1], parts.Length > 2 ? parts[2] : null));
                    break;
                case "jnz":
                    results.Add(new AssembunnyInstr(OpCode.Jnz, parts[1], parts.Length > 2 ? parts[2] : null));
                    break;
                default:
                    throw new InvalidOperationException($"Unknown instruction: {line}");
            }
        }
        return results;
    }

    [Solveable("2016/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
       
        var rtg = RunCode(instructions);
        return new SolutionResult(rtg.GetRegisterValue("a").ToString());
    }

    [Solveable("2016/Puzzles/Day12.txt", "Day 12 part 2", 12)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        instructions.Insert(0, new AssembunnyInstr(OpCode.Cpy, "1", "c")); // set register c to 1
        var rtg = RunCode(instructions);
        return new SolutionResult(rtg.GetRegisterValue("a").ToString());
    }

    private static RTG RunCode(List<AssembunnyInstr> instructions)
    {
        var rtg = new RTG();

        int pc = 0;
        while (pc >= 0 && pc < instructions.Count)
        {
            var instr = instructions[pc];
            switch (instr.OpCode)
            {
                case OpCode.Cpy:
                    // copy value (literal or register) to register
                    if (instr.Arg2 is null)
                    {
                        pc++;
                        break;
                    }

                    if (int.TryParse(instr.Arg1, out var lit))
                    {
                        rtg.SetRegisterValue(instr.Arg2, lit);
                    }
                    else
                    {
                        var val = rtg.GetRegisterValue(instr.Arg1);
                        rtg.SetRegisterValue(instr.Arg2, val);
                    }

                    pc++;
                    break;

                case OpCode.Inc:
                    {
                        var v = rtg.GetRegisterValue(instr.Arg1);
                        rtg.SetRegisterValue(instr.Arg1, v + 1);
                        pc++;
                        break;
                    }

                case OpCode.Dec:
                    {
                        var v = rtg.GetRegisterValue(instr.Arg1);
                        rtg.SetRegisterValue(instr.Arg1, v - 1);
                        pc++;
                        break;
                    }

                case OpCode.Jnz:
                    {
                        var check = 0;
                        if (int.TryParse(instr.Arg1, out var litCheck))
                        {
                            check = litCheck;
                        }
                        else
                        {
                            check = rtg.GetRegisterValue(instr.Arg1);
                        }

                        if (check != 0)
                        {
                            if (instr.Arg2 is null)
                            {
                                pc++;
                            }
                            else if (int.TryParse(instr.Arg2, out var offset))
                            {
                                pc += offset;
                            }
                            else
                            {
                                var offVal = rtg.GetRegisterValue(instr.Arg2);
                                pc += offVal;
                            }
                        }
                        else
                        {
                            pc++;
                        }

                        break;
                    }

                default:
                    pc++;
                    break;
            }
        }
        return rtg;
    }
}

public record AssembunnyInstr(OpCode OpCode, string Arg1, string? Arg2 = null);

public enum OpCode
{
    Cpy,
    Inc,
    Dec,
    Jnz,
}

public class RTG
{
    private readonly Dictionary<string, int> _registers = new(StringComparer.OrdinalIgnoreCase);

    public int GetRegisterValue(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return 0;
        }

        if (_registers.TryGetValue(name, out var val))
        {
            return val;
        }

        return 0;
    }

    public void SetRegisterValue(string name, int value)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }

        _registers[name] = value;
    }
}