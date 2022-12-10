namespace Advent2021;
public class Day24
{
    public static IEnumerable<(Instruction instruction, List<string> parameters)> ParseInstructions(string filename)
    {
        foreach (var line in File.ReadAllLines(filename))
        {
            var input = line.Split(' ');
            var instr = Enum.Parse<Instruction>(input[0]);
            yield return (instr, input.Skip(1).ToList());
        }
    }

    public static ALU RunProgram(List<(Instruction inst, List<string> p)> instructions, List<long> list)
    {
        var alu = new ALU();
        alu.Execute(instructions, list);
        return alu;
    }

    public static long FindLargestModelNumber(string filename, string input)
    {
        var instructions = ParseInstructions(filename).ToList();
        var instructionList = new List<List<(Instruction inst, List<string>)>>();
        var current = new List<(Instruction inst, List<string>)>();
        current.Add(instructions.First());
        //TODO: Get it working, got the stars by using this:
        //Adapted from the explanation and example here: https://github.com/dphilipson/advent-of-code-2021/blob/master/src/days/day24.rs
        //        PUSH input[0] +6
        //PUSH input[1] +7
        //PUSH input[2] +10
        //PUSH input[3] +2
        //POP input[4] == popped_value - 7(input3)
        //PUSH input[5] +8
        //PUSH input[6] +1
        //POP input[7] == popped_value - 5(input6)
        //PUSH input[8] +5
        //POP input[9] == popped_value - 3(input8)
        //POP input[10] == popped_value(input5)
        //POP input[11] == popped_value - 5(input2)
        //POP input[12] == popped_value - 9(input1)
        //POP input[13] == popped(input0)
        //Part1: 39494995791953
        //Part2: 13161151139617


        //input4 == input3 - 5
        //input7 == input6 - 4
        //input9 == input8 + 2
        //input10 == input5 + 8
        //input11 == input2 + 5
        //input12 == input1 - 2
        //input13 = input0 + 6

        //39494195799979


        foreach (var instruction in instructions.Skip(1))
        {

        }
        return 0;
    }
}

public class ALU
{
    public ALU()
    {
        Registers.Add("x", 0);
        Registers.Add("y", 0);
        Registers.Add("z", 0);
        Registers.Add("w", 0);
    }

    public void Execute(List<(Instruction inst, List<string> p)> instructions, List<long> list)
    {
        var executedInputs = 0;
        foreach (var instr in instructions)
        {
            switch (instr.inst)
            {
                case Instruction.inp:
                    SetRegister(instr.p.First(), list.ElementAt(executedInputs));
                    executedInputs++;
                    break;
                case Instruction.add:
                    AddAndSet(instr.p);
                    break;
                case Instruction.mul:
                    MultiplyAndSet(instr.p);
                    break;
                case Instruction.div:
                    DivideAndSet(instr.p);
                    break;
                case Instruction.mod:
                    ModAndSet(instr.p);
                    break;
                case Instruction.eql:
                    EqualAndSet(instr.p);
                    break;
                default:
                    throw new NotImplementedException();

            }
        }
    }

    private void EqualAndSet(List<string> p)
    {
        var val1 = Registers[p.First()];
        var isInt = int.TryParse(p.Last(), out var val2);
        Registers[p.First()] = isInt ? (val1 == val2 ? 1 : 0) : (val1 == Registers[p.Last()] ? 1 : 0);
    }

    private void ModAndSet(List<string> p)
    {
        var val1 = Registers[p.First()];
        var isInt = long.TryParse(p.Last(), out var val2);
        var res = val1 % (isInt ? val2 : Registers[p.Last()]);
        Registers[p.First()] = res;
    }

    private void DivideAndSet(List<string> p)
    {
        var val1 = Registers[p.First()];
        var isInt = long.TryParse(p.Last(), out var val2);
        var quot = val1 / (isInt ? val2 : Registers[p.Last()]);
        Registers[p.First()] = quot;
    }

    private void MultiplyAndSet(List<string> p)
    {
        var val1 = Registers[p.First()];
        var isInt = long.TryParse(p.Last(), out var val2);
        var prod = val1 * (isInt ? val2 : Registers[p.Last()]);
        Registers[p.First()] = prod;
    }

    private void AddAndSet(List<string> p)
    {
        var val1 = Registers[p.First()];
        var isInt = long.TryParse(p.Last(), out var val2);
        var sum = val1 + (isInt ? val2 : Registers[p.Last()]);
        Registers[p.First()] = sum;
    }

    private void SetRegister(string register, long value)
    {
        Registers[register] = value;
    }

    public Dictionary<string, long> Registers { get; private set; } = new();
}

public enum Instruction
{
    inp,
    add,
    mul,
    div,
    mod,
    eql
}
