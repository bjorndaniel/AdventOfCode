namespace AoC2015;

public class Day14
{
    public static List<Reindeer> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.Select(line =>
        {
            var parts = line.Split(' ');
            var name = parts[0];
            var speed = int.Parse(parts[3]);
            var flyTime = int.Parse(parts[6]);
            var restTime = int.Parse(parts[13]);
            return new Reindeer(name, speed, flyTime, restTime);
        }).ToList();
    }

    [Solveable("2015/Puzzles/Day14.txt", "Day 14 part 1", 14)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var reindeer = ParseInput(filename);
        var raceTime = filename.Contains("test") ? 1000 : 2503;
        var distances = reindeer.OrderByDescending(r => r.Distance(raceTime));
        return new SolutionResult(distances.First().Distance(raceTime).ToString());
    }

    [Solveable("2015/Puzzles/Day14.txt", "Day 14 part 2", 14)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var reindeer = ParseInput(filename);
        var raceTime = filename.Contains("test") ? 1000 : 2503;
        for (int t = 1; t <= raceTime; t++)
        {
            var distances = reindeer.ToDictionary(r => r.Name, r => r.Distance(t));
            var maxDistance = distances.Values.Max();
            var leader = distances.Where(d => d.Value == maxDistance).Select(_ => _.Key);
            foreach (var r in reindeer.Where(r => leader.Contains(r.Name)))
            {
                r.Points++;
            }

        }
        return new SolutionResult(reindeer.Max(_ => _.Points).ToString());
    }


}
public record Reindeer(string Name, int Speed, int FlyTime, int RestTime)
{
    public int Points { get; set; } = 0;
    public int Distance(int time)
    {
        var cycleTime = FlyTime + RestTime;
        var fullCycles = time / cycleTime;
        var remainingTime = time % cycleTime;
        var distance = fullCycles * Speed * FlyTime;
        if (remainingTime > FlyTime)
        {
            distance += Speed * FlyTime;
        }
        else
        {
            distance += Speed * remainingTime;
        }
        return distance;
    }
};