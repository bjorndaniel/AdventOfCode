namespace AoC2023;
public class Day6
{
    public static List<Race> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Race>();
        var times = lines.First().Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var records = lines.Last().Split(":").Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < times.Length; i++)
        {
            result.Add(new Race(long.Parse(times[i]), long.Parse(records[i])));
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day6.txt", "Day 6 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var races = ParseInput(filename);
        var maxWays = new List<int>();
        foreach (var race in races)
        {
            int maxCombinations = GetCombinations(race);
            maxWays.Add(maxCombinations);
        }
        return new SolutionResult(maxWays.Aggregate((a, b) => a * b).ToString());
    }

    [Solveable("2023/Puzzles/Day6.txt", "Day 6 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var races = ParseInput(filename);
        var ltime = long.Parse(races.Select(_ => _.Time.ToString()).Aggregate((a, b) => $"{a}{b}"));
        var lrecord = long.Parse(races.Select(_ => _.Record.ToString()).Aggregate((a, b) => $"{a}{b}"));
        var race = new Race(ltime, lrecord);
        var time = race.Time;
        var record = race.Record;
        return new SolutionResult(GetCombinations(race).ToString());

    }

    private static int GetCombinations(Race race)
    {
        var time = race.Time;
        var record = race.Record;
        var maxCombinations = 0;
        for (var waitTime = 0; waitTime < time; waitTime++)
        {
            var remainingTime = time - waitTime;
            var distance = waitTime * remainingTime;
            if (distance > record)
            {
                maxCombinations++;
            }
        }
        return maxCombinations;
    }


    public record Race(long Time, long Record) { }

}