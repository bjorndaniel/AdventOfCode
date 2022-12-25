
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

            if (ComparePair(p) == CompareResult.True)
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

    public static CompareResult ComparePair(Pair pair)
    {
        var (left, right) = pair.GetJsonElement();
        if (IsEmptyArray(left) && !IsEmptyArray(right))
        {
            return CompareResult.True;
        }
        if (!IsEmptyArray(left) && IsEmptyArray(right))
        {
            return CompareResult.False;
        }
        if (IsEmptyArray(left) && IsEmptyArray(right))
        {
            return CompareResult.Equals;
        }
        if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Number)
        {
            if (left.GetInt32() < right.GetInt32())
            {
                return CompareResult.True;
            }
            if (left.GetInt32() > right.GetInt32())
            {
                return CompareResult.False;
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
            pair.Right = $"[{pair.Right}]";
            return ComparePair(pair);
        }
        if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Array)
        {
            var x = left.EnumerateArray().First();
            var y = right.EnumerateArray().First();
            if (x.ValueKind == JsonValueKind.Number && y.ValueKind == JsonValueKind.Number)
            {
                if (x.GetInt32() < y.GetInt32())
                {
                    return CompareResult.True;
                }
                if (x.GetInt32() > y.GetInt32())
                {
                    return CompareResult.False;
                }
                return ComparePair(pair.RemoveFirst());
            }
            var result = ComparePair(new Pair(JsonSerializer.Serialize(x), JsonSerializer.Serialize(y)));
            if (result != CompareResult.Equals)
            {
                return result;
            }
            return ComparePair(pair.RemoveFirst());
        }
        return CompareResult.False;
        static bool IsEmptyArray(JsonElement element) =>
            element.ValueKind == JsonValueKind.Array && !element.EnumerateArray().Any();
    }

}

public class Comparer : IComparer<string>
{
    public int Compare(string? a, string? b)
    {
        var p = new Pair(a!, b!);
        var r = Day13.ComparePair(p);
        return r == CompareResult.True ? -1 : 1;
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

    public (JsonElement left, JsonElement right) GetJsonElement() =>
        ((JsonElement)JsonSerializer.Deserialize<object>(Left)!, (JsonElement)JsonSerializer.Deserialize<object>(Right)!);

    public Pair RemoveFirst()
    {
        var (left, right) = GetJsonElement();
        var newL = left.EnumerateArray().Skip(1);
        var newR = right.EnumerateArray().Skip(1);

        return new Pair(JsonSerializer.Serialize(newL), JsonSerializer.Serialize(newR));
    }
}

public enum CompareResult
{
    True,
    False,
    Equals
}
