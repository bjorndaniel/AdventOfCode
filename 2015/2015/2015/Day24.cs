namespace AoC2015;

public class Day24
{
    public static IEnumerable<int> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach(var line in lines)
        {
            yield return int.Parse(line);
        }
    }

    [Solveable("2015/Puzzles/Day24.txt", "Day 24 part 1", 24)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var numbers = ParseInput(filename).ToList();

        if (!numbers.Any())
        {
            return new SolutionResult("");
        }

        var total = numbers.Sum();
        var groups = 3;
        if (total % groups != 0)
        {
            return new SolutionResult("");
        }

        var target = total / groups;
        // sort descending to help pruning
        numbers.Sort((a, b) => b.CompareTo(a));

        long? bestQE = null;

        for (int groupSize = 1; groupSize <= numbers.Count; groupSize++)
        {
            var foundAny = false;
            long bestForSize = long.MaxValue;

            void SearchFirstGroup(int start, int remaining, int sum, long product, List<int> chosen)
            {
                if (sum > target)
                {
                    return;
                }
                if (remaining == 0)
                {
                    if (sum == target)
                    {
                        // build remaining list
                        var remainingList = new List<int>();
                        var chosenIndices = new HashSet<int>(chosen);
                        for (int i = 0; i < numbers.Count; i++)
                        {
                            if (!chosenIndices.Contains(i))
                            {
                                remainingList.Add(numbers[i]);
                            }
                        }

                        if (CanFindSubsetSum(remainingList, target))
                        {
                            foundAny = true;
                            if (product < bestForSize)
                            {
                                bestForSize = product;
                            }
                        }
                    }
                    return;
                }

                for (int i = start; i < numbers.Count; i++)
                {
                    // choose index i
                    chosen.Add(i);
                    SearchFirstGroup(i + 1, remaining - 1, sum + numbers[i], product * numbers[i], chosen);
                    chosen.RemoveAt(chosen.Count - 1);
                }
            }

            SearchFirstGroup(0, groupSize, 0, 1, new List<int>());

            if (foundAny)
            {
                bestQE = bestForSize;
                break;
            }
        }

        return new SolutionResult(bestQE.HasValue ? bestQE.Value.ToString() : "");
    }

    [Solveable("2015/Puzzles/Day24.txt", "Day 24 part 2", 24)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var numbers = ParseInput(filename).ToList();

        if (!numbers.Any())
        {
            return new SolutionResult("");
        }

        var total = numbers.Sum();
        var groups = 4;
        if (total % groups != 0)
        {
            return new SolutionResult("");
        }

        var target = total / groups;
        numbers.Sort((a, b) => b.CompareTo(a));

        long? bestQE = null;

        for (int groupSize = 1; groupSize <= numbers.Count; groupSize++)
        {
            var foundAny = false;
            long bestForSize = long.MaxValue;

            void SearchFirstGroup(int start, int remaining, int sum, long product, List<int> chosen)
            {
                if (sum > target)
                {
                    return;
                }
                if (remaining == 0)
                {
                    if (sum == target)
                    {
                        // build remaining list
                        var remainingList = new List<int>();
                        var chosenIndices = new HashSet<int>(chosen);
                        for (int i = 0; i < numbers.Count; i++)
                        {
                            if (!chosenIndices.Contains(i))
                            {
                                remainingList.Add(numbers[i]);
                            }
                        }

                        // Need to find two disjoint subsets in remainingList that each sum to target
                        var subset1 = FindSubsetIndices(remainingList, target);
                        if (subset1 != null)
                        {
                            // remove subset1 indices to form remaining2
                            var set1 = new HashSet<int>(subset1);
                            var remaining2 = new List<int>();
                            for (int i = 0; i < remainingList.Count; i++)
                            {
                                if (!set1.Contains(i))
                                {
                                    remaining2.Add(remainingList[i]);
                                }
                            }

                            if (CanFindSubsetSum(remaining2, target))
                            {
                                foundAny = true;
                                if (product < bestForSize)
                                {
                                    bestForSize = product;
                                }
                            }
                        }
                    }
                    return;
                }

                for (int i = start; i < numbers.Count; i++)
                {
                    // choose index i
                    chosen.Add(i);
                    SearchFirstGroup(i + 1, remaining - 1, sum + numbers[i], product * numbers[i], chosen);
                    chosen.RemoveAt(chosen.Count - 1);
                }
            }

            SearchFirstGroup(0, groupSize, 0, 1, new List<int>());

            if (foundAny)
            {
                bestQE = bestForSize;
                break;
            }
        }

        return new SolutionResult(bestQE.HasValue ? bestQE.Value.ToString() : "");
    }


    private static bool CanFindSubsetSum(List<int> nums, int target)
    {
        var sums = new HashSet<int> { 0 };
        foreach (var n in nums)
        {
            var next = new HashSet<int>(sums);
            foreach (var s in sums)
            {
                var ns = s + n;
                if (ns == target)
                {
                    return true;
                }
                if (ns < target)
                {
                    next.Add(ns);
                }
            }
            sums = next;
        }
        return false;
    }

    private static List<int>? FindSubsetIndices(List<int> nums, int target)
    {
        var result = new List<int>();
        bool Recurse(int start, int sum)
        {
            if (sum == target)
            {
                return true;
            }
            for (int i = start; i < nums.Count; i++)
            {
                var v = nums[i];
                if (sum + v > target)
                {
                    continue;
                }
                result.Add(i);
                if (Recurse(i + 1, sum + v))
                {
                    return true;
                }
                result.RemoveAt(result.Count - 1);
            }
            return false;
        }

        return Recurse(0, 0) ? new List<int>(result) : null;
    }

}