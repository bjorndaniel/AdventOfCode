﻿namespace AoC2022;
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
        if (!string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right))
        {
            return false;
        }
        if (left.StartsWith('[') && !right.StartsWith('['))
        {
            return true;
        }
        if (!left.StartsWith('[') && !right.StartsWith('[') && left.IndexOf(']') < right.IndexOf(']'))
        {
            return false;
        }

        var (firstLeft, remainLeft) = GetFirst(left);
        var (firstRight, remainRight) = GetFirst(right);
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
        if (remainRight.StartsWith("[[") && !remainLeft.StartsWith("[["))
        {
            return false;
        }
        return ComparePair(new Pair(remainLeft, remainRight));
        static (int[] first, string rest) GetFirst(string input)
        {
            if (!input.Contains('['))
            {
                return (input.Split(',').Select(_ => int.Parse(_)).ToArray(), string.Empty);
            }
            if (input.Count(_ => _ == '[') == 1 && input.EndsWith(']'))
            {
                if (input.Length == 2)
                {
                    return (new int[0], string.Empty);
                }
                return (input.Trim('[').Trim(']').Split(',').Select(_ => int.Parse(_)).ToArray(), string.Empty);
            }
            if (!input.StartsWith('['))
            {
                var first = input[0..input.IndexOf(',')];
                var rest = input[input.IndexOf(',')..].Trim(',');
                return (new int[] { int.Parse(first.Trim(']')) }, rest);
            }
            else if (input.IndexOf(',') < 0)
            {
                return (new int[0], input[1..^1]);
            }
            else
            {
                var left = input[1..input.IndexOf(',')];
                var rest = input[input.IndexOf(',')..].Trim(',');
                if (left.EndsWith(']'))
                {
                    left = left.Trim(']');
                }
                else
                {
                    rest = rest.Remove(rest.LastIndexOf(']'), 1);
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
            return (null, null);
        }

        //if (left.StartsWith('['))
        //{
        //    if (!right.StartsWith('['))
        //    {
        //        var index = right.IndexOf(",");
        //        if (index < 1)
        //        {
        //            right = $"[{right}]";
        //        }
        //        else
        //        {
        //            right = $"[{right[1..index]}]{right[(index + 1)..]}";
        //        }
        //    }
        //}
        //else if (right.StartsWith('['))
        //{
        //    if (!left.StartsWith('['))
        //    {
        //        var index = left.IndexOf(",");
        //        if (index < 1)
        //        {
        //            left = $"[{left}]";
        //        }
        //        else
        //        {
        //            left = $"[{left[1..index]}]{left[(index + 1)..]}";
        //        }
        //    }
        //}

        var (newLeft, nextLeft) = GetNext(left);
        var (newRight, nextRight) = GetNext(right);
        if (newLeft.ValueInt < newRight.ValueInt)
        {
            return true;
        }
        if (newLeft.GetValueAt(0) < newRight.GetValueAt(0))
        {
            return true;
        }
        if (newLeft.GetValueAt(0) > newRight.GetValueAt(0))
        {
            return false;
        }
        //if (compareL == null && compareR == null)
        //{
        //    return true;
        //}
        //if (compareL != null && compareR == null)
        //{
        //    return false;
        //}
        //if (compareL == null && compareR != null)
        //{
        //    return true;
        //}
        //for (int i = 0; i < compareL.Length; i++)
        //{

        //    if (compareL[i] < compareR[i])
        //    {
        //        return true;
        //    }
        //    if (compareL[i] > compareR[i])
        //    {
        //        return false;
        //    }
        //}
        //if (compareL.HasValue && !compareR.HasValue)
        //{
        //    return false;
        //}
        //if (!compareL.HasValue && !compareR.HasValue)
        //{
        //    return false;
        //}
        //if (!compareL.HasValue && compareR.HasValue)
        //{
        //    return false;
        //}
        //if (compareL < compareR)
        //{
        //    return true;
        //}
        //if (compareR < compareL)
        //{
        //    return false;
        //}
        return (ComparePair(new Pair(nextLeft, nextRight)));
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
