namespace AoC2022;
public static class Day11
{
    public static Dictionary<int, Monkey> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new Dictionary<int, Monkey>();
        var monkeyChunks = GetMonkeyStrings(lines);
        foreach (var m in monkeyChunks)
        {
            var (op, value) = GetOperation(m.Skip(2).First());
            var monkey = new Monkey
            (
                id: int.Parse(m.First().Split(' ')[1].Trim(':')),
                items: GetItems(m.Skip(1).First()),
                operation: op,
                worryOpValue: value,
                testDivider: int.Parse(m.Skip(3).First().Split(' ').Last().Trim()),
                passMonkey: int.Parse(m.Skip(4).First().Split(' ').Last().Trim()),
                failMonkey: int.Parse(m.Skip(5).First().Split(' ').Last().Trim())
            );
            result.Add(monkey.Id, monkey);
        }
        return result;

        static (WorryOp op, int value) GetOperation(string op)
        {
            var opTest = op.Split('=').Last();
            if (opTest.Contains("*"))
            {
                var opValue = opTest.Split('*').Last().Trim();
                if (opValue == "old")
                {
                    return (WorryOp.OldSquare, 0);
                }
                else
                {
                    return (WorryOp.OldMultiply, int.Parse(opValue));
                }
            }
            else
            {
                var opValue = opTest.Split('+').Last().Trim();
                return (WorryOp.OldAdd, int.Parse(opValue));
            }
        }

        static List<long> GetItems(string items) =>
             items.Split(':').Last().Split(',').Select(i => long.Parse(i.Trim())).ToList();

        static List<List<string>> GetMonkeyStrings(string[] lines)
        {
            var monkeyChunks = new List<List<string>>();
            var currentChunk = new List<string>();
            foreach (var l in lines)
            {
                if (string.IsNullOrEmpty(l))
                {
                    monkeyChunks.Add(currentChunk);
                    currentChunk = new List<string>();
                }
                else
                {
                    currentChunk.Add(l);
                }
            }
            monkeyChunks.Add(currentChunk);
            return monkeyChunks;
        }
    }

    public static (long result, Dictionary<int, Monkey> monkeys) SolvePart1(string filename, int rounds) =>
        RunRounds(filename, rounds);

    public static (long result, Dictionary<int, Monkey> monkeys) SolvePart2(string filename, int rounds) =>
        RunRounds(filename, rounds, true);

    private static (long result, Dictionary<int, Monkey> monkeys) RunRounds(string filename, int rounds, bool isPartTwo = false)
    {
        var monkeys = ParseInput(filename);
        var modulo = monkeys.Values.Select(_ => _.TestDivider).Aggregate((a, b) => a * b);
        for (int i = 0; i < rounds; i++)
        {
            for (int id = 0; id < monkeys.Max(_ => _.Key) + 1; id++)
            {
                var monkey = monkeys[id];
                foreach (var item in monkey.Items)
                {
                    var newWorry = item;
                    monkey.NrOfInspections++;
                    switch (monkey.Operation)
                    {
                        case WorryOp.OldSquare:
                            newWorry = item * item;
                            break;
                        case WorryOp.OldMultiply:
                            newWorry = item * monkey.WorryOpValue;
                            break;
                        case WorryOp.OldAdd:
                            newWorry = item + monkey.WorryOpValue;
                            break;
                        default:
                            throw new ArgumentException("Unknown operation");
                    }
                    if (!isPartTwo)
                    {
                        newWorry /= 3;
                    }
                    else
                    {
                        newWorry %= modulo;
                    }
                    if (newWorry % monkey.TestDivider == 0)
                    {
                        monkeys[monkey.PassMonkey].Items.Add(newWorry);
                    }
                    else
                    {
                        monkeys[monkey.FailMonkey].Items.Add(newWorry);
                    }
                }
                monkey.Items.Clear();
            }
        }
        var max = monkeys.OrderByDescending(_ => _.Value.NrOfInspections).Take(2);
        return (max.First().Value.NrOfInspections * max.Last().Value.NrOfInspections, monkeys);

    }
}

public class Monkey
{
    public Monkey(int id, List<long> items, WorryOp operation, int worryOpValue, int passMonkey, int failMonkey, int testDivider)
    {
        Id = id;
        Items = items;
        Operation = operation;
        WorryOpValue = worryOpValue;
        PassMonkey = passMonkey;
        FailMonkey = failMonkey;
        TestDivider = testDivider;
    }

    public int Id { get; private set; }
    public List<long> Items { get; private set; } = new();
    public WorryOp Operation { get; private set; }
    public int WorryOpValue { get; private set; }
    public int TestDivider { get; private set; }
    public int PassMonkey { get; private set; }
    public int FailMonkey { get; private set; }
    public long NrOfInspections { get; set; }
}

public enum WorryOp
{
    OldSquare,
    OldAdd,
    OldMultiply
}

