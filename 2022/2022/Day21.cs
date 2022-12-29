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

    public static double SolvePart2(string filename, IPrinter printer)
    {
        var monkeys = ParseInput(filename)!;
        var value = 0L;
        var low = 0L;
        var high = 1000000000000L;
        var humn = monkeys.First(_ => _.Value.Name == "humn");
        while (true)
        {
            var mid = Math.Floor((low + high) / 2.0);
            humn.Value.Value = mid;
            var result = GetMonkeyValue(monkeys["root"], monkeys);
        }
        return value;
    }
    private static double GetMonkeyValue(ScreamMonkey monkey, Dictionary<string, ScreamMonkey> monkeys)
    {
        return monkey.Operator switch
        {
            Operator.Add => GetMonkeyValue(monkeys[monkey.Left!], monkeys) + GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Multiply => GetMonkeyValue(monkeys[monkey.Left!], monkeys) * GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Subtract => GetMonkeyValue(monkeys[monkey.Left!], monkeys) - GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            Operator.Divide => GetMonkeyValue(monkeys[monkey.Left!], monkeys) / GetMonkeyValue(monkeys[monkey.Right!], monkeys),
            _ => monkey.Value!.Value
        };
    }

    private static (double value, bool success) GetMonkeyValue2(ScreamMonkey monkey, ref Dictionary<string, ScreamMonkey> monkeys)
    {
        if (monkey.Left == "humn" || monkey.Right == "humn")
        {
            return (0, false);
        }
        if (monkey.Operator == Operator.None)
        {
            return (monkey.Value!.Value, true);
        }
        var (l, lS) = GetMonkeyValue2(monkeys[monkey.Left!], ref monkeys);
        var (r, rS) = GetMonkeyValue2(monkeys[monkey.Right!], ref monkeys);
        if (lS && rS)
        {
            var value = monkey.Operator switch
            {
                Operator.Add => l + r,
                Operator.Multiply => l * r,
                Operator.Subtract => l - r,
                Operator.Divide => l / r,
                _ => monkey.Value!.Value
            };
            monkey.Left = null; ;
            monkey.Right = null;
            monkey.Value = value;
            return (value, true);
        }
        return (0, false);

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
