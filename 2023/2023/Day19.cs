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
                        else if(f.Output == "R")
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
                        if(condition == Condition.GreaterThan)
                        {
                            if(number > f.Number)
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
                            if(number < f.Number)
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

    [Solveable("2023/Puzzles/Day19.txt", "Day 19 part 2", 19)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (workflows, ratings) = ParseInput(filename);
        var start = workflows.First(w => w.Name == "in");
        return new SolutionResult("");
    }

    public record Rating(Guid id, Dictionary<char, long> PartRatings)
    {
        public long Value() =>
            PartRatings['x'] + PartRatings['m'] + PartRatings['a'] + PartRatings['s'];
    }

    public record Workflow(string Name, List<Flow> Flows) { }

    public record Flow(int Order, char Part, long Number, Condition Condition, string Output) { }

    public enum Condition
    {
        Output,
        LessThan,
        GreaterThan,
    }
}