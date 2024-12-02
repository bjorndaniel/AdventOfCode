namespace AoC2015;
public class Day9
{
    public static List<Route> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Route>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var from = parts[0];
            var to = parts[2];
            var distance = int.Parse(parts[4]);
            result.Add(new (from, to, distance));
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day9.txt", "Day 9 part 1", 9)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var routes = ParseInput(filename);
        var (permutations, dist) = PrepareData(routes);
        var minDistance = int.MaxValue;
        foreach (var perm in permutations)
        {
            var currentDistance = 0;
            for (int i = 0; i < perm.Count - 1; i++)
            {
                currentDistance += dist[perm[i], perm[i + 1]];
            }
            minDistance = Math.Min(minDistance, currentDistance);
        }

        return new SolutionResult(minDistance.ToString());
    }

    [Solveable("2015/Puzzles/Day9.txt", "Day 9 part 2", 9)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var routes = ParseInput(filename);
        var (permutations, dist) = PrepareData(routes);
        var maxDistance = int.MinValue;
        foreach (var perm in permutations)
        {
            var currentDistance = 0;
            for (int i = 0; i < perm.Count - 1; i++)
            {
                currentDistance += dist[perm[i], perm[i + 1]];
            }
            maxDistance = Math.Max(maxDistance, currentDistance);
        }

        return new SolutionResult(maxDistance.ToString());
    }

    private static (IEnumerable<List<int>> permutations, int[,] dist) PrepareData(List<Route> routes)
    {
        var cities = routes.SelectMany(r => new[] { r.From, r.To }).Distinct().ToList();
        var cityIndex = cities.Select((city, index) => new { city, index }).ToDictionary(x => x.city, x => x.index);
        var n = cities.Count;
        var dist = new int[n, n];

        foreach (var route in routes)
        {
            var i = cityIndex[route.From];
            var j = cityIndex[route.To];
            dist[i, j] = route.Distance;
            dist[j, i] = route.Distance;
        }

        var permutations = GetPermutations(Enumerable.Range(0, n).ToList(), n);
        return (permutations, dist);
    }

    private static IEnumerable<List<Route>> GetPermutations<Route>(List<Route> list, int length)
    {
        if (length == 1)
        {
            return list.Select(_=> new List<Route> { _ });
        }
        return GetPermutations(list, length - 1)
            .SelectMany(_ => 
                list.Where(r => _.Contains(r) is false),(r1, r2) => r1.Concat(new List<Route> { r2 })
            .ToList());
    }
}

public record Route(string From, string To, int Distance);