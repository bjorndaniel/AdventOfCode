namespace Advent2021;
public class Day14
{
    public static long CalculateElements(string filename, int steps = 10)
    {
        var (template, rules) = ReadTemplateAndRules(filename);
        return CountPairs(template, rules, steps);
    }

    private static long CountPairs(string template, Dictionary<string, (string, string)> rules, int steps)
    {
        var initialCount = template.GroupBy(_ => _);
        //var pairs = Chunk(template).ToDictionary(_ => _, _ => (long)1);
        var pairs = new Dictionary<string, long>();
        var chunks = Chunk(template);
        foreach (var c in chunks)
        {
            AddOrIncrement(pairs, c, 1);
        }
        for (int i = 0; i < steps; i++)
        {
            pairs = Count(pairs, rules);
        }
        var values = pairs
            .Select(_ => new { Key = _.Key.First(), Value = _.Value })
            .GroupBy(_ => _.Key)
            .Select(_ => new { Key = _.Key, Value = (_.Key == template.Last() ? _.Sum(k => k.Value) + 1 : _.Sum(k => k.Value)) });
        //Stupid! If highest or lowest is last letter add one since we only count first char in pair
        var x = values.GroupBy(_ => _.Key).Max(_ => _.Sum(_ => _.Value));
        var y = values.GroupBy(_ => _.Key).Min(_ => _.Sum(_ => _.Value));
        return x - y;
    }

    private static Dictionary<string, long> Count(Dictionary<string, long> pairs, Dictionary<string, (string, string)> rules)
    {
        var result = new Dictionary<string, long>();

        foreach (var pair in pairs)
        {
            if (rules.ContainsKey(pair.Key))
            {
                AddOrIncrement(result, rules[pair.Key].Item1, pair.Value);
                AddOrIncrement(result, rules[pair.Key].Item2, pair.Value);
            }
        }
        return result;
    }

    private static void AddOrIncrement(Dictionary<string, long> target, string value, long increment)
    {
        if (target.ContainsKey(value))
        {
            target[value] += increment;
        }
        else
        {
            target.Add(value, increment);
        }
    }

    private static (string template, Dictionary<string, (string, string)> rules) ReadTemplateAndRules(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var template = lines.First();
        var rules = new Dictionary<string, (string, string)>();
        foreach (var line in lines.Skip(2))
        {
            var rule = line.Split("->").First().Trim();
            var insertion = line.Split("->").Last().Trim().First();
            rules.Add(rule, ($"{rule.First()}{insertion}", $"{insertion}{rule.Last()}"));
        }
        return (template, rules);
    }

    private static IEnumerable<string> Chunk(string s)
    {
        var newString = s;
        while (newString.Length > 1)
        {
            var chunk = newString.Take(2);
            newString = string.Concat(newString.Skip(1));
            yield return string.Concat(chunk);
        }
    }

}

