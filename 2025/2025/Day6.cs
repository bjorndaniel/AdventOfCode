namespace AoC2025;

public class Day6
{
    public static (List<string>[] numbers, string[] operands) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        int maxLen = lines.Max(l => l.Length);
        var sharedIndices = new List<int>();
        for (int i = 0; i < maxLen; i++)
        {
            if (lines.All(l => i < l.Length && l[i] == ' '))
            {
                sharedIndices.Add(i);
            }
        }
        var blocks = new List<(int start, int end)>();
        for (int i = 0; i < sharedIndices.Count;)
        {
            int start = sharedIndices[i];
            int end = start;
            while (i + 1 < sharedIndices.Count && sharedIndices[i + 1] == end + 1)
            {
                end = sharedIndices[++i];
            }
            blocks.Add((start, end));
            i++;
        }



        var firstCols = SplitByBlocks(lines.First(), blocks).ToArray();
        var nrOfColumns = firstCols.Length;
        var numberArray = new List<string>[nrOfColumns];
        for (int i = 0; i < nrOfColumns; i++) numberArray[i] = new List<string>();
        var operandArray = new string[nrOfColumns];

        foreach (var line in lines)
        {
            var columns = SplitByBlocks(line, blocks).ToArray();
            var hasOperatorOnly = columns.All(t => t.Trim() == "+" || t.Trim() == "*" || t.Trim() == string.Empty)
                                  && columns.Any(t => t.Trim() == "+" || t.Trim() == "*");
            if (hasOperatorOnly)
            {
                for (int i = 0; i < nrOfColumns; i++)
                {
                    var token = columns[i].Trim();
                    if (token == "+" || token == "*") operandArray[i] = token;
                }
            }
            else
            {
                for (int i = 0; i < nrOfColumns; i++)
                {
                    numberArray[i].Add(columns[i]);
                }
            }
        }
        return (numberArray, operandArray);

        static IEnumerable<string> SplitByBlocks(string line, List<(int start, int end)> blocks)
        {
            int pos = 0;
            foreach (var (start, end) in blocks)
            {
                int safeStart = Math.Min(start, line.Length);
                int len = Math.Max(0, safeStart - pos);
                yield return len > 0 ? line.Substring(pos, len) : string.Empty;
                pos = Math.Min(end + 1, line.Length);
            }
            if (pos <= line.Length)
            {
                var tail = line.Substring(pos);
                yield return tail;
            }
        }
    }

    [Solveable("2025/Puzzles/Day6.txt", "Day 6 part 1", 6)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (numbers, operands) = ParseInput(filename);
        var result = 0L;
        for (int i = 0; i < numbers.Length; i++)
        {
            var values = numbers[i]
                .Select(s => s.Trim())
                .Where(s => s.Length > 0 && long.TryParse(s, out _))
                .Select(s => long.Parse(s))
                .ToList();

            switch (operands[i])
            {
                case "+":
                    result += values.Sum();
                    break;
                case "*":
                    result += values.Aggregate((a, b) => a * b);
                    break;
                default:
                    throw new Exception($"Unknown operand {operands[i]}");
            }
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2025/Puzzles/Day6.txt", "Day 6 part 2", 6)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (numbers, operands) = ParseInput(filename);
        var result = 0L;

        for (int col = operands.Length - 1; col >= 0; col--)
        {
            var columnRows = numbers[col];
            int maxWidth = columnRows.Max(s => s.Length);
            var padded = columnRows.Select(s => s.PadLeft(maxWidth, ' ')).ToList();
            var constructed = new List<long>();
            for (int pos = maxWidth - 1; pos >= 0; pos--)
            {
                var sb = new System.Text.StringBuilder();
                for (int row = 0; row < padded.Count; row++)
                {
                    var ch = padded[row][pos];
                    if (ch != ' ') sb.Append(ch);
                }
                if (sb.Length > 0)
                {
                    constructed.Add(long.Parse(sb.ToString()));
                }
            }

            switch (operands[col])
            {
                case "+":
                    result += constructed.Sum();
                    break;
                case "*":
                    result += constructed.Aggregate((a, b) => a * b);
                    break;
                default:
                    throw new Exception($"Unknown operand {operands[col]}");
            }
        }

        return new SolutionResult(result.ToString());
    }


}