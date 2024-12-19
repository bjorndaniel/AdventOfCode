namespace AoC2024;
public class Day17
{
    private static int[] _code;

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
        var outputs = RunProgram(computer, program);
        return new SolutionResult(outputs.Select(_ => _.ToString()).Aggregate((a, b) => $"{a},{b}"));
    }

    [Solveable("2024/Puzzles/Day17.txt", "Day 17 part 2", 17)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (computer, program) = ParseInput(filename);
        var result = 0L;
        _code = program.ToArray();
        result = FindResult(0, 0, program);
        return new SolutionResult(result.ToString());
    }

  

    public static long FindResult(long requiredA, int position, List<int> program)
    {
        if (position >= _code.Length)
        {
            return requiredA;
        }
        var requiredOutput = _code[_code.Length - position - 1];
        var shifted = requiredA << 3;
        for (int i = 0; i < (1 << 20); i++)
        {
            var testA = XorWithPadding(shifted, i); 

            (long newA, long output) = RunPartial(new Computer(testA, 0,0), program);
            if (newA == requiredA && output == requiredOutput)
            {
                var result = FindResult(testA, position + 1, program);
                if (result > 0)
                {
                    return result;
                }
            }
        }
        return -1;
    }

    public static (long b, long output) RunPartial(Computer computer, List<int> program)
    {
        var outputs = new List<long>();
        var pointer = 0;
        while (pointer < program.Count)
        {
            var instr = (Instruction)program[pointer];
            var operand = (Operand)program[pointer + 1];
            var (output, jump) = computer.RunInstruction(instr, operand);
            if (output.HasValue)
            {
                outputs.Add(output.Value);
                pointer += 2;
                return (computer.A, output.Value);
            }
            else if (jump.HasValue)
            {
                //return (computer.A, outputs.Last());
                pointer = jump.Value;
            }
            else
            {
                pointer += 2;
            }
        }
        return (0, 0);
    }

    public static List<long> RunProgram(Computer computer, List<int> program)
    {
        var outputs = new List<long>();
        var pointer = 0;
        while (pointer < program.Count)
        {
            var instr = (Instruction)program[pointer];
            var operand = (Operand)program[pointer + 1];
            var (output, jump) = computer.RunInstruction(instr, operand);
            if (output.HasValue)
            {
                outputs.Add(output.Value);
                pointer += 2;
            }
            else if (jump.HasValue)
            {
                pointer = jump.Value;
            }
            else
            {
                pointer += 2;
            }
        }
        return outputs;
    }

    public static long XorWithPadding(long num1, long num2)
    {
        var str1 = Convert.ToString(num1, 2);
        var str2 = Convert.ToString(num2, 2);
        var maxLength = Math.Max(str1.Length, str2.Length);
        str1 = str1.PadLeft(maxLength, '0');
        str2 = str2.PadLeft(maxLength, '0');
        var result = new char[maxLength];
        for (int i = 0; i < str2.Length; i++)
        {
            result[i] = (str1[i] == str2[i]) ? '0' : '1';
        }
        var num = new string(result);
        return Convert.ToInt64(num, 2);
    }
}

public class Computer(long a, long b, long c)
{
    public long A { get; set; } = a;
    public long B { get; set; } = b;
    public long C { get; set; } = c;

    public (long? result, int? jump) RunInstruction(Instruction instruction, Operand operand)
    {
        switch (instruction)
        {
            case Instruction.Adv:
                var numerator = A;
                var denominator = Math.Pow(2, (double)GetOperand(operand));
                var result = numerator / denominator;
                A = (int)result;
                break;
            case Instruction.Bxl:
                var op = (int)operand;
                B = Day17.XorWithPadding(B, op);
                break;
            case Instruction.Bst:
                B = ((int)GetOperand(operand)) % 8;
                break;
            case Instruction.Jnz:
                if (A != 0)
                {
                    return (null, (int)operand);
                }
                break;
            case Instruction.Bxc:
                B = Day17.XorWithPadding(B, C);
                break;
            case Instruction.Out:
                var value = (GetOperand(operand)) % 8;
                return (value, null);
            case Instruction.Bdv:
                var numeratorB = A;
                var denominatorB = Math.Pow(2, (double)GetOperand(operand));
                var resultB = numeratorB / denominatorB;
                B = (int)resultB;
                break;
            case Instruction.Cdv:
                var numeratorC = A;
                var denominatorC = Math.Pow(2, (double)GetOperand(operand));
                var resultC = numeratorC / denominatorC;
                C = (int)resultC;
                break;
        }
        return (null, null);

        long GetOperand(Operand operand)
        {
            return operand switch
            {
                Operand.Literal0 => 0,
                Operand.Literal1 => 1,
                Operand.Literal2 => 2,
                Operand.Literal3 => 3,
                Operand.ValueA => A,
                Operand.ValueB => B,
                Operand.ValueC => C,
                _ => throw new Exception("Invalid operand")
            };
        }
    }
}



public enum Operand
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