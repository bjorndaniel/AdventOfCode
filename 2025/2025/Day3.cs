namespace AoC2025;

public class Day3
{
    public static List<BatteryBank> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var banks = new List<BatteryBank>();    
        foreach (var line in lines)
        {
            var batteries = line.Select(_ => long.Parse(_.ToString())).ToList();
            banks.Add(new BatteryBank(batteries));
        }
        return banks;
    }

    [Solveable("2025/Puzzles/Day3.txt", "Day 3 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var banks = ParseInput(filename);
        var result = 0L;
        foreach(var bank in banks)
        {
            var largestTwoDigitNumber = FindLargestNDigitNumber(bank, 2);
            result += largestTwoDigitNumber;
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2025/Puzzles/Day3.txt", "Day 3 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var banks = ParseInput(filename);
        var result = 0L;
        
        foreach(var bank in banks)
        {
            var largest12DigitNumber = FindLargestNDigitNumber(bank, 12);
            result += largest12DigitNumber;
        }

        return new SolutionResult(result.ToString());
    }

    private static long FindLargestNDigitNumber(BatteryBank bank, int n)
    {
        var batteries = bank.Batteries;
        if (batteries.Count < n)
        {
            return 0;
        }
        var selectedIndices = new List<int>();
        var startIndex = 0;
        for (int position = 0; position < n; position++)
        {
            var remainingPositions = n - position - 1;
            var maxDigit = -1L;
            var maxIndex = -1;

            for (int i = startIndex; i <= batteries.Count - remainingPositions - 1; i++)
            {
                if (batteries[i] > maxDigit)
                {
                    maxDigit = batteries[i];
                    maxIndex = i;
                }
            }

            selectedIndices.Add(maxIndex);
            startIndex = maxIndex + 1;
        }

        var result = 0L;
        foreach (var index in selectedIndices)
        {
            result = result * 10 + batteries[index];
        }
        return result;
    }

}

public record BatteryBank(List<long> Batteries);
