namespace AoC2024;
public class Day7
{
    public static List<Equation> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Equation>();
        foreach (var line in lines)
        {
            var parts = line.Split(":");
            var resultValue = long.Parse(parts[0]);
            var operands = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            result.Add(new Equation(resultValue, operands));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day7.txt", "Day 7 part 1", 7)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var equations = ParseInput(filename);
        var result = 0L;
        foreach (var equation in equations)
        {
            if (CombineOperandsRecursive(equation.Operands, 0, equation.Operands[0], equation.Result))
            {
                result += equation.Result;
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day7.txt", "Day 7 part 2", 7)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var equations = ParseInput(filename);
        var result = 0L;
        foreach (var equation in equations)
        {
            if (CombineOperandsRecursive(equation.Operands, 0, equation.Operands[0], equation.Result, true))
            {
                result += equation.Result;
            }
        }
        return new SolutionResult(result.ToString());
    }

    private static bool CombineOperandsRecursive(List<long> operands, int index, long current, long result, bool part2 = false)
    {
        if (index == operands.Count - 1)
        {
            return current == result;
        }

        long nextOperand = operands[index + 1];

        // Try addition
        if (CombineOperandsRecursive(operands, index + 1, current + nextOperand, result, part2))
        {
            return true;
        }

        // Try multiplication
        if (CombineOperandsRecursive(operands, index + 1, current * nextOperand, result, part2))
        {
            return true;
        }

        if(part2)
        {
            if(index + 1 < operands.Count)
            {
                if (CombineOperandsRecursive(operands, index + 1, long.Parse(current.ToString() + operands[index + 1].ToString()) , result, part2))
                {
                    return true;
                }
            
            }
        }

        return false;
    }
}

public record Equation(long Result, List<long> Operands);