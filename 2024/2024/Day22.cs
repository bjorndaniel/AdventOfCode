namespace AoC2024;
public class Day22
{
    public static List<long> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<long>();
        foreach (var l in lines)
        {
            result.Add(long.Parse(l));
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day22.txt", "Day 22 part 1", 22)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var data = ParseInput(filename);
        var result = 0L;

        for (int j = 0; j < data.Count; j++)
        {
            for (int i = 0; i < 2000; i++)
            {
                data[j] = GenerateNext(data[j]);
            }
        }
        result = data.Sum();
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day22.txt", "Day 22 part 2", 22)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var data = ParseInput(filename);
        var counter = filename.Contains("test3") ? 10 : 2000;
        var sequences = new List<Dictionary<(long s1, long s2, long s3, long s4), (long banana, long change)>>();
        for (int dCount = 0; dCount < data.Count; dCount++)
        {
            var dict = new Dictionary<(long s1, long s2, long s3, long s4), (long banana, long change)>();
            var prev = data[dCount] % 10;
            var changes = new List<(long banana, long change)>();
            for (int round = 0; round < counter; round++)
            {
                data[dCount] = GenerateNext(data[dCount]);
                var newNum = data[dCount] % 10;
                var change = newNum - prev;
                changes.Add((newNum, change));
                prev = newNum;
                if (changes.Count == 4)
                {
                    if (dict.ContainsKey((changes[0].change, changes[1].change, changes[2].change, changes[3].change)))
                    {
                        //var val = dict[(changes[0].change, changes[1].change, changes[2].change, changes[3].change)];
                        //if (val.banana < changes[3].banana)
                        //{
                        //    dict[(changes[0].change, changes[1].change, changes[2].change, changes[3].change)] = (changes[3].banana, changes[3].change);
                        //}
                    }
                    else if(changes[3].banana > 0)
                    {
                        dict.Add((changes[0].change, changes[1].change, changes[2].change, changes[3].change), (changes[3].banana, changes[3].change));
                    }
                    changes.RemoveAt(0);
                }
            }
            sequences.Add(dict);
        }

        

        var maxBananas = long.MinValue;
        for (int i = 0; i < sequences.Count; i++)
        {
            var current = sequences[i];
            foreach(var s in current)
            {
                var currentSum = s.Value.banana;
                
                for (int k = 0; k < sequences.Count; k++)
                {
                    if (k == i)
                    {
                        continue;
                    }
                    var other = sequences[k];
                    if(other.ContainsKey(s.Key))
                    {
                        //foreach (var s2 in other)
                        //{
                        //    if (s2.Key == s.Key)
                        //    {
                                currentSum += other[s.Key].banana;
                                break;
                        //    }
                        //}
                    }
                }
                if (currentSum > maxBananas)
                {
                    maxBananas = currentSum;
                }
                
            }
            
        }

        return new SolutionResult(maxBananas.ToString());
    }

    public static long GenerateNext(long number)
    {
        var next = number * 64;
        next = number ^ next; //Helpers.XorWithPadding(number, next);
        next %= 16777216;
        var temp = next / 32;
        next = next ^ temp; //Helpers.XorWithPadding(next, temp);
        next %= 16777216;
        temp = next * 2048;
        next = next ^ temp; //Helpers.XorWithPadding(next, temp);
        next %= 16777216;
        return next;
    }
}