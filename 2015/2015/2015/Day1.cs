namespace AoC2015;
public class Day1
{
    public static List<char> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.First().ToList();
    }

    [Solveable("2015/Puzzles/Day1.txt", "Day 1 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var floor = instructions.Count(_ => _ == '(') - instructions.Count(_ => _ == ')');
        return new SolutionResult(floor.ToString());
    }

    [Solveable("2015/Puzzles/Day1.txt", "Day 1 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var floor = 0;
        var count = 0;
        while(floor != -1)
        {
            floor += instructions[count] == '(' ? 1 : -1;
            count++;
        }
        
        return new SolutionResult(count.ToString());
    }


}