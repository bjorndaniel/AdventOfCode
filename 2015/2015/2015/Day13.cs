namespace AoC2015;
public class Day13
{
    public static List<Guest> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var guests = new Dictionary<string, Guest>();
        foreach (var line in lines)
        {
            var name = line.Split(' ')[0];
            var happiness = new Dictionary<string, int>();
            var otherGuest = line.Split(' ').Last()[..^1];
            var happinessValue = int.Parse(line.Split(' ')[3]);
            if (line.Contains("lose"))
            {
                happinessValue = -happinessValue;
            }
            if (guests.ContainsKey(name))
            {
                guests[name].Happiness.Add(otherGuest, happinessValue);
            }
            else
            {
                guests[name] = new Guest(name, new Dictionary<string, int> { { otherGuest, happinessValue } });
            }
        }
        return guests.Values.ToList();
    }

    [Solveable("2015/Puzzles/Day13.txt", "Day 13 part 1", 13)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var guests = ParseInput(filename);
        var names = guests.Select(g => g.Name).ToList();
        var maxHappiness = int.MinValue;
        foreach (var seating in GetPermutations(names, names.Count))
        {
            var total = 0;
            for (var i = 0; i < seating.Count; i++)
            {
                var left = seating[i];
                var right = seating[(i + 1) % seating.Count];
                var leftGuest = guests.First(g => g.Name == left);
                var rightGuest = guests.First(g => g.Name == right);
                total += leftGuest.GetHappiness(right);
                total += rightGuest.GetHappiness(left);
            }
            if (total > maxHappiness)
            {
                maxHappiness = total;
            }
        }
        return new SolutionResult(maxHappiness.ToString());
    }

    // Helper to generate all permutations of a list
    private static IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
    {
        if (length == 1)
        {
            return list.Select(t => new List<T> { t });
        }
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new List<T> { t2 }).ToList());
    }

    [Solveable("2015/Puzzles/Day13.txt", "Day 13 part 2", 13)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var guests = ParseInput(filename);
        var names = guests.Select(g => g.Name).ToList();
        var meHappiness = new Dictionary<string, int>();
        foreach (var name in names)
        {
            meHappiness[name] = 0;
        }
        guests.Add(new Guest("Me", meHappiness));
        foreach (var guest in guests)
        {
            if (guest.Name != "Me")
            {
                guest.Happiness["Me"] = 0;
            }
        }
        names.Add("Me");
        var newMaxHappiness = int.MinValue;
        foreach (var seating in GetPermutations(names, names.Count))
        {
            var total = 0;
            for (var i = 0; i < seating.Count; i++)
            {
                var left = seating[i];
                var right = seating[(i + 1) % seating.Count];
                var leftGuest = guests.First(g => g.Name == left);
                var rightGuest = guests.First(g => g.Name == right);
                total += leftGuest.GetHappiness(right);
                total += rightGuest.GetHappiness(left);
            }
            if (total > newMaxHappiness)
            {
                newMaxHappiness = total;
            }
        }
        return new SolutionResult(newMaxHappiness.ToString());
    }
}

public record Guest(string Name, Dictionary<string, int> Happiness)
{
    public int GetHappiness(string name) => Happiness[name];
};