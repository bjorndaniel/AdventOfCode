namespace AoC2024;
public class Day17
{
    public static (Computer computer, List<int> program) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var regA = 0L;
        var regB = 0L;
        var regC = 0L;
        var program = new List<int>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (i < 3)
            {
                var vals = lines[i].Split(":");
                if (i == 0)
                {
                    regA = long.Parse(vals[1]);
                }
                if (i == 1)
                {
                    regB = long.Parse(vals[1]);
                }
                if (i == 2)
                {
                    regC = long.Parse(vals[1]);
                }
            }
            if (i == 4)
            {
                var instr = lines[i].Split(":");
                program = instr[1].Split(",").Select(_ => int.Parse(_.Trim())).ToList();
            }
        }
        return (new Computer(regA, regB, regC), program);
    }

    [Solveable("2024/Puzzles/Day17.txt", "Day 17 part 1", 17)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (computer, program) = ParseInput(filename);
        var outputs = new List<long>();
        var pointer = 0;
        while(pointer < program.Count)
        {
            var instr = (Instruction)program[pointer];
            var operand = (ComboOperand)program[pointer + 1];
            var (output, jump) = computer.RunInstruction(instr, operand);
            if (output.HasValue)
            {
                outputs.Add(output.Value);
            }
            if (jump.HasValue)
            {
                pointer = jump.Value;
            }
            else
            {
                jump += 2;
            }
        }
        
        return new SolutionResult(outputs.Select(_ => _.ToString()).Aggregate((a, b) => $"{a},{b}"));
    }

    [Solveable("2024/Puzzles/Day17.txt", "Day 17 part 2", 17)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }


}

public class Computer(long a, long b, long c)
{
    public long A { get; set; } = a;
    public long B { get; set; } = b;
    public long C { get; set; } = c;

    public (long? result, int? jump) RunInstruction(Instruction instruction, ComboOperand operand)
    {
        switch (instruction)
        {
            case Instruction.Adv:
                var numerator = A;
                var denominator = GetOperand(operand);
                var result = numerator / denominator;
                A = (int)result;
                break;
            case Instruction.Bxl:
                var op = (int)operand;
                B = B ^ op;
                break;
            case Instruction.Bst:
                B = ((int)operand) % 8;
                break;
            case Instruction.Jnz:
                if(A != 0)
                {
                    return (null, (int)operand);    
                }
                break;
            case Instruction.Bxc:
                B = B ^ C;
                break;
            case Instruction.Out:
                var value = ((int)operand) % 8;
                return (value, null);
            case Instruction.Bdv:
                var numerator = A;
                var denominator = GetOperand(operand);
                var result = numerator / denominator;
                B = (int)result;
                break;
            case Instruction.Cdv:
                var numerator = A;
                var denominator = GetOperand(operand);
                var result = numerator / denominator;
                C = (int)result;
                break;
        }

        long GetOperand(ComboOperand operand)
        {
            return operand switch
            {
                ComboOperand.Literal0 => 0,
                ComboOperand.Literal1 => 1,
                ComboOperand.Literal2 => 2,
                ComboOperand.Literal3 => 3,
                ComboOperand.ValueA => A,
                ComboOperand.ValueB => B,
                ComboOperand.ValueC => C,
                _ => throw new Exception("Invalid operand")
            };
        }

    }
}

public enum ComboOperand
{
    Literal0,
    Literal1,
    Literal2,
    Literal3,
    ValueA,
    ValueB,
    ValueC,
    Reserved
}

public enum Instruction
{
    Adv,
    Bxl,
    Bst,
    Jnz,
    Bxc,
    Out,
    Bdv,
    Cdv
}