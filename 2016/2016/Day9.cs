namespace AoC2016;

public class Day9
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    private static long GetDecompressedLength(string input, bool recursive = false)
    {
        long length = 0;
        int i = 0;
        
        while (i < input.Length)
        {
            if (input[i] == '(')
            {
                // Find the closing parenthesis
                int closingParen = input.IndexOf(')', i);
                var marker = input.Substring(i + 1, closingParen - i - 1);
                var parts = marker.Split('x');
                int charCount = int.Parse(parts[0]);
                int repeatCount = int.Parse(parts[1]);
                
                // Move past the marker
                i = closingParen + 1;
                
                // Get the data to repeat
                var dataToRepeat = input.Substring(i, charCount);
                
                if (recursive)
                {
                    // In part 2, recursively decompress the data section
                    length += GetDecompressedLength(dataToRepeat, true) * repeatCount;
                }
                else
                {
                    // In part 1, just count the repeated data length
                    length += dataToRepeat.Length * repeatCount;
                }
                
                // Move past the data section
                i += charCount;
            }
            else
            {
                // Regular character
                length++;
                i++;
            }
        }
        
        return length;
    }

    [Solveable("2016/Puzzles/Day9.txt", "Day 9 part 1", 9)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var inputs = ParseInput(filename);
        long totalLength = 0;
        
        foreach (var input in inputs)
        {
            var decompressedLength = GetDecompressedLength(input, false);
            totalLength += decompressedLength;
        }
        
        return new SolutionResult(totalLength.ToString());
    }

    [Solveable("2016/Puzzles/Day9.txt", "Day 9 part 2", 9)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var inputs = ParseInput(filename);
        long totalLength = 0;
        
        foreach (var input in inputs)
        {
            var decompressedLength = GetDecompressedLength(input, true);
            totalLength += decompressedLength;
        }
        
        return new SolutionResult(totalLength.ToString());
    }


}