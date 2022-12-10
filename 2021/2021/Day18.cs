namespace Advent2021;
public class Day18
{
    public static long CompleteHomework(string filename)
    {
        var sum = ReduceFile(filename);
        return CalculateMagnitude(sum);
    }

    public static string ReduceFile(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var start = lines.First();
        foreach (var line in lines.Skip(1))
        {
            start = Sum(start, line);
            start = Reduce(start);
        }
        return start;

    }

    public static long CalculateMagnitude(string input)
    {
        if (input.All(_ => char.IsDigit(_)))
        {
            return long.Parse(input);
        }
        var result = 0;
        var stack = new Stack<(int index, char bracket)>();
        for (int i = 0; i < input.Length; i++)
        {
            var ch = input[i];
            switch (ch)
            {
                case '[':
                    stack.Push((i, ch));
                    break;
                case ']':
                    var (ei, eb) = stack.Pop();
                    var stringLeft = input[..ei];
                    var stringRight = input[(i + 1)..];
                    var pair = input[(ei + 1)..i];
                    var (left, right) = SplitIntoPair(pair);
                    if (left.All(_ => char.IsDigit(_)) && right.All(_ => char.IsDigit(_)))
                    {
                        result = (int.Parse(left) * 3) + (int.Parse(right) * 2);
                        return CalculateMagnitude($"{stringLeft}{result}{stringRight}");
                    }
                    break;
            }
        }
        return 0;
    }

    public static string Sum(string first, string second) =>
        $"[{first},{second}]";

    public static string Reduce(string input)
    {
        var isDone = false;
        while (!isDone)
        {
            var result = CheckReduce(input);
            var beforeSplit = result;
            if (input == result)
            {
                result = CheckReduce(result, false);
                if (beforeSplit == result)
                {
                    return result;
                }
            }
            input = result;
        }
        return input;
    }

    public static string CheckReduce(string input, bool explodes = true)
    {
        var regexEnd = new Regex(@"(\d+)(?!.*\d)");
        var regexStart = new Regex(@"(\d+)");
        var result = string.Empty;
        var stack = new Stack<(int index, char bracket)>();
        if (explodes)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var ch = input[i];
                switch (ch)
                {
                    case '[':
                        stack.Push((i, ch));
                        break;
                    case ']':
                        var (ei, eb) = stack.Pop();
                        if (stack.Count == 4)
                        {
                            var pair = input[(ei + 1)..i];
                            var left = int.Parse(pair.Split(',')[0]);
                            var right = int.Parse(pair.Split(',')[1]);
                            var newLeft = ExplodeLeft(input[..ei], left);
                            var newRight = ExplodeRight(input[(i + 1)..], right);
                            if (!newLeft.EndsWith(','))
                            {
                                if (newLeft.EndsWith('['))
                                {
                                    newLeft = $"{newLeft}0,";
                                }
                                else
                                {
                                    newLeft = $"{newLeft},";
                                }
                            }
                            else if (!newRight.StartsWith('0'))
                            {
                                newRight = $"0{newRight}";
                            }
                            return $"{newLeft}{newRight}";
                        }
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < input.Length; i++)
            {
                var next = regexStart.Match(input[i..]);
                var d = next.Success ? int.Parse(next.Value) : -1;

                if (next.Success && next.Index == 0 && d > 9)
                {
                    var left = input[..i];
                    var newPair = $"[{d / 2},{(d / 2) + (d % 2)}]";
                    var right = ReplaceFirstOccurrence(input[i..], next.Value, "");
                    if (!right.StartsWith(',') && !right.StartsWith(']'))
                    {
                        right = $",{right}";
                    }
                    return $"{left}{newPair}{right}";
                }
            }
        }

        return input;

    }

    public static (string left, string right) SplitIntoPair(string pair)
    {
        if (!pair.Contains('['))
        {
            var result = pair.Split(',');
            return (result[0], result[1]);
        }
        var stack = new Stack<(int index, char br)>();
        for (int i = 0; i < pair.Length; i++)
        {
            var c = pair[i];
            switch (c)
            {
                case '[':
                    stack.Push((i, c));
                    break;
                case ']':
                    var (ind, br) = stack.Pop();
                    if (i == pair.Length - 1)
                    {
                        return (pair[..(ind - 1)], pair[ind..]);
                    }
                    else if (pair[i + 1] == ',')
                    {
                        return (pair[..(i + 1)], pair[(i + 2)..]);
                    }
                    break;
            }
        }
        return (string.Empty, string.Empty);
    }

    public static string ExplodeLeft(string v, int digit)
    {
        var regexStart = new Regex(@"(\d+)");
        for (int i = v.Length - 1; i > 0; i--)
        {
            var index = i;
            while (index > 0)
            {
                if (char.IsDigit(v[index]))
                {
                    index--;
                }
                else
                {
                    break;
                }
            }
            var d = regexStart.Match(v[index..]);
            if (d.Success)
            {
                var newText = ReplaceFirstOccurrence(v[(index + 1)..], d.Value, "");
                var newDigit = digit + (int.Parse(d.Value));
                var result = $"{newDigit}{newText}";
                return $"{v[..(index + 1)]}{result}";
            }
        }
        return $"{v}0";
    }

    public static string ExplodeRight(string v, int digit)
    {
        for (int i = 0; i < v.Length; i++)
        {
            var regexStart = new Regex(@"(\d+)");
            var d = regexStart.Match(v[i..]);
            if (d.Success)
            {
                var newText = ReplaceFirstOccurrence(v[d.Index..], d.Value, "");
                var newDigit = digit + (int.Parse(d.Value));
                var prefix = v[..d.Index].StartsWith(',') ? v[1..d.Index] : v[..d.Index];
                var result = $"{prefix}{newDigit}{newText}";
                return result;
            }
        }
        return $"0{v}";
    }

    public static long GetLargestMagnitued(string filename)
    {
        long result = 0;
        var lines = File.ReadAllLines(filename).ToList();
        foreach (var l in lines)
        {
            var checker = l;
            foreach (var line in lines.Where(_ => _ != checker))
            {
                var added = Sum(checker, line);
                var red = Reduce(added);
                var mag = CalculateMagnitude(red);
                if (mag > result)
                {
                    result = mag;
                }
            }
        }

        return result;
    }

    private static string ReplaceFirstOccurrence(string source, string find, string replace)
    {
        int place = source.IndexOf(find);
        string result = source.Remove(place, find.Length).Insert(place, replace);
        return result;
    }

    private static (int digit, int endIndex) GetDigit(int index, string v) =>
        (int.Parse(v[index..(index + 1)]), index + 1);
}
