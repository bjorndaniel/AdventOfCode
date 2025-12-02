namespace AoC2015;

public class Day20
{
    [Solveable("2015/Puzzles/Day20.txt", "Day 20 part 1", 20)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var target = 29000000;
        var house = FindHouse(target, part2: false);
        return new SolutionResult(house.ToString());
    }

    [Solveable("2015/Puzzles/Day20.txt", "Day 20 part 2", 20)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var target = 29000000;
        var house = FindHouse(target, part2: true);
        return new SolutionResult(house.ToString());
    }

    // Finds the lowest house number that receives at least 'target' presents.
    // part2 = false: each elf delivers10 * elf to every multiple
    // part2 = true: each elf delivers11 * elf but only to50 houses maximum
    private static int FindHouse(int target, bool part2)
    {
        var factor = part2 ? 11 : 10;
        var limit = Math.Max(1_000, target / factor);

        while (true)
        {
            var presents = new int[limit + 1];

            for (var elf = 1; elf <= limit; elf++)
            {
                var amount = elf * factor;
                if (!part2)
                {
                    for (var house = elf; house <= limit; house += elf)
                    {
                        presents[house] += amount;
                    }
                }
                else
                {
                    var deliveries = 0;
                    for (var house = elf; house <= limit && deliveries < 50; house += elf, deliveries++)
                    {
                        presents[house] += amount;
                    }
                }
            }

            for (var h = 1; h <= limit; h++)
            {
                if (presents[h] >= target)
                {
                    return h;
                }
            }

            // increase limit and retry
            limit *= 2;
        }
    }
}