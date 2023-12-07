namespace AoC2017;
public class Day5
{
    public static int[] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(_ => int.Parse(_)).ToArray();
    }

    [Solveable("2017/Puzzles/Day5.txt", "Day 5 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        return new SolutionResult(RunProgram(instructions).ToString());
    }

    [Solveable("2017/Puzzles/Day5.txt", "Day 5 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        return new SolutionResult(RunProgram(instructions, true).ToString());
    }

    private static int RunProgram(int[] instructions, bool part2 = false) 
    {
        var steps = 0;
        int currentInstruction = 0;
        while (currentInstruction < instructions.Length)
        {
            var instr = instructions[currentInstruction];
            instructions[currentInstruction] = instr + 1;
            if(part2 && instr >= 3)
            {
                instructions[currentInstruction] = instr - 1;
            }
            currentInstruction += instr;
            steps++;
        }
        return steps;
    }

}