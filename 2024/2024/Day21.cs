//Solution adapted from https://www.bytesizego.com/blog/aoc-day21-golang
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
        var result = 0L;
        var robots = 2;
        foreach (var (code, number) in codes)
        {
            var seq1 = GetPressesForNumericPad(code, PadKey.A, KeyPad());
            var count = GetCountAfterRobots(seq1, robots, 1, DirectionPad(), []);

            result += count * number;
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day21.txt", "Day 21 part 2", 21)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var codes = ParseInput(filename);
        var result = 0L;
        var robots = 25;
        foreach (var (code, number) in codes)
        {
            var seq1 = GetPressesForNumericPad(code, PadKey.A, KeyPad());
            var count = GetCountAfterRobots(seq1, robots, 1, DirectionPad(), []);
            result += count * number;
        }
        return new SolutionResult(result.ToString());
    }

    public static Dictionary<PadKey, (int x, int y)> KeyPad()
    {
        var keyPad = new Dictionary<PadKey, (int x, int y)>();
        keyPad.Add(PadKey.SEVEN, (0, 3));
        keyPad.Add(PadKey.EIGHT, (1, 3));
        keyPad.Add(PadKey.NINE, (2, 3));
        keyPad.Add(PadKey.FOUR, (0, 2));
        keyPad.Add(PadKey.FIVE, (1, 2));
        keyPad.Add(PadKey.SIX, (2, 2));
        keyPad.Add(PadKey.ONE, (0, 1));
        keyPad.Add(PadKey.TWO, (1, 1));
        keyPad.Add(PadKey.THREE, (2, 1));
        //keyPad.Add(PadKey.OUTOFBOUNDS, (0, 0));
        keyPad.Add(PadKey.ZERO, (1, 0));
        keyPad.Add(PadKey.A, (2, 0));
        return keyPad;
    }

    public static Dictionary<PadKey, (int x, int y)> DirectionPad()
    {
        var dirPad = new Dictionary<PadKey, (int x, int y)>();
        //dirPad.Add(PadKey.OUTOFBOUNDS, (0, 1));
        dirPad.Add(PadKey.UP, (1, 1));
        dirPad.Add(PadKey.A, (2, 1));
        dirPad.Add(PadKey.LEFT, (0, 0));
        dirPad.Add(PadKey.DOWN, (1, 0));
        dirPad.Add(PadKey.RIGHT, (2, 0));
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

    public static List<PadKey> GetPadKeys(string input)
    {
        var padKeys = new List<PadKey>();
        foreach (var c in input)
        {
            if (c == '>')
            {
                padKeys.Add(PadKey.RIGHT);
                continue;
            }
            if (c == '<')
            {
                padKeys.Add(PadKey.LEFT);
                continue;
            }
            if (c == '^')
            {
                padKeys.Add(PadKey.UP);
                continue;
            }
            if (c == 'v')
            {
                padKeys.Add(PadKey.DOWN);
                continue;
            }
            if (c == 'A')
            {
                padKeys.Add(PadKey.A);
                continue;
            }
            padKeys.Add((PadKey)Enum.Parse(typeof(PadKey), c.ToString()));
        }
        return padKeys;
    }

    private static long GetCountAfterRobots(string input, int nrOfRobots, long robot, Dictionary<PadKey, (int x, int y)> pad, Dictionary<string, long[]> cache)
    {
        if (cache.TryGetValue(input, out var val))
        {
            if (val[robot-1] != 0)
            {
                return val[robot -1];
            }
        }
        else
        {
            cache.Add(input, new long[nrOfRobots]);
        }
        var seq = GetPressesForDirectionalPad(input, PadKey.A, pad);
        cache[input][0] = seq.Length;
        if(robot == nrOfRobots)
        {
            return seq.Length;
        }
        var split = GetIndividualSteps(seq);
        var count = 0L;
        foreach (var s in split)
        {
            var c = GetCountAfterRobots(s, nrOfRobots, robot + 1, pad, cache);
            if (cache.ContainsKey(s) is false)
            {
                cache[s] = new long[nrOfRobots];
            }
            cache[s][0] = c;
            count += c;
        }

        cache[input][robot - 1] = count;
        return count;
    }

    private static List<string> GetIndividualSteps(string input)
    {
        var output = new List<string>();
        var current = new StringBuilder();

        foreach (var c in input)
    {
            current.Append(c);

            if (c == 'A')
            {
                output.Add(current.ToString());
                current = new();
            }
        }

        return output;
    }


    private static string GetPressesForNumericPad(string input, PadKey start, Dictionary<PadKey, (int x, int y)> keyPad)
    {
        var current = keyPad[start];
        var output = new StringBuilder();

        foreach (var key in GetPadKeys(input))
        {
            
            var dest = keyPad[key];
            var diffX = dest.x - current.x;
            var diffY = dest.y - current.y;

            var horizontal = new StringBuilder();
            var vertical = new StringBuilder();

            for (int i = 0; i < Math.Abs(diffX); i++)
            {
                if (diffX >= 0)
                {
                    horizontal.Append(">");
                }
                else
                {
                    horizontal.Append("<");
                }
            }

            for (int i = 0; i < Math.Abs(diffY); i++)
            {
                if (diffY >= 0)
                {
                    vertical.Append("^");
                }
                else
                {
                    vertical.Append("v");
                }
            }

            // prioritization order:
            // 1. moving with least turns
            // 2. moving < over ^ over v over >

            if (current.y == 0 && dest.x == 0)
            {
                output.Append(vertical);
                output.Append(horizontal);
            }
            else if (current.x == 0 && dest.y == 0)
            {
                output.Append(horizontal);
                output.Append(vertical);
            }
            else if (diffX < 0)
            {
                output.Append(horizontal);
                output.Append(vertical);
            }
            else if (diffX >= 0)
            {
                output.Append(vertical);
                output.Append(horizontal);
            }

            current = dest;
            output.Append("A");
        }

        return output.ToString();
    }

    private static string GetPressesForDirectionalPad(string input, PadKey start, Dictionary<PadKey, (int x, int y)> directionalPad)
    {
        var current = directionalPad[start];
        var output = new StringBuilder();
        foreach (var key in GetPadKeys(input))
        {
            var dest = directionalPad[key];
            var diffX = dest.x - current.x;
            var diffY = dest.y - current.y;

            var horizontal = new StringBuilder();
            var vertical = new StringBuilder();

            for (int i = 0; i < Math.Abs(diffX); i++)
            {
                if (diffX >= 0)
                {
                    horizontal.Append(">");
                }
                else
                {
                    horizontal.Append("<");
                }
            }

            for (int i = 0; i < Math.Abs(diffY); i++)
            {
                if (diffY >= 0)
                {
                    vertical.Append("^");
                }
                else
                {
                    vertical.Append("v");
                }
            }

            // prioritization order:
            // 1. moving with least turns
            // 2. moving < over ^ over v over >

            if (current.x == 0 && dest.y == 1)
            {
                output.Append(horizontal);
                output.Append(vertical);
            }
            else if (current.y == 1 && dest.x == 0)
            {
                output.Append(vertical);
                output.Append(horizontal);
            }
            else if (diffX <= 0)
            {
                output.Append(horizontal);
                output.Append(vertical);
            }
            else if (diffX > 0)
            {
                output.Append(vertical);
                output.Append(horizontal);
            }

            current = dest;
            output.Append("A");
        }
        return output.ToString();
    }

}
