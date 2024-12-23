namespace AoC2024;
public class Day21
{
    public static List<(string code, long number)> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<(string code, long number)>();
        foreach (var line in lines)
        {
            var parts = line[..^1];
            result.Add((line, long.Parse(parts)));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day21.txt", "Day 21 part 1", 21)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var codes = ParseInput(filename);
        var keypad = KeyPad();
        var dirPad1 = DirectionPad();
        var dirPad2 = DirectionPad();
        var dirPad3 = DirectionPad();
        var result = new List<PadKey>();
        var complexity = 0L;
        var currentPosition = keypad[PadKey.A];
        var currentPositionDirpad1 = dirPad1[PadKey.A];
        var currentPositionDirpad2 = dirPad2[PadKey.A];
        var currentPositionDirpad3 = dirPad3[PadKey.A];
        foreach (var code in codes)
        {
            var codeMoves = new List<PadKey>();
            foreach (var padKey in GetPadKeys(code.code))
            {
                var targetPosition = keypad[padKey];
                // Find moves on the first direction pad (keypad)
                var path1 = FindShortestPathOnDirectionPad(currentPosition, targetPosition, keypad);
                path1.Add(PadKey.A); // Add the key to the path
                codeMoves.AddRange(path1);
                //path1.AddRange(FindShortestPathOnDirectionPad(targetPosition, (2,3), keypad));

                //// Find moves on the second direction pad to get the moves for path1
                //var path2 = new List<PadKey>();
                //foreach (var move in path1)
                //{
                //    var dirPad1End = dirPad1[move];
                //    path2.AddRange(FindShortestPathOnDirectionPad(currentPositionDirpad1, dirPad1End, dirPad1));
                //    currentPositionDirpad1 = dirPad1End;
                //}
                //codeMoves.AddRange(path2);

                //var path3 = new List<PadKey>();
                //foreach (var move in path2)
                //{
                //    var dirPad2End = dirPad2[move];
                //    path3.AddRange(FindShortestPathOnDirectionPad(currentPositionDirpad2, dirPad2End, dirPad2));
                //    currentPositionDirpad2 = dirPad2End;
                //}
                //codeMoves.AddRange(path3);

                //var path4 = new List<PadKey>();
                //foreach (var move in path3)
                //{
                //    var dirPad3End = dirPad3[move];
                //    path4.AddRange(FindShortestPathOnDirectionPad(currentPositionDirpad3, dirPad3End, dirPad3));
                //    currentPositionDirpad3 = dirPad3End;
                //}

                //codeMoves.AddRange(path4);
                currentPosition = targetPosition;
               
            }
            printer.Print(code.code);
            printer.Flush();
            var display = codeMoves.Select(_ => GetKey(_)).Aggregate((a, b) => $"{a}{b}");
            printer.Print(display);
            printer.Flush();
            printer.Print("");
            printer.Flush();
            complexity += code.number * codeMoves.Count;
            result.AddRange(codeMoves);
        }

        return new SolutionResult(complexity.ToString());
    }

    private static List<PadKey> FindShortestPathOnDirectionPad((int x, int y) start, (int x, int y) end, Dictionary<PadKey, (int x, int y)> pad)
    {
        var directions = new Dictionary<PadKey, (int x, int y)>
        {
            { PadKey.UP, (0, -1) },
            { PadKey.DOWN, (0, 1) },
            { PadKey.LEFT, (-1, 0) },
            { PadKey.RIGHT, (1, 0) }
        };

        var queue = new Queue<((int x, int y) pos, List<PadKey> path)>();
        var visited = new HashSet<(int x, int y)>();
        queue.Enqueue((start, new List<PadKey>()));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (current, path) = queue.Dequeue();

            if (current == end)
            {
                return path;
            }

            foreach (var direction in directions)
            {
                var next = (x: current.x + direction.Value.x, y: current.y + direction.Value.y);
                if (IsValidPosition(next, pad) && !visited.Contains(next))
                {
                    var newPath = new List<PadKey>(path) { direction.Key };
                    queue.Enqueue((next, newPath));
                    visited.Add(next);
                }
            }
        }

        return new List<PadKey>(); // Return an empty list if no path is found
    }

    private static bool IsValidPosition((int x, int y) pos, Dictionary<PadKey, (int x, int y)> pad) =>
        pad.Values.Contains(pos) && pad[PadKey.OUTOFBOUNDS] != pos; // Ensure the position is within bounds and not OUTOFBOUNDS

    [Solveable("2024/Puzzles/Day21.txt", "Day 21 part 2", 21)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    public static Dictionary<PadKey, (int x, int y)> KeyPad()
    {
        var keyPad = new Dictionary<PadKey, (int x, int y)>();
        keyPad.Add(PadKey.SEVEN, (0, 0));
        keyPad.Add(PadKey.EIGHT, (1, 0));
        keyPad.Add(PadKey.NINE, (2, 0));
        keyPad.Add(PadKey.FOUR, (0, 1));
        keyPad.Add(PadKey.FIVE, (1, 1));
        keyPad.Add(PadKey.SIX, (2, 1));
        keyPad.Add(PadKey.ONE, (0, 2));
        keyPad.Add(PadKey.TWO, (1, 2));
        keyPad.Add(PadKey.THREE, (2, 2));
        keyPad.Add(PadKey.OUTOFBOUNDS, (0, 3));
        keyPad.Add(PadKey.ZERO, (1, 3));
        keyPad.Add(PadKey.A, (2, 3));
        return keyPad;
    }

    public static Dictionary<PadKey, (int x, int y)> DirectionPad()
    {
        var dirPad = new Dictionary<PadKey, (int x, int y)>();
        dirPad.Add(PadKey.OUTOFBOUNDS, (0, 0));
        dirPad.Add(PadKey.UP, (1, 0));
        dirPad.Add(PadKey.A, (2, 0));
        dirPad.Add(PadKey.LEFT, (0, 1));
        dirPad.Add(PadKey.DOWN, (1, 1));
        dirPad.Add(PadKey.RIGHT, (2, 1));
        return dirPad;
    }

    public enum PadKey
    {
        ZERO = 0,
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        A = 10,
        UP = 11,
        LEFT = 12,
        RIGHT = 13,
        DOWN = 14,
        OUTOFBOUNDS = 15
    }

    public static List<PadKey> GetPadKeys(string code)
    {
        var padKeys = new List<PadKey>();
        foreach (var c in code)
        {
            padKeys.Add((PadKey)Enum.Parse(typeof(PadKey), c.ToString()));
        }
        return padKeys;
    }

    public static string GetKey(PadKey key) => key switch
    {
        PadKey.ZERO => "0",
        PadKey.ONE => "1",
        PadKey.TWO => "2",
        PadKey.THREE => "3",
        PadKey.FOUR => "4",
        PadKey.FIVE => "5",
        PadKey.SIX => "6",
        PadKey.SEVEN => "7",
        PadKey.EIGHT => "8",
        PadKey.NINE => "9",
        PadKey.A => "A",
        PadKey.UP => "^",
        PadKey.LEFT => "<",
        PadKey.RIGHT => ">",
        PadKey.DOWN => "v",
        PadKey.OUTOFBOUNDS => "X",
        _ => ""
    };
}
