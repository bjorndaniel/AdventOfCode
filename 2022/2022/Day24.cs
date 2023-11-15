//Adapted from: https://github.com/hyper-neutrino/advent-of-code/tree/main
namespace AoC2022;
public static class Day24
{
    public static (Dictionary<char, List<(int row, int col)>> blizzards, int row, int col) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename).Skip(1).ToArray();
        var result = new Dictionary<char, List<(int, int)>>();
        var r = lines.Length - 1;
        var c = lines[0].Length - 1;

        for (var i = 0; i < r; i++)
        {
            for (var j = 1; j < c; j++)
            {
                var item = lines[i][j];
                if (item == '<' || item == '>' || item == '^' || item == 'v')
                {
                    if (result.ContainsKey(item))
                    {
                        result[item].Add((i, j - 1));
                    }
                    else
                    {
                        result.Add(item, new List<(int, int)> { (i, j - 1) });
                    }
                }
            }
        }
        return (result, r, c - 1);

    }

    public static int SolvePart1(string filename, IPrinter printer)
    {
        var (blizzards, rows, columns) = ParseInput(filename);

        var queue = new Queue<(int, int, int)>();
        queue.Enqueue((0, -1, 0));
        var (targetRow, targetColumn) = (rows, columns - 1);
        var lcm = LCM(rows, columns);
        var seen = new HashSet<(int, int, int)>();
        var movements = new List<(int dr, int dc)> { (0, 1), (0, -1), (-1, 0), (1, 0), (0, 0) };

        printer.Print($"TargetRow {targetRow} TargetColumn {targetColumn}");
        printer.Flush();
        printer.Print($"Rows {rows} Columns {columns}");
        printer.Flush();
        printer.Print($"LCM {lcm}");
        printer.Flush();

        while (queue.Any())
        {
            var (time, cr, cc) = queue.Dequeue();
            time += 1;
            foreach (var (dr, dc) in new[] { (0, 1), (0, -1), (-1, 0), (1, 0), (0, 0) })
            {
                var nr = cr + dr;
                var nc = cc + dc;
                if ((nr, nc) == (targetRow, targetColumn))
                {
                    printer.Print($"At {nr} {nc}");
                    printer.Flush();
                    return time;
                }
                if ((nr < 0 || nc < 0 || nr >= rows || nc >= columns) && (nr, nc) != (-1, 0))
                {
                    continue;
                }

                var checks = new List<(char dir, int tr, int tc)> { ('<', 0, -1), ('>', 0, 1), ('^', -1, 0), ('v', 1, 0) };
                var wasInBlizzard = false;
                if ((nr, nc) != (-1, 0))
                {
                    foreach (var (dir, tr, tc) in checks)
                    {
                        var newr = (nr - tr * time) % rows;
                        var newc = (nc - tc * time) % columns;
                        var x = (newr < 0 ? (newr + (tr * time)) : newr, newc < 0 ? (newc + (tc * time)) : newc);
                        if (blizzards.ContainsKey(dir) && blizzards[dir].Any(_ => _.row == x.Item1 && _.col == x.Item2))
                        {
                            wasInBlizzard = true;
                            break;
                        }
                    }
                }
                if (!wasInBlizzard)
                {
                    var mod = time % lcm;
                    var key = (nr, nc, mod < 0 ? mod + lcm : mod);
                    if (seen.Contains(key))
                    {
                        continue;
                    }
                    seen.Add(key);
                    queue.Enqueue((time, nr, nc));
                }
            }
        }
        return -1;

        int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }

        int GCD(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}

public class ValleyPoint
{
    public List<char?> Occupants { get; set; } = new();

    public void AddOccupant(char occupant)
    {
        if (Occupants.All(_ => _ == '.'))
        {
            Occupants.Clear();
            Occupants.Add(occupant);
        }
        else if (Occupants.Any(_ => _ == '#'))
        {
            throw new ArgumentException("Cannot add occupant to a wall");
        }
        else
        {
            Occupants.Add(occupant);
        }
    }

    public bool Skip() =>
        Occupants.All(_ => _ == '.' || _ == '#' || _ == 'E');

    public override string ToString() =>
        Occupants.Count() > 1 ? Occupants.Count().ToString() : Occupants[0]?.ToString() ?? ".";

    public void RemoveOccupant(char c)
    {
        Occupants.Remove(c);
        if (Occupants.Count == 0)
        {
            Occupants.Add('.');
        }
    }
}