namespace AoC2024;
public class Day5
{
    public static (List<(int first, int second)> rules, List<List<int>> instructions) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var instructions = new List<List<int>>();
        var rules = new List<(int first, int second)>();
        foreach (var line in lines)
        {
            if (line.Contains("|"))
            {
                var ruleParts = line.Split("|");
                rules.Add((int.Parse(ruleParts[0]), int.Parse(ruleParts[1])));
            }
            else if (line.Contains(","))
            {
                var instructionParts = line.Split(",");
                instructions.Add(instructionParts.Select(_ => int.Parse(_)).ToList());
            }
        }
        return (rules, instructions);
    }

    [Solveable("2024/Puzzles/Day5.txt", "Day 5 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (rules, instructions) = ParseInput(filename);
        var result = 0;
        foreach (var instruction in instructions)
        {
            var middleIndex = instruction.Count / 2;
            var number = instruction[middleIndex];
            var isValid = true;
            foreach (var rule in rules.Where(_ => instruction.IndexOf(_.first) != -1 && instruction.IndexOf(_.second) != -1))
            {
                var firstIndex = instruction.IndexOf(rule.first);
                var secondIndex = instruction.IndexOf(rule.second);
                
                if (firstIndex > secondIndex)
                {
                    isValid = false;
                }
            }
            if (isValid)
            {
                result += number;
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day5.txt", "Day 5 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (rules, instructions) = ParseInput(filename);
        var result = 0;
        var invalidInstructions = new List<List<int>>();
        foreach (var instruction in instructions)
        {
            if (IsOrdered(instruction).isOrdered is false)
            {
                invalidInstructions.Add(instruction);
            }
        }

        foreach (var instruction in invalidInstructions)
        {
            var (isOrdered, firstIndex, secondIndex) = IsOrdered(instruction);
            while (isOrdered is false)
            {
                var temp = instruction[firstIndex];
                instruction[firstIndex] = instruction[secondIndex];
                instruction[secondIndex] = temp;
                (isOrdered, firstIndex, secondIndex) = IsOrdered(instruction);
            }
        }

        foreach(var instruction in invalidInstructions)
        {
            var middleIndex = instruction.Count / 2;
            var number = instruction[middleIndex];
            result += number;
        }

        return new SolutionResult(result.ToString());

        (bool isOrdered, int firstIndex, int secondIndex) IsOrdered(List<int> instruction)
        {
            foreach (var rule in rules)
            {
                var firstIndex = instruction.IndexOf(rule.first);
                var secondIndex = instruction.IndexOf(rule.second);
                if (firstIndex == -1 || secondIndex == -1)
                {
                    continue;
                }
                if (firstIndex > secondIndex)
                {
                    return (false, firstIndex, secondIndex);
                }
            }
            return (true, 0, 0);
        }
    }


}
