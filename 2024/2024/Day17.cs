namespace AoC2024;
public class Day17
{
    private static int[] _code = [];

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

    [Solveable("2024/Puzzles/Day17.txt", "Day 17 part 1", 17, true)]
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
        //var result = 0L;
        //_code = program.ToArray();
        //result = FindResult([0], 0, program, printer);
        var backTrack = program.ToArray().Reverse().ToList();
        var validA = new List<long>{0};
        for (int i = 0;i< backTrack.Count; i ++)
        {
            var newAs = new List<long>();
            foreach(var requiredA in validA)
            {
                for (long j = 0; j < 9; j++)
                {
                    var shifted = requiredA << 3;
                    shifted += j;
                    //(long newA, long output) = RunPartial(new Computer((long)shifted, 0, 0), program);
                    var res = RunProgram(new Computer(shifted, 0, 0), program);
                    if (res[0] == backTrack[i])
                    {
                        newAs.Add(shifted);
                    }
                }

            }
            validA = newAs;

        }
        //foreach(var a in validA)
        //{
        //    var result = RunProgram(new Computer((long)a, 0, 0), program);
        //    printer.Print(result.Select(_ => _.ToString()).Aggregate((a, b) => $"{a},{b}"));
        //    printer.Flush();
        //}
        return new SolutionResult(validA.Min().ToString());
    }

    //public static long FindResult(List<long> validA, int position, List<int> program, IPrinter printer)
    //{
    //    //if (position >= _code.Length)
    //    //{
    //    //    return validA.Min();
    //    //}
    //    //var outputPosition = _code.Length - position - 1;
    //    //var requiredOutput = _code[outputPosition];
    //    //var newAs = new List<long>();
    //    //foreach(var requiredA in validA)
    //    //{
            
    //    //    //printer.Print($"Testing position {outputPosition} with requiredA {requiredA} and requiredOutput {requiredOutput}");
    //    //    //printer.Flush();
    //    //    //var counter = shifted > 0 ? (shifted << 10) : 1024;
    //    //    for (int i = 0; i < 8; i++)
    //    //    {
    //    //        var shifted = requiredA << 3;
    //    //        shifted += i;
    //    //        //var testA = shifted + i;
    //    //        (long newA, long output) = RunPartial(new Computer(shifted, 0, 0), program);
    //    //        if (newA == requiredA && output == requiredOutput)
    //    //        {
    //    //            newAs.Add(shifted);
    //    //            //printer.Print($"Found result {output} for {outputPosition} with testA {testA} and newA {newA}");
    //    //            //printer.Flush();
    //    //            var result = FindResult(newAs, position + 1, program, printer);
    //    //            if (result >= 0)
    //    //            {
    //    //                //printer.Print($"Found result {result} at with testA {testA}");
    //    //                //printer.Flush();
    //    //                return result;
    //    //            }
    //    //        }
    //    //    }
    //    //}
        
    //    //return -1;
    //}

    public static (long a, long output) RunPartial(Computer computer, List<int> program)
    {
        var returnOutPut = 0L;
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
                returnOutPut  = output.Value;
                pointer += 2;
            }
            else if (jump.HasValue)
            {
                pointer = jump.Value;
                return (computer.A, returnOutPut);
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
                B = Helpers.XorWithPadding(B, op);
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
                B = Helpers.XorWithPadding(B, C);
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