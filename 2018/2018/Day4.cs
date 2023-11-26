using static AoC2018.Day4;

namespace AoC2018;
public class Day4
{
    public static List<Guard> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Guard>();
        var withDates = new List<(DateTime date, string action)>();
        foreach(var l in lines)
        {
            var date = DateTime.Parse(l.Substring(1, 16));
            withDates.Add((date, l.Substring(19)));
        }
        var ordered = withDates.OrderBy(_ => _.date);
        Guard currentGuard = null!;
        foreach(var l in ordered)
        {
            if (l.action.StartsWith("Guard"))
            {
                var id = int.Parse(l.action.Split(' ')[1].Substring(1));
                var guard = result.FirstOrDefault(g => g.Id == id);
                if (guard == null)
                {
                    guard = new Guard { Id = id };
                    result.Add(guard);
                }
                currentGuard = guard;
            }
            else if (l.action.StartsWith("falls"))
            {
                var shift = new Shift { Asleep = l.date, Type = ActionType.FallAsleep };
                currentGuard.Shifts.Add(shift);
            }
            else if (l.action.StartsWith("wakes"))
            {
                //var guard = result.Last();
                var shift = currentGuard.Shifts.Where(_ => _.Type == ActionType.FallAsleep).OrderBy(_ => _.Asleep).Last();  
                shift.Type = ActionType.WakeUp;
                shift.Wakeup = l.date;
                var minutes = Enumerable.Range(shift.Asleep.Minute, shift.Wakeup.Minute - shift.Asleep.Minute);
                shift.MinutesAsleep.AddRange(minutes);
            }
        }
        return result;
    }

    [Solveable("2018/Puzzles/Day4.txt", "Day4 part 1")]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var guards = ParseInput(filename);
        var guard = guards.OrderByDescending(_ => _.GetAsleepCount()).First();
        return new SolutionResult((guard.GetMostAsleepMinute().minute * guard.Id).ToString());
    }

    [Solveable("2018/Puzzles/Day4.txt", "Day4 part 2")]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var guards = ParseInput(filename);
        var result = guards.Select(_ => new { guard = _, minute = _.GetMostAsleepMinute() }).OrderByDescending(_ => _.minute.count).First();
        return new SolutionResult((result.minute.minute * result.guard.Id).ToString());
    }

    public class Guard
    {
        public int Id { get; set; }
        public List<Shift> Shifts { get; set; } = new List<Shift>();
        public (int minute, int count) GetMostAsleepMinute()
        {
            var result = Shifts.SelectMany(_ => _.MinutesAsleep).GroupBy(_ => _).OrderByDescending(_ => _.Count()).FirstOrDefault();
            return (result?.Key ?? 0, result?.Count() ?? 0);
        }
        public int GetAsleepCount()
        {
            var result = Shifts.SelectMany(_ => _.MinutesAsleep).Count();
            return result;
        }   
    }

    public class Shift
    {
        public DateTime Asleep { get; set; }
        public DateTime Wakeup { get; set; }
        public ActionType Type { get; set; }
        public List<int> MinutesAsleep { get; set; } = new List<int>();
    }

    public enum ActionType
    {
        BeginShift,
        FallAsleep,
        WakeUp
    }
}