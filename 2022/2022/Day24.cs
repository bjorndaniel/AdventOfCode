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
        return TimeToTarget(printer, blizzards, rows, columns, new List<(int, int)> { (rows, columns - 1) }, -1, 0, 0);
    }

    public static int SolvePart2(string filename, IPrinter printer)
    {
        var (blizzards, rows, columns) = ParseInput(filename);
        return TimeToTarget(printer, blizzards, rows, columns, new List<(int, int)> { (rows, columns - 1), (-1,0), (rows, columns - 1) }, -1, 0, 2);
    }

    private static int TimeToTarget(IPrinter printer,
        Dictionary<char, List<(int row, int col)>> blizzards,
        int rows, int columns, List<(int tr, int tc)> targets, int startRow, int startColumn, int endStage)
    {
        var queue = new Queue<(int, int, int, int)>();
        queue.Enqueue((0, startRow, startColumn, 0));
        var lcm = LCM(rows, columns);
        var seen = new HashSet<(int, int, int, int)>();
        var movements = new List<(int dr, int dc)> { (0, 1), (0, -1), (-1, 0), (1, 0), (0, 0) };

        while (queue.Any())
        {
            var (time, cr, cc, stage) = queue.Dequeue();
            time += 1;
            foreach (var (dr, dc) in new[] { (0, 1), (0, -1), (-1, 0), (1, 0), (0, 0) })
            {
                var nr = cr + dr;
                var nc = cc + dc;
                var nstage = stage;
                if ((nr, nc) == targets[Modulo(stage, 2)])
                {
                    if(stage == endStage)
                    {
                        return time;
                    }
                    nstage += 1;
                }
                if ((nr < 0 || nc < 0 || nr >= rows || nc >= columns) && !targets.Contains((nr,nc)))
                {
                    continue;
                }

                var checks = new List<(char dir, int tr, int tc)> { ('<', 0, -1), ('>', 0, 1), ('^', -1, 0), ('v', 1, 0) };
                var wasInBlizzard = false;
                if (!targets.Contains((nr, nc)))
                {
                    foreach (var (dir, tr, tc) in checks)
                    {
                        var x = (Modulo(nr - tr * time, rows), Modulo(nc - tc * time, columns));
                        if (blizzards.ContainsKey(dir) && blizzards[dir].Any(_ => _.row == x.Item1 && _.col == x.Item2))
                        {
                            wasInBlizzard = true;
                            break;
                        }
                    }
                }
                if (!wasInBlizzard)
                {
                    var mod = Modulo(time,lcm);
                    var key = (nr, nc, stage, mod);
                    if (seen.Contains(key))
                    {
                        continue;
                    }
                    seen.Add(key);
                    queue.Enqueue((time, nr, nc, nstage));
                }
            }
        }
        return -1;
    }

    private static int LCM(int a, int b)
    {
        return a * b / GCD(a, b);
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static int Modulo(int a, int b)
    {
        int result = a % b;
        if (result < 0 && b > 0)
        {
            result += b;
        }
        else if (result > 0 && b < 0)
        {
            result += b;
        }
        return result;
    }
}

