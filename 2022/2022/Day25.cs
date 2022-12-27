namespace AoC2022;
public static class Day25
{
    public static IEnumerable<SNAFU> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var length = lines.Max(_ => _.Length);
        foreach (var l in lines)
        {
            yield return new SNAFU(l.PadLeft(length, '0'));
        }
    }

    public static string SolvePart1(string filename)
    {
        var snafus = ParseInput(filename);
        var sum = 0L;
        foreach (var snafu in snafus)
        {
            sum += snafu.ToDecimal();
        }
        return SNAFU.FromDecimal(sum).Value;
    }
}

public record SNAFU(string Value)
{
    public long ToDecimal()
    {
        if (Value.Length == 1)
        {
            return long.Parse(Value);
        }

        var result = 0L;
        for (int i = 0; i < Value.Length; i++)
        {
            switch (Value[i])
            {
                case '2':
                    result += 2 * Multipliers[Value.Length - i - 1];
                    break;
                case '1':
                    result += Multipliers[Value.Length - i - 1];
                    break;
                case '0':
                    break;
                case '-':
                    result -= Multipliers[Value.Length - i - 1];
                    break;
                case '=':
                    result -= 2 * Multipliers[Value.Length - i - 1];
                    break;
                default:
                    throw new ArgumentException("Invalid character in string");
            }

        }
        return result;
    }

    public static SNAFU FromDecimal(long decimalValue)
    {
        var result = "";
        var n = decimalValue;
        while (n != 0)
        {
            var rem = n % 5;
            var dig = Converter[rem];
            result += dig;
            n = (n + 2) / 5;
        }
        return new SNAFU(result.Reverse().Select(_ => _.ToString()).Aggregate((a, b) => $"{a}{b}"));
    }

    private static Dictionary<long, char> Converter = new Dictionary<long, char>
    {
        { 3, '='},
        {4, '-' },
        {0, '0' },
        {1 , '1' },
        {2, '2' }
    };

    private static List<long> Multipliers = new List<long>
    {
        1,
        5,
        25,
        125,
        625,
        3125,
        15625,
        78125,
        390625,
        1953125,
        9765625,
        48828125,
        244140625,
        1220703125,
        6103515625,
        30517578125,
        152587890625,
        762939453125,
        3814697265625,
        19073486328125,
        95367431640625
    };
}