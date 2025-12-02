namespace AoC2016;

public class Day7
{
    public static List<string> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.ToList();
    }

    private static bool HasAbba(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length < 4)
        {
            return false;
        }
        for (int i = 0; i <= s.Length - 4; i++)
        {
            char a = s[i], b = s[i + 1], c = s[i + 2], d = s[i + 3];
            if (a != b && a == d && b == c) return true;
        }
        return false;
    }

    private static (List<string> supernets, List<string> hypernets) SplitSegments(string line)
    {
        var supernets = new List<string>();
        var hypernets = new List<string>();
        var buffer = new System.Text.StringBuilder();
        var inBracket = false;
        foreach (char ch in line)
        {
            if (ch == '[')
            {
                if (buffer.Length > 0)
                {
                    supernets.Add(buffer.ToString());
                    buffer.Clear();
                }
                inBracket = true;
                continue;
            }
            if (ch == ']')
            {
                if (buffer.Length > 0)
                {
                    hypernets.Add(buffer.ToString());
                    buffer.Clear();
                }
                inBracket = false;
                continue;
            }
            buffer.Append(ch);
        }
        if (buffer.Length > 0)
        {
            if (inBracket)
            {
                hypernets.Add(buffer.ToString());
            }
            else
            {
                supernets.Add(buffer.ToString());
            }
        }
        return (supernets, hypernets);
    }

    [Solveable("2016/Puzzles/Day7.txt", "Day 7 part 1", 7)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var ips = ParseInput(filename);
        var count = 0;
        foreach (var line in ips)
        {
            var (supernets, hypernets) = SplitSegments(line);

            // If any hypernet contains an ABBA, the IP does NOT support TLS.
            bool hyperHasAbba = hypernets.Any(HasAbba);
            if (hyperHasAbba)
            {
                continue;
            }

            // If any supernet contains an ABBA, the IP supports TLS.
            var superHasAbba = supernets.Any(HasAbba);
            if (superHasAbba)
            {
                count++;
            }
        }
        return new SolutionResult(count.ToString());
    }

    [Solveable("2016/Puzzles/Day7.txt", "Day 7 part 2", 7)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = ParseInput(filename);
        var count = 0;
        foreach (var line in lines)
        { 
            var (supernets, hypernets) = SplitSegments(line);
            var abas = new List<string>();
            foreach (var supernet in supernets)
            {
                for (int i = 0; i <= supernet.Length - 3; i++)
                {
                    char a = supernet[i], b = supernet[i + 1], c = supernet[i + 2];
                    if (a != b && a == c)
                    {
                        abas.Add($"{a}{b}{a}");
                    }
                }
            }
            var babs = new HashSet<string>();
            foreach (var hypernet in hypernets)
            {
                for (int i = 0; i <= hypernet.Length - 3; i++)
                {
                    char a = hypernet[i], b = hypernet[i + 1], c = hypernet[i + 2];
                    if (a != b && a == c)
                    {
                        babs.Add($"{b}{a}{b}");
                    }
                }
            }
            if (abas.Any(aba => babs.Contains(aba)))
            {
                count++;
            }
        }

        return new SolutionResult(count.ToString());
    }


}