
namespace AoC2022;
public static class Day13
{

    public static List<Pair> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Pair>();
        for (int i = 0; i < lines.Length - 1; i++)
        {
            if (string.IsNullOrEmpty(lines[i]))
            {
                continue;
            }

            var left = lines[i][0..];
            var right = lines[i + 1][0..];
            i++;
            result.Add(new Pair(left, right));
        }
        return result;
    }

    public static int SolvePart1(string filename)
    {
        var pairs = ParseInput(filename);
        var indices = new List<int>();
        var counter = 1;
        foreach (var p in pairs)
        {

            if (ComparePair(p))
            {
                indices.Add(counter);
            }
            counter++;
        }

        return indices.Sum();
    }

    public static int SolvePart2(string filename)
    {
        var pairs = ParseInput(filename);
        pairs.Add(new Pair("[2]", "[6]"));

        var all = pairs.Select(_ => _.Left).ToList();
        all.AddRange(pairs.Select(_ => _.Right));
        var ordered = all.OrderBy(_ => _, new Comparer()).ToList();
        var index1 = ordered.IndexOf("[2]") + 1;
        var index2 = ordered.IndexOf("[6]") + 1;
        return (index1 * index2);
    }

    public static bool ComparePair(Pair pair)
    {
        var (left, right) = pair.GetJsonElement();
        if (IsEmptyArray(left) && !IsEmptyArray(right))
        {
            return true;
        }
        if (!IsEmptyArray(left) && IsEmptyArray(right))
        {
            return false;
        }
        if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Number)
        {
            if (left.GetInt32() < right.GetInt32())
            {
                return true;
            }
            if (left.GetInt32() > right.GetInt32())
            {
                return false;
            }
            return ComparePair(pair.RemoveFirst());
        }
        if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Array)
        {
            pair.Left = $"[{pair.Left}]";
            return ComparePair(pair);
        }
        if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Number)
        {
            pair.Left = $"[{pair.Right}]";
            return ComparePair(pair);
        }
        if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Array)
        {
            var x = left.EnumerateArray().First();
            var y = right.EnumerateArray().First();
            if (x.GetInt32() < y.GetInt32())
            {
                return true;
            }
            if (x.GetInt32() > y.GetInt32())
            {
                return false;
            }
            return ComparePair(pair.RemoveFirst());
        }
        return false;
        static bool IsEmptyArray(JsonElement element) =>
            element.ValueKind == JsonValueKind.Array && element.GetArrayLength() == 0;
    }

    //public static bool ComparePairOLD(Pair pair)
    //{
    //    var left = pair.Left;
    //    var right = pair.Right;
    //    if (string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
    //    {
    //        return true;
    //    }
    //    if (string.IsNullOrEmpty(right) && !string.IsNullOrEmpty(left))
    //    {
    //        return false;
    //    }
    //    if (left.StartsWith("[[") && !right.StartsWith("[["))
    //    {
    //        var (fl, rl) = GetFirst(left);
    //        var (fr, rr) = GetFirst(right);
    //        if (fl.Length == 0 && fr.Length == 0)
    //        {
    //            if (rr.Length == 0 && rl.Length != 0)
    //            {
    //                return false;
    //            }
    //            if (rl.StartsWith("],"))
    //            {
    //                rl = rl[2..];
    //            }
    //            if (rr.StartsWith("],"))
    //            {
    //                rl = rl[2..];
    //            }
    //            return ComparePair(new Pair(rl, rr));
    //        }
    //        if (fl.Length == 0 && fr.Length != 0)
    //        {
    //            return true;
    //        }
    //        if (fl.Length > 0 && fr.Length > 0)
    //        {

    //            if (fl[0] < fr[0])
    //            {
    //                return true;
    //            }
    //            return false;
    //        }

    //        if (rl.Length > rr.Length)
    //        {
    //            return false;
    //        }

    //    }
    //    var (firstLeft, remainLeft) = GetFirst(left);
    //    var (firstRight, remainRight) = GetFirst(right);
    //    if (string.IsNullOrEmpty(remainLeft) && string.IsNullOrEmpty(remainRight))
    //    {
    //        if (!firstLeft.Any() && !firstRight.Any())
    //        {
    //            return true;
    //        }
    //    }

    //    if (firstLeft.Any() && !firstRight.Any())
    //    {
    //        return false;
    //    }
    //    if (firstLeft.Any() && firstRight.Any() && firstLeft[0] < firstRight[0])
    //    {
    //        return true;
    //    }
    //    if (!firstLeft.Any() && firstRight.Any())
    //    {
    //        return true;
    //    }

    //    if (remainLeft.StartsWith(']') && !remainRight.StartsWith(']'))
    //    {
    //        if (firstLeft.Any() && firstRight.Any() && firstLeft[0] > firstRight[0])
    //        {
    //            return false;
    //        }
    //        if (remainLeft.EndsWith(']') && !remainRight.Contains(']'))
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //    if (remainRight.StartsWith(']') && !remainLeft.StartsWith(']') && !remainLeft.StartsWith('[') && remainLeft.Contains(']'))
    //    {
    //        return false;
    //    }

    //    if (remainRight.StartsWith("[[") && !remainLeft.StartsWith("[[") && !char.IsDigit(remainLeft[0]))
    //    {
    //        return false;
    //    }

    //    for (int i = 0; i < firstLeft.Length; i++)
    //    {
    //        if (i >= firstRight.Length)
    //        {
    //            return false;
    //        }
    //        if (firstLeft[i] > firstRight[i])
    //        {
    //            return false;
    //        }
    //        if (firstLeft[i] < firstRight[i])
    //        {
    //            return true;
    //        }
    //    }
    //    if (firstRight.Length > firstLeft.Length)
    //    {
    //        return true;
    //    }
    //    if (remainLeft.StartsWith("],"))
    //    {
    //        remainLeft = remainLeft[2..];
    //    }
    //    if (remainRight.StartsWith("],"))
    //    {
    //        remainRight = remainRight[2..];
    //    }
    //    if (remainLeft.StartsWith(","))
    //    {
    //        remainLeft = remainLeft[1..];
    //    }
    //    if (remainRight.StartsWith(","))
    //    {
    //        remainRight = remainRight[1..];
    //    }
    //    return ComparePair(new Pair(remainLeft, remainRight));

    //    static (int[] first, string rest) GetFirst(string input)
    //    {
    //        if (string.IsNullOrEmpty(input))
    //        {
    //            return (new int[0], string.Empty);
    //        }
    //        if (!input.Contains('['))
    //        {
    //            for (int i = 0; i < input.Length; i++)
    //            {
    //                if (char.IsDigit(input[i]))
    //                {
    //                    return (new int[] { int.Parse(input[i].ToString()) }, input[(i + 1)..]);
    //                }
    //            }

    //            return (new int[0], string.Empty);

    //            // return (input.TrimStart(']').Split(',', StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_.Trim(']'))).ToArray(), string.Empty);
    //        }
    //        if (input.Count(_ => _ == '[') == 1 && input.EndsWith(']'))
    //        {
    //            if (input.Length == 2)
    //            {
    //                return (new int[0], string.Empty);
    //            }
    //            if (input.Count(_ => _ == ']') == 1 && input.Contains(','))
    //            {
    //                if (input.StartsWith(','))
    //                {
    //                    return (new int[0], input[1..]);
    //                }
    //                var left = input[1..input.IndexOf(',')];
    //                var right = input[input.IndexOf(',')..];
    //                return (new int[] { int.Parse(left) }, right);
    //            }
    //            var value = input.Trim('[').Trim(']').Split(',', StringSplitOptions.RemoveEmptyEntries)
    //                .Select(_ => _.Trim(']').Trim('['));
    //            if (value.Any() && value.All(_ => _.Any() && _.All(_ => char.IsDigit(_))))
    //            {
    //                return (value.Select(_ => int.Parse(_)).ToArray(), string.Empty);
    //            }
    //            return (new int[0], string.Empty);


    //            //.Select(_ => int.Parse(_.Trim(']').Trim('['))).ToArray(), string.Empty);  
    //        }
    //        if (!input.StartsWith('['))
    //        {
    //            var first = input[0..input.IndexOf(',')];
    //            var rest = input[input.IndexOf(',')..];

    //            if (first.EndsWith(']'))
    //            {
    //                rest = $"]{rest}";
    //            }
    //            else
    //            {
    //                rest = rest.Trim(',');
    //            }

    //            if (int.TryParse(first?.Trim(']') ?? "", out var result))
    //            {
    //                return (new int[] { result }, rest);

    //            }
    //            return (new int[0], rest);
    //        }
    //        else if (input.IndexOf(',') < 0)
    //        {
    //            return (new int[0], input[1..^1]);
    //        }
    //        else
    //        {
    //            var left = input[1..input.IndexOf(',')];
    //            var rest = input[input.IndexOf(',')..];
    //            if (left.EndsWith(']'))
    //            {
    //                left = left.Trim(']');
    //                rest = $"]{rest}";
    //            }
    //            else if (left.Contains(']'))
    //            {
    //                rest = rest.Trim(',').Remove(rest.LastIndexOf(']'), 1);
    //            }
    //            if (string.IsNullOrWhiteSpace(left))
    //            {
    //                return (new int[0], rest);
    //            }
    //            if (int.TryParse(left.Trim('['), out var x))
    //            {
    //                return (new int[] { x }, rest);
    //            }
    //            else
    //            {
    //                return (new int[0], rest);
    //            }
    //        }
    //    }

    //}
}

public class Comparer : IComparer<string>
{
    public int Compare(string? a, string? b)
    {
        var p = new Pair(a!, b!);
        var r = Day13.ComparePair(p);
        return r ? -1 : 1;
    }
}

public class Part
{
    public string Raw { get; set; } = "";
    public int ValueInt =>
        Raw.All(_ => char.IsDigit(_)) ? int.Parse(Raw) : 0;
    public bool IsDigit => Raw.All(_ => char.IsDigit(_));
    public bool IsEmptyArray => Raw.Length == 2 && Raw.Contains('[') && Raw.Contains(']');
    public bool IsArray => Raw.StartsWith('[');
    public int GetValueAt(int index)
    {
        if (IsDigit)
        {
            return ValueInt;
        }
        var array = Raw.Trim('[').Trim(']');
        if (array.Contains(','))
        {
            var digits = array.Split(',');
            if (digits.Length > index)
            {
                return int.Parse(digits[index]);
            }
        }
        return 0;
    }
}

public class Pair
{
    public string Left { get; set; }
    public string Right { get; set; }

    public Pair(string left, string right)
    {
        Left = left;
        Right = right;
    }


    public Pair ReOrder() =>
        new(Right, Left);

    public (JsonElement left, JsonElement right) GetJsonElement() =>
        ((JsonElement)JsonSerializer.Deserialize<object>(Left)!, (JsonElement)JsonSerializer.Deserialize<object>(Right)!);

    public Pair RemoveFirst()
    {
        var (left, right) = GetJsonElement();
        var newl = left.EnumerateArray().Skip(1);
        var newR = right.EnumerateArray().Skip(1);
        return new Pair(JsonSerializer.Serialize(newl), JsonSerializer.Serialize(newR));
    }
}

public class Packet
{
    public Packet(string raw)
    {
        Raw = raw;
    }

    public string Raw { get; private set; }

    public override bool Equals(object? obj)
    {
        var p = (Packet)obj!;
        return Day13.ComparePair(new Pair(Raw, p.Raw));
    }

    public override int GetHashCode() =>
        base.GetHashCode();

}
