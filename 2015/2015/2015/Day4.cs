namespace AoC2015;
public class Day4
{
    public static string ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.First();
    }

    [Solveable("2015/Puzzles/Day4.txt", "Day 4 part 1", 4)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var counter = 1L;
        while (true)
        {
            var hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes($"{input}{counter}"));
            var hashString = BitConverter.ToString(hash).Replace("-", "");
            if (hashString.StartsWith("00000"))
            {
                return new SolutionResult(counter.ToString());
            }
            counter++;
        }
    }

    [Solveable("2015/Puzzles/Day4.txt", "Day 4 part 2", 4, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var counter = 999999;
        var md5 = MD5.Create();
        while (true)
        {
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes($"{input}{counter}"));
            var hashString = BitConverter.ToString(hash).Replace("-", "");
            if (hashString.StartsWith("000000"))
            {
                return new SolutionResult(counter.ToString());
            }
            counter++;
        }
    }


}