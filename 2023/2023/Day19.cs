namespace AoC2023;
public class Day19
{
    public static (List<Workflow> workflows, List<Rating> ratings) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var flows = new List<Workflow>();
        var ratings = new List<Rating>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            if (line.StartsWith('{'))
            {
                var vals = line[1..^1].Split(',');
                var x = long.Parse(vals[0].Split('=')[1]);
                var m = long.Parse(vals[1].Split('=')[1]);
                var a = long.Parse(vals[2].Split('=')[1]);
                var s = long.Parse(vals[3].Split('=')[1]);
                ratings.Add(new Rating(Guid.NewGuid(), new Dictionary<char, long> { { 'x', x }, { 'm', m }, { 'a', a }, { 's', s } }));
            }
            else
            {
                var parts = line.Split('{')[1][..^1];
                var name = line.Split('{')[0];
                var flowsParts = parts.Split(',');
                var f = new List<Flow>();
                for (int i = 0; i < flowsParts.Length; i++)
                {
                    if (flowsParts[i].Contains(':'))
                    {
                        var condition = flowsParts[i].Contains('>') ? Condition.GreaterThan : Condition.LessThan;
                        var part = flowsParts[i][0];
                        var output = flowsParts[i].Split(':')[1];
                        var number = int.Parse(flowsParts[i].Split(':')[0][2..]);
                        f.Add(new Flow(i, part, number, condition, output));
                    }
                    else
                    {
                        f.Add(new Flow(i, '_', -1, Condition.Output, flowsParts[i]));
                    }
                }
                flows.Add(new Workflow(name, f));
            }
        }
        return (flows, ratings);
    }

    [Solveable("2023/Puzzles/Day19.txt", "Day 19 part 1", 19)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (workflows, ratings) = ParseInput(filename);
        var start = workflows.First(w => w.Name == "in");
        var accepted = new List<Rating>();
        foreach (var rating in ratings)
        {
            var done = false;
            while (!done)
            {
                foreach (var f in start.Flows.OrderBy(_ => _.Order))
                {
                    if (f.Condition == Condition.Output)
                    {
                        if (f.Output == "A")
                        {
                            accepted.Add(rating);
                            done = true;
                            start = workflows.First(w => w.Name == "in");
                            break;
                        }
                        else if (f.Output == "R")
                        {
                            done = true;
                            start = workflows.First(w => w.Name == "in");
                            break;
                        }
                        else
                        {
                            start = workflows.First(w => w.Name == f.Output);
                            break;
                        }
                    }
                    else
                    {
                        var part = f.Part;
                        var condition = f.Condition;
                        var number = rating.PartRatings[part];
                        if (condition == Condition.GreaterThan)
                        {
                            if (number > f.Number)
                            {
                                if (f.Output == "A")
                                {
                                    accepted.Add(rating);
                                    done = true;
                                    start = workflows.First(w => w.Name == "in");
                                    break;
                                }
                                else if (f.Output == "R")
                                {
                                    done = true;
                                    start = workflows.First(w => w.Name == "in");
                                    break;
                                }
                                else
                                {
                                    start = workflows.First(w => w.Name == f.Output);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (number < f.Number)
                            {
                                if (f.Output == "A")
                                {
                                    accepted.Add(rating);
                                    done = true;
                                    start = workflows.First(w => w.Name == "in");
                                    break;
                                }
                                else if (f.Output == "R")
                                {
                                    done = true;
                                    start = workflows.First(w => w.Name == "in");
                                    break;
                                }
                                else
                                {
                                    start = workflows.First(w => w.Name == f.Output);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

        }
        return new SolutionResult(accepted.Sum(_ => _.Value()).ToString());
    }

    //Following walktrough from https://www.youtube.com/watch?v=3RwIpUegdU4
    [Solveable("2023/Puzzles/Day19.txt", "Day 19 part 2", 19)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var lines = File.ReadAllLines(filename);
        var ranges = new Dictionary<char, (int low, int high)>
        {
            {'x', (1, 4000)},
            {'m', (1, 4000)},
            {'a', (1, 4000)},
            {'s', (1, 4000)}
         };
        var workflows = new Dictionary<string, (List<(char key, char comp, int number, string target)> rules, string fallback)>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                break;
            }
            var prts = line[..^1].ToString().Split("{");
            var name = prts[0];
            var rules = prts[1].Split(",")[..^1];
            var fallback = prts[1].Split(",").Last();
            workflows.Add(name, ([], fallback));
            foreach (var rule in rules)
            {
                var (comparison, target) = (rule.Split(":")[0], rule.Split(":")[1]);
                var key = comparison[0];
                var cmp = comparison[1];
                var number = int.Parse(comparison[2..]);
                workflows[name].rules.Add((key, cmp, number, target));
            }

        }
        var result = Count(ranges, workflows);
        return new SolutionResult(result.ToString());
        static long Count(Dictionary<char, (int low, int high)> ranges, Dictionary<string, (List<(char key, char comp, int number, string target)> rules, string fallback)> workflows, string name = "in")
        {
            if (name == "R")
            {
                return 0;
            }
            if (name == "A")
            {
                var product = 1L;
                foreach (var r in ranges)
                {
                    product *= (r.Value.high - r.Value.low + 1);
                }
                return product;
            }
            var total = 0L;
            var cont = true;
            var (rules, fallback) = workflows[name];
            foreach (var (key, cmp, n, target) in rules)
            {
                var (low, high) = ranges[key];
                var (tl, th) = (0, 0);
                var (fl, fh) = (0, 0);
                if (cmp == '<')
                {
                    (tl, th) = (low, Math.Min(n - 1, high));
                    (fl, fh) = (Math.Max(n, low), high);
                }
                else
                {
                    (tl, th) = (Math.Max(n + 1, low), high);
                    (fl, fh) = (low, Math.Min(n, high));
                }
                if (tl <= th)
                {
                    var copy = new Dictionary<char, (int low, int high)>(ranges)
                    {
                        [key] = (tl, th)
                    };
                    var next = target;
                    total += Count(copy, workflows, next);
                }
                if (fl <= fh)
                {
                    var copy = new Dictionary<char, (int low, int high)>(ranges);
                    ranges = copy;
                    ranges[key] = (fl, fh);
                }
                else
                {
                    cont = false;
                    break;
                }
            }
            if (cont)
            {
                total += Count(ranges, workflows, fallback);
            }
            return total;
        }

    }

    public record Rating(Guid id, Dictionary<char, long> PartRatings)
    {
        public long Value() =>
            PartRatings['x'] + PartRatings['m'] + PartRatings['a'] + PartRatings['s'];
    }

    public record Workflow(string Name, List<Flow> Flows) { }

    public record Flow(int Order, char Part, int Number, Condition Condition, string Output) { }

    public enum Condition
    {
        Output,
        LessThan,
        GreaterThan,
    }
}