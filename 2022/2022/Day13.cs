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

            var left = lines[i][1..^1];
            var right = lines[i + 1][1..^1];
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

    public static bool ComparePair(Pair pair)
    {
        var left = pair.Left;
        var right = pair.Right;
        if (string.IsNullOrEmpty(left) && !string.IsNullOrEmpty(right))
        {
            return true;
        }
        if (left.StartsWith("[[") && !right.StartsWith("[["))
        {
            var (fl, rl) = GetFirst(left);
            var (fr, rr) = GetFirst(right);
            if (fl.Length == 0 && fr.Length == 0)
            {
                if (rr.Length == 0 && rl.Length != 0)
                {
                    return false;
                }
                if (rl.StartsWith("],"))
                {
                    rl = rl[2..];
                }
                if (rr.StartsWith("],"))
                {
                    rl = rl[2..];
                }
                return ComparePair(new Pair(rl, rr));
            }
            if (fl.Length == 0 && fr.Length != 0)
            {
                return true;
            }
            if (fl.Length > 0 && fr.Length > 0)
            {

                if (fl[0] < fr[0])
                {
                    return true;
                }
                return false;
            }

            if (rl.Length > rr.Length)
            {
                return false;
            }

        }
        var (firstLeft, remainLeft) = GetFirst(left);
        var (firstRight, remainRight) = GetFirst(right);

        if (firstLeft.Any() && !firstRight.Any())
        {
            return false;
        }
        if (firstLeft.Any() && firstRight.Any() && firstLeft[0] < firstRight[0])
        {
            return true;
        }
        

        if (remainLeft.StartsWith(']') && !remainRight.StartsWith(']'))
        {
            if (firstLeft.Any() && firstRight.Any() && firstLeft[0] > firstRight[0])
            {
                return false;
            }
            return true;
        }
        if (remainRight.StartsWith(']') && !remainLeft.StartsWith(']'))
        {
            return false;
        }

        if (remainRight.StartsWith("[[") && !remainLeft.StartsWith("[[") && !char.IsDigit(remainLeft[0]))
        {
            return false;
        }

        for (int i = 0; i < firstLeft.Length; i++)
        {
            if (i >= firstRight.Length)
            {
                return false;
            }
            if (firstLeft[i] > firstRight[i])
            {
                return false;
            }
            if (firstLeft[i] < firstRight[i])
            {
                return true;
            }
        }
        if (firstRight.Length > firstLeft.Length)
        {
            return true;
        }
        if (remainLeft.StartsWith("],"))
        {
            remainLeft = remainLeft[2..];
        }
        if (remainRight.StartsWith("],"))
        {
            remainRight = remainRight[2..];
        }
        return ComparePair(new Pair(remainLeft, remainRight));

        static (int[] first, string rest) GetFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return (new int[0], string.Empty);
            }
            if (!input.Contains('['))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        return (new int[] { int.Parse(input[i].ToString()) }, input[(i + 1)..]);
                    }
                }

                return (new int[0], string.Empty);

                // return (input.TrimStart(']').Split(',', StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_.Trim(']'))).ToArray(), string.Empty);
            }
            if (input.Count(_ => _ == '[') == 1 && input.EndsWith(']'))
            {
                if (input.Length == 2)
                {
                    return (new int[0], string.Empty);
                }
                return (input.Trim('[').Trim(']').Split(',', StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_.Trim(']').Trim('['))).ToArray(), string.Empty);
            }
            if (!input.StartsWith('['))
            {
                var first = input[0..input.IndexOf(',')];
                var rest = input[input.IndexOf(',')..];

                if (first.EndsWith(']'))
                {
                    rest = $"]{rest}";
                }
                else
                {
                    rest = rest.Trim(',');
                }

                if (int.TryParse(first?.Trim(']') ?? "", out var result))
                {
                    return (new int[] { result }, rest);

                }
                return (new int[0], rest);
            }
            else if (input.IndexOf(',') < 0)
            {
                return (new int[0], input[1..^1]);
            }
            else
            {
                var left = input[1..input.IndexOf(',')];
                var rest = input[input.IndexOf(',')..];
                if (left.EndsWith(']'))
                {
                    left = left.Trim(']');
                    rest = $"]{rest}";
                }
                else if (left.Contains(']'))
                {
                    rest = rest.Trim(',').Remove(rest.LastIndexOf(']'), 1);
                }
                if (string.IsNullOrWhiteSpace(left))
                {
                    return (new int[0], rest);
                }
                if (int.TryParse(left.Trim('['), out var x))
                {
                    return (new int[] { x }, rest);
                }
                else
                {
                    return (new int[0], rest);
                }
            }
        }

    }

    static (Part left, string right) GetNext(string input)
    {
        if (input.StartsWith('['))
        {
            var left = input[0..(input.LastIndexOf(']') + 1)];
            var right = input[input.LastIndexOf(']')..].Trim(',');
            return (new Part { Raw = left }, right);
        }
        if (input.Contains(','))
        {

            var left = input[0..input.IndexOf(',')];
            var right = input[input.IndexOf(',')..];
            return (new Part { Raw = left }, right.Trim(','));
        }
        else
        {
            return (new Part { Raw = input }, string.Empty);
        }
        return (null, null);

        //if (string.IsNullOrEmpty(input))
        //{
        //    return ("", null);
        //}
        //var newInput = string.Empty;
        //int[]? comparer = null;
        //if (input.All(_ => _ == '[' || _ == ']'))
        //{
        //    return (input[1..^1], new int[0]);
        //}
        //if (input.All(_ => char.IsDigit(_)))
        //{
        //    comparer = new int[] { int.Parse(input) };
        //}
        //else
        //{
        //    var left = "";
        //    if (input.IndexOf(',') < 0)
        //    {
        //        left = input.Trim('[').Trim(']');
        //    }
        //    else
        //    {
        //        left = input[0..input.IndexOf(',')].Trim('[').Trim(']');
        //    }
        //    newInput = input[(input.IndexOf(',') + 1)..];
        //    if (string.IsNullOrEmpty(left))
        //    {
        //        comparer = null;
        //    }
        //    else if (left.All(_ => char.IsDigit(_)))
        //    {
        //        comparer = new int[] { int.Parse(left) };
        //    }
        //}
        //return (newInput, comparer);
    }

    static (string newString, int? comparer) GetNextDigit(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return ("", null);
        }
        var newInput = input;
        int? comparer = null;
        if (input.All(_ => _ == '[' || _ == ']'))
        {
            return (input[1..^1], null);
        }
        if (input.StartsWith("["))
        {
            var index = input.IndexOf(",");
            if (index < 1)
            {
                index = input.IndexOf("]");
                newInput = input[(index + 1)..];
                comparer = index < 2 ? null : int.Parse(input[1..index].Trim(']'));
            }
            else
            {
                newInput = input[(index + 1)..];
                var content = input[1..index].Trim(']').Trim('[');
                comparer = string.IsNullOrWhiteSpace(content) ? null : int.Parse(content.Trim('['));
            }
        }
        else
        {
            var index = input.IndexOf(",");
            if (index == -1)
            {
                comparer = int.Parse(input.Trim(']'));
                newInput = string.Empty;
            }
            else
            {
                comparer = int.Parse(input[..index].Trim(']'));
                newInput = input[(index + 1)..];
            }
        }

        return (newInput, comparer);
    }
    
}

public class Part
{
    public string Raw { get; set; }
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

public record Pair(string Left, string Right);
