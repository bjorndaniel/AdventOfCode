﻿namespace AoC2024;
public class Day17
{
    public static (Computer computer, List<int> program) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        ulong regA = 0L;
        ulong regB = 0L;
        ulong regC = 0L;
        var program = new List<int>();
        for (int i = 0; i < lines.Length; i++)
        {
            if (i < 3)
            {
                var vals = lines[i].Split(":");
                if (i == 0)
                {
                    regA = ulong.Parse(vals[1]);
                }
                if (i == 1)
                {
                    regB = ulong.Parse(vals[1]);
                }
                if (i == 2)
                {
                    regC = ulong.Parse(vals[1]);
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
        var validA = new List<ulong> { 0 };
        var reversedProgram = program.ToArray().Reverse().ToList();
        for (int i = 0; i < reversedProgram.Count; i++)
        {
            var newAs = new List<ulong>();
            foreach (var a in validA)
            {
                for (ulong j = 0; j < 8; j++)
                {
                    ulong newA = (a << 3) + j;
                    var output = RunProgram(new Computer(newA, 0, 0), program);
                    if ((int)output[0] == reversedProgram[i])
                    {
                        newAs.Add(newA);
                    }
                }
            }
            validA = newAs.Distinct().ToList();
        }

        return new SolutionResult(validA.Min().ToString());
    }

    public static List<ulong> RunProgram(Computer computer, List<int> program)
    {
        var outputs = new List<ulong>();
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

public class Computer(ulong a, ulong b, ulong c)
{
    public ulong A { get; set; } = a;
    public ulong B { get; set; } = b;
    public ulong C { get; set; } = c;

    public (ulong? result, int? jump) RunInstruction(Instruction instruction, Operand operand)
    {
        switch (instruction)
        {
            case Instruction.Adv:
                var numerator = A;
                var denominator = Math.Pow(2, GetOperand(operand));
                var result = numerator / denominator;
                A = (ulong)result;
                break;
            case Instruction.Bxl:
                var op = (ulong)operand;
                B = B ^ op;
                break;
            case Instruction.Bst:
                B = (GetOperand(operand)) % 8;
                break;
            case Instruction.Jnz:
                if (A != 0)
                {
                    return (null, (int)operand);
                }
                break;
            case Instruction.Bxc:
                B = B ^ C;
                break;
            case Instruction.Out:
                var value = (GetOperand(operand)) % 8;
                return (value, null);
            case Instruction.Bdv:
                var numeratorB = A;
                var denominatorB = Math.Pow(2, GetOperand(operand));
                var resultB = numeratorB / denominatorB;
                B = (ulong)resultB;
                break;
            case Instruction.Cdv:
                var numeratorC = A;
                var denominatorC = Math.Pow(2, GetOperand(operand));
                var resultC = numeratorC / denominatorC;
                C = (ulong)resultC;
                break;
        }
        return (null, null);

        ulong GetOperand(Operand operand)
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