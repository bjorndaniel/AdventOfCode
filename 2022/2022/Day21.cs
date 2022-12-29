using System.Runtime.Intrinsics.Arm;

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

    public static long SolvePart1(string filename)
    {
        var monkeys = ParseInput(filename);
        return GetMonkeyValue(monkeys["root"], monkeys);
    }

    public static long SolvePart2(string filename, IPrinter printer)
    {
        var monkeys = ParseInput(filename)!;
        printer.Print(monkeys.Values.Where(_ => _.Operator == Operator.None).Count().ToString());
        printer.Flush();
        var count = 0;
        while(count < monkeys.Values.Where(_ => _.Operator == Operator.None).Count())
        {
            foreach (var m in monkeys.Values.Where(_ => _.Name != "root"))
            {
                var (val, dep, val1, dep1) = GetMonkeyValue2(m, monkeys);
                //if (!dep)
                //{
                //    m.Value = val;
                //    m.Operator = Operator.None;
                //}
                //if (!dep1)
                //{

                //}
            }
            count = monkeys.Values.Where(_ => _.Operator == Operator.None).Count();
        }
        
        printer.Print(monkeys.Values.Where(_ => _.Operator == Operator.None).Count().ToString());
        printer.Flush();
        
        return 0;

        //while (!done)
        //{
        //    foreach (var pair in monkeys.Where(_ => _.Value.Operator != Operator.None && _.Value.Name != "root"))
        //    {
        //        var (ls, left) = GetMonkeyValue2(monkeys[pair.Value.Left!], monkeys);
        //        var (rs, right) = GetMonkeyValue2(monkeys[pair.Value.Right!], monkeys);
        //        if (ls)
        //        {
        //            monkeys[pair.Value.Left!].Operator = Operator.None;
        //            monkeys[pair.Value.Left!].Value = left;
        //        }
        //        if (rs)
        //        {
        //            monkeys[pair.Value.Right!].Operator = Operator.None;
        //            monkeys[pair.Value.Right!].Value = right;
        //        }
        //    }
        //    var newC = monkeys.Count(_ => _.Value.Operator != Operator.None);
        //    if (newC == counter)
        //    {
        //        var root = monkeys["root"];
        //        var (ls, l) = GetMonkeyValue2(monkeys[root.Left!], monkeys);
        //        var (rs, r) = GetMonkeyValue2(monkeys[root.Right!], monkeys);
        //        if (ls)
        //        {
        //            monkeys[root.Left!].Value = l;
        //            monkeys[root.Left!].Operator = Operator.None;
        //        }
        //        if (rs)
        //        {
        //            monkeys[root.Right!].Value = l;
        //            monkeys[root.Right!].Operator = Operator.None;
        //        }
        //        if (rs || ls)
        //        {
        //            counter = monkeys.Count(_ => _.Value.Operator != Operator.None);
        //            continue;
        //        }
        //        done = true;
        //    }
        //    else
        //    {
        //        counter = newC;
        //    }
        //}
    }

    private static (long value, bool dependant) GetMonkeyValue2(ScreamMonkey monkey, ref Dictionary<string, ScreamMonkey> monkeys)
    {
        if(monkey.Operator  == Operator.None)
        {
            return (monkey.Value!.Value, false);
        }
        
        var left = monkey.Left;
        var right = monkey.Right;
        //if (left == "humn" || right == "humn")
        //{
        //    return (0, true);
        //}
        var (val, dep) = left == "humn" ? (0, true) : GetMonkeyValue2(monkeys[left!], ref monkeys);
        var (val2, dep2) = right == "humn" ? (0,true) : GetMonkeyValue2(monkeys[right!], ref monkeys);
        if(dep && !dep2)
        {
            monkeys[right].Value = val2;
            monkeys[right].Operator = Operator.None;
        }
        if(!dep && dep2)
        {
            monkeys[left].Value = val2;
            monkeys[left].Operator = Operator.None;
        }
        //if (dep || dep2)
        //{
        //    (val, dep, val2, dep2);
        //}
        return (monkey.Operator switch
        {
            Operator.Add => val + val2,
            Operator.Subtract => val - val2,
            Operator.Divide => val / val2,
            Operator.Multiply => val * val2,
            _ => 0,
        }, false);
        //return monkey.Operator switch
        //{
        //    Operator.Add => GetMonkeyValue(monkeys[monkey.Left!], monkeys) + GetMonkeyValue(monkeys[monkey.Right!], monkeys),
        //    Operator.Multiply => GetMonkeyValue(monkeys[monkey.Left!], monkeys) * GetMonkeyValue(monkeys[monkey.Right!], monkeys),
        //    Operator.Subtract => GetMonkeyValue(monkeys[monkey.Left!], monkeys) - GetMonkeyValue(monkeys[monkey.Right!], monkeys),
        //    Operator.Divide => GetMonkeyValue(monkeys[monkey.Left!], monkeys) / GetMonkeyValue(monkeys[monkey.Right!], monkeys),
        //    _ => monkey.Value!.Value
        //};
    }

    private static long GetMonkeyValue(ScreamMonkey monkey, Dictionary<string, ScreamMonkey> monkeys)
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
    
}

public class ScreamMonkey
{
    public ScreamMonkey(string name, long? value, string? left, string? right, Operator op)
    {
        Name = name;
        Value = value;
        Left = left;
        Right = right;
        Operator = op;
    }

    public string Name { get; }
    public long? Value { get; set; }
    public string? Left { get; }
    public string? Right { get; }
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
