namespace AoC2022;
public static class Day21
{
    public static Dictionary<string, ScreamMonkey> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new Dictionary<string, ScreamMonkey>();
        foreach (var l in lines)
        {
            var name = l.Split(':')[0];
            if (int.TryParse(l.Split(':')[1], out var value))
            {
                result.Add(name, new ScreamMonkey(
                    name,
                    value,
                    string.Empty,
                    string.Empty,
                    Operator.None
                ));
            }
            else
            {
                var right = l.Split(':')[1];
                var vals = right.Split(' ', StringSplitOptions.TrimEntries);
                result.Add(name, new ScreamMonkey(
                    name,
                    null,
                    vals[1],
                    vals[3],
                    GetOperator(vals[2])
                ));
            }
        }
        return result;

        static Operator GetOperator(string input) =>
            input switch
            {
                "+" => Operator.Add,
                "-" => Operator.Subtract,
                "/" => Operator.Divide,
                "*" => Operator.Multiply,
                _ => Operator.None,
            };
    }

    public static double SolvePart1(string filename)
    {
        var monkeys = ParseInput(filename);
        return GetMonkeyValue(monkeys["root"], monkeys);
    }

    public static long SolvePart2(string filename, IPrinter printer)
    {
        var monkeys = ParseInput(filename)!;

        var low = 0D;
        var high = 10000000000D;
        var humn = monkeys.First(_ => _.Value.Name == "humn");
        var root = monkeys.First(_ => _.Value.Name == "root");
        root.Value.Operator = Operator.Equals;
        while (true)
        {
            var mid = Math.Floor((low + high) / 2);
            humn.Value.Value = mid;
            var resultL = GetMonkeyValue(monkeys[root.Value.Left!], monkeys);
            var resultR = GetMonkeyValue(monkeys[root.Value.Right!], monkeys);

            if (resultL == resultR)
            {
                return (long)humn.Value.Value.Value;
            }
            if (resultL < resultR)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }
    }
    private static double GetMonkeyValue(ScreamMonkey monkey, Dictionary<string, ScreamMonkey> monkeys)
    {
        return monkey.Operator switch
        {
            Operator.Add => GetMonkeyValue(monkeys[monkey.Left!], monkeys) + GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Multiply => GetMonkeyValue(monkeys[monkey.Left!], monkeys) * GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Subtract => GetMonkeyValue(monkeys[monkey.Left!], monkeys) - GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Divide => GetMonkeyValue(monkeys[monkey.Left!], monkeys) / GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Equals => GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            _ => monkey.Value!.Value
        };
    }

}

public class ScreamMonkey
{
    public ScreamMonkey(string name, double? value, string? left, string? right, Operator op)
    {
        Name = name;
        Value = value;
        Left = left;
        Right = right;
        Operator = op;
    }

    public string Name { get; }
    public double? Value { get; set; }
    public string? Left { get; set; }
    public string? Right { get; set; }
    public Operator Operator { get; set; }
}


public enum Operator
{
    None,
    Add,
    Subtract,
    Multiply,
    Divide,
    Equals
}
