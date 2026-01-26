namespace AoC2015;

public class Day25
{
    public static (int row, int column) ParseInput(string filename)
    {
        var text = File.ReadAllText(filename);
        var matches = Regex.Matches(text, "\\d+");
        if (matches.Count == 2)
        {
            var row = int.Parse(matches[0].Value);
            var column = int.Parse(matches[1].Value);
            return (row, column);
        }

        return (0, 0);
    }

    [Solveable("2015/Puzzles/Day25.txt", "Day 25 part 1", 25)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (row, column) = ParseInput(filename);   
        var startValue = 20151125L;
        // compute sequence index for position (row, column)
        // n = (r+c-2)*(r+c-1)/2 + c
        var r = row;
        var c = column;
        var n = (long)(r + c - 2) * (r + c - 1) / 2 + c;

        const long multiplier = 252533L;
        const long mod = 33554393L;

        // value at position n is startValue * multiplier^(n-1) mod mod
        var power = ModPow(multiplier, n - 1, mod);
        var code = (startValue * power) % mod;

        return new SolutionResult(code.ToString());
    }

    private static long ModPow(long value, long exp, long mod)
    {
        long result = 1 % mod;
        long baseVal = value % mod;
        while (exp > 0)
        {
            if ((exp & 1) == 1)
            {
                result = (result * baseVal) % mod;
            }
            baseVal = (baseVal * baseVal) % mod;
            exp >>= 1;
        }
        return result;
    }

}