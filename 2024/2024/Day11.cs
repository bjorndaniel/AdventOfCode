namespace AoC2024;
public class Day11
{
    public static List<string> ParseInput(string filename)
    {
        var line = File.ReadAllLines(filename).First();
        var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        return numbers.ToList();
    }

    [Solveable("2024/Puzzles/Day11.txt", "Day 11 part 1", 11)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var numbers = ParseInput(filename);
        var result = SimulateStones(numbers, 25);
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day11.txt", "Day 11 part 2", 11)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var numbers = ParseInput(filename);
        var result = SimulateStones(numbers, 75);
        return new SolutionResult(result.ToString());
    }

    private static long SimulateStones(List<string> stones, int blinks)
    {
        var stoneCounts = new Dictionary<string, long>();
        stones.ForEach(_ =>
        {
            if (stoneCounts.ContainsKey(_))
            {
                stoneCounts[_]++;
            }
            else
            {
                stoneCounts[_] = 1;
            }
        });

        for (int i = 0; i < blinks; i++)
        {
            var result = new Dictionary<string, long>();
            foreach (var (stone, count) in stoneCounts)
            {
                if (stone == "0")
                {
                    AddStone(result, "1", count);
                }
                else if (stone.Length % 2 == 0)
                {
                    var (firstHalf, secondHalf) = SplitStone(stone);
                    AddStone(result, firstHalf, count);
                    AddStone(result, secondHalf, count);
                }
                else
                {
                    var stoneValue = long.Parse(stone);
                    var newStone = (stoneValue * 2024).ToString();
                    AddStone(result , newStone, count);
                }
            }
            stoneCounts = result;
        }
        return stoneCounts.Values.Sum();

        void AddStone(Dictionary<string, long> stoneCounts, string stone, long count)
        {
            if (stoneCounts.ContainsKey(stone))
            {
                stoneCounts[stone] += count;
            }
            else
            {
                stoneCounts[stone] = count;
            }
        }

        (string, string) SplitStone(string stone)
        {
            var middle = stone.Length / 2;
            var firstHalf = stone[..middle];
            var secondHalf = stone[middle..];
            

            return (long.Parse(firstHalf).ToString(), long.Parse(secondHalf).ToString());
        }
    }
}
