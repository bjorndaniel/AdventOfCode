namespace AoC2015;
public class Day3
{
    public static List<char> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        return lines.First().ToCharArray().ToList();
    }

    [Solveable("2015/Puzzles/Day3.txt", "Day 3 part 1", 3)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var movements = ParseInput(filename);
        var visited = new HashSet<(int x, int y)>();
        var (cx, cy) = (0, 0);
        visited.Add((cx, cy));
        foreach (var c in movements)
        {
            (cx, cy) = CheckAndAddHouse(visited, cx, cy, c);
        }
        return new SolutionResult(visited.Count().ToString());
    }

    [Solveable("2015/Puzzles/Day3.txt", "Day 3 part 2", 3)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var movements = ParseInput(filename);
        var visited = new HashSet<(int x, int y)>();
        var (sx, sy) = (0, 0);
        var (rx, ry) = (0, 0);
        visited.Add((sx, sy));
        for (int i = 0; i < movements.Count - 1; i = i + 2)
        {
            var santas = movements[i];
            var robo = movements[i + 1];
            (sx, sy) = CheckAndAddHouse(visited, sx, sy, santas);
            (rx, ry) = CheckAndAddHouse(visited, rx, ry, robo);
        }

        return new SolutionResult(visited.Count().ToString());
      
    }
    private static (int nx, int ny) CheckAndAddHouse(HashSet<(int x, int y)> visited, int cx, int cy, char movement)
    {
        switch (movement)
        {
            case '^':
                cy = cy + 1;
                if (!visited.Contains((cx, cy)))
                {
                    visited.Add((cx, cy));
                }
                break;
            case 'v':
                cy = cy - 1;
                if (!visited.Contains((cx, cy)))
                {
                    visited.Add((cx, cy));
                }
                break;
            case '>':
                cx = cx + 1;
                if (!visited.Contains((cx, cy)))
                {
                    visited.Add((cx, cy));
                }
                break;
            case '<':
                cx = cx - 1;
                if (!visited.Contains((cx, cy)))
                {
                    visited.Add((cx, cy));
                }
                break;
            default:
                throw new Exception($"Unknown movement {movement}");
        }
        return (cx, cy);
    }

}