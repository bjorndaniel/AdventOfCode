namespace AoC2022;
public static class Day25
{
    public static IEnumerable<SNAFU> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            yield return new SNAFU(l);
        }
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
        return (int)result;
    }

    public static SNAFU FromDecimal(long decimalValue)
    {
        var length = decimalValue / 5;
        var mod = (int)(decimalValue % 5);
        if (length < 1)
        {
            if (decimalValue < 3)
            {
                return new SNAFU(decimalValue.ToString());
            }
            switch (decimalValue)
            {
                case 3:
                    return new SNAFU("1=");
                case 4:
                    return new SNAFU("1-");
                default:
                    return new SNAFU("10");
            }
        }
        var result = Multipliers[mod - 1].ToString();
        return new SNAFU("result");
    }

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
        6103515625
    };
}