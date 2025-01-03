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
        foreach (var (code, number) in codes)
        {
            var seq1 = GetPressesForNumericPad(code, PadKey.A, KeyPad());
            var seq2 = GetPressesForDirectionalPad(seq1, PadKey.A, DirectionPad());
            seq2 = GetPressesForDirectionalPad(seq2, PadKey.A, DirectionPad());
            result += seq2.Length * number;
        }
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day21.txt", "Day 21 part 2", 21)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var codes = ParseInput(filename);
        var result = 0L;
        foreach (var (code, number) in codes)
        {
            var seq1 = GetPressesForNumericPad(code, PadKey.A, KeyPad());
            var seq2 = GetPressesForDirectionalPad(seq1, PadKey.A, DirectionPad());

            for(int i = 0; i < 25; i++)
            {
                seq2 = GetPressesForDirectionalPad(seq2, PadKey.A, DirectionPad());
            }
            
            result += seq2.Length * number;
        }
        return new SolutionResult("");
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
        keyPad.Add(PadKey.OUTOFBOUNDS, (0, 0));
        keyPad.Add(PadKey.ZERO, (1, 0));
        keyPad.Add(PadKey.A, (2, 0));
        return keyPad;
    }

    public static Dictionary<PadKey, (int x, int y)> DirectionPad()
    {
        var dirPad = new Dictionary<PadKey, (int x, int y)>();
        dirPad.Add(PadKey.OUTOFBOUNDS, (0, 1));
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
            if(c == '>')
            {
                padKeys.Add(PadKey.RIGHT);
                continue;
            }
            if(c == '<')
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
            if(c == 'A')
            {
                padKeys.Add(PadKey.A);
                continue;
            }
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

    public static string GetPressesForNumericPad(string input, PadKey start, Dictionary<PadKey, (int x, int y)> keyPad)
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

    public static string GetPressesForDirectionalPad(string input, PadKey start, Dictionary<PadKey, (int x, int y)> directionalPad)
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
            else if (current.x == 1 && dest.y == 0)
            {
                output.Append(vertical);
                output.Append(horizontal);
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

}
