namespace AoC2015;
public class Day11
{
    public static string ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.First();
    }

    [Solveable("2015/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var previousPassword = ParseInput(filename);
        var nextPassword = GenerateNextPassword(previousPassword);
        return new SolutionResult(nextPassword);
    }

    [Solveable("2015/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var previousPassword = ParseInput(filename);
        var nextPassword = GenerateNextPassword(previousPassword);
        nextPassword = GenerateNextPassword(nextPassword);
        return new SolutionResult(nextPassword);
    }

    private static string GenerateNextPassword(string previousPassword)
    {
        var password = previousPassword;
        do
        {
            password = IncrementPassword(password);
        } while (IsValidPassword(password) is false);
        return password;
    }

    // Increments the password string as a base-26 number (a-z)
    private static string IncrementPassword(string password)
    {
        var chars = password.ToCharArray();
        var i = chars.Length - 1;
        while (i >= 0)
        {
            if (chars[i] == 'z')
            {
                chars[i] = 'a';
                i--;
            }
            else
            {
                chars[i]++;
                break;
            }
        }
        return new string(chars);
    }

    // Checks all password rules
    private static bool IsValidPassword(string password)
    {
        // Rule 2: No i, o, l
        if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
        {
            return false; 
        }

        // Rule 1: At least one straight of three increasing letters
        var hasStraight = false;
        for (var i = 0; i < password.Length - 2; i++)
        {
            if (password[i + 1] == password[i] + 1 && password[i + 2] == password[i] + 2)
            {
                hasStraight = true;
                break;
            }
        }
        if (!hasStraight)
        {
            return false; 
        }

        // Rule 3: At least two different, non-overlapping pairs
        var pairCount = 0;
        char? lastPairChar = null;
        for (var i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1] && password[i] != lastPairChar)
            {
                pairCount++;
                lastPairChar = password[i];
                i++; // skip next char to avoid overlap
            }
        }
        return pairCount >= 2;
    }

}