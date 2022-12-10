namespace Advent2021;
public class Day8
{
    public static int CountDigits(string filename)
    {
        var outputs = GetLists(filename, false);
        return GetSum(outputs);
    }

    private static int GetSum(IEnumerable<string> inputs)
    {
        var result = 0;
        result += inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 2).Select(_ => _.Count()).Sum();
        result += inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 4).Select(_ => _.Count()).Sum();
        result += inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 3).Select(_ => _.Count()).Sum();
        result += inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 7).Select(_ => _.Count()).Sum();
        return result;
    }
    public static int GetTotal(string filename)
    {
        var dictionary = GetAllDigitPatterns(filename);
        var lines = File.ReadAllLines(filename);
        var result = 0;
        var counter = 0;
        foreach (var line in lines)
        {
            var outputs = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(_ => SortString(_));
            var builder = new StringBuilder();
            foreach (var output in outputs)
            {
                builder.Append(dictionary[(counter, output)]);
            }

            var count = int.Parse(builder.ToString());
            result += count;
            counter++;
        }
        return result;
    }

    public static Dictionary<(int line, string code), int> GetAllDigitPatterns(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var counter = 0;
        var result = new Dictionary<(int, string), int>();
        foreach (var line in lines)
        {

            var input = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[0];
            var output = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1];
            var inputs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var outputs = output.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var one = SortString(outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 2).FirstOrDefault()?.FirstOrDefault() ??
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 2).FirstOrDefault()?.FirstOrDefault() ?? "");
            var four = SortString(outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 4).FirstOrDefault()?.FirstOrDefault() ??
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 4).FirstOrDefault()?.FirstOrDefault() ?? "");
            var seven = SortString(outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 3).FirstOrDefault()?.FirstOrDefault() ??
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 3).FirstOrDefault()?.FirstOrDefault() ?? "");
            var eight = SortString(outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 7).FirstOrDefault()?.FirstOrDefault() ??
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 7).FirstOrDefault()?.FirstOrDefault() ?? "");
            result.Add((counter, one), 1);
            result.Add((counter, four), 4);
            result.Add((counter, seven), 7);
            result.Add((counter, eight), 8);
            var top = SortString(RemoveAll(seven, one));
            var nineSixOrZero = inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 6).Any() ?
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 6) :
                outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 6);
            var twoThreeOrFive = inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 5).Any() ?
                inputs.GroupBy(_ => _.Length).Where(_ => _.Key == 5) :
                outputs.GroupBy(_ => _.Length).Where(_ => _.Key == 5);
            var nine = SortString(nineSixOrZero.First().First(_ => ContainsAll(SortString(_), four)));
            result.Add((counter, nine), 9);
            var sixOrZero = nineSixOrZero.First().Where(_ => SortString(_) != nine);
            var zero = SortString(sixOrZero.First(_ => ContainsAll(SortString(_), one)));
            var six = SortString(sixOrZero.First(_ => SortString(_) != zero));
            result.Add((counter, zero), 0);
            result.Add((counter, six), 6);
            var three = SortString(twoThreeOrFive.First().First(_ => ContainsAll(SortString(_), one)));
            result.Add((counter, three), 3);
            var twoOrFive = twoThreeOrFive.First().Where(_ => SortString(_) != three);
            var leftMiddle = RemoveAll(four, one);
            var five = SortString(twoOrFive.First(_ => ContainsAll(SortString(_), leftMiddle)));
            result.Add((counter, five), 5);
            var two = SortString(twoOrFive.First(_ => SortString(_) != five));
            result.Add((counter, two), 2);
            counter++;

        }
        return result;
    }

    private static IEnumerable<string> GetLists(string filename, bool inputs = true)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<string>();
        foreach (var line in lines)
        {
            var input = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[0];
            var output = line.Split("|", StringSplitOptions.RemoveEmptyEntries)[1];
            if (inputs)
            {
                result.AddRange(input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            else
            {
                result.AddRange(output.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList());
            }

        }
        return result;
    }

    //From https://stackoverflow.com/questions/6441583/is-there-a-simple-way-that-i-can-sort-characters-in-a-string-in-alphabetical-ord/6441603
    private static string SortString(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        char[] characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }

    private static string RemoveAll(string target, string charsToReplace)
    {
        foreach (var c in charsToReplace)
        {
            target = target.Replace(c.ToString(), "");
        }
        return target;
    }

    static bool ContainsAll(string target, string compares)
    {
        var contains = true;
        foreach (var c in compares)
        {
            contains = contains && target.Contains(c);
        }
        return contains;
    }
}

