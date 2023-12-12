namespace AoC2023;
public class Day12
{
    public static List<ConditionRecord> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<ConditionRecord>();
        foreach (var line in lines)
        {
            var conditions = line.Split(" ")[0];
            var groups = line.Split(" ")[1].Split(",");
            result.Add(new ConditionRecord(conditions, groups.Select(_ => int.Parse(_)).ToList()));
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day12.txt", "Day 12 part 1", 12)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var records = ParseInput(filename);
        var sum = 0;
        var x = HeapPermutation(records[0].Records.ToCharArray(), records[0].Records.Length, records[0].Records.Length);
        foreach (var item in x)
        {
            printer.Print(item.ToString());
            printer.Flush();
        }   

        //foreach (var record in records)
        //{
        //    //var validArrangements = CountArrangements(record.Records, record.Groups.ToArray());
        //    //sum += validArrangements;
        //    //printer.Print($"{record.Records} {validArrangements}");
        //    //printer.Flush();
        //}
        return new SolutionResult(sum.ToString());
    }

    [Solveable("2023/Puzzles/Day12.txt", "Day 12 part 2", 12)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    public record ConditionRecord(string Records, List<int> Groups) { }

    private static char[] HeapPermutation(char[] a, int size, int n)
    {
        // if size becomes 1 then prints the obtained
        // permutation
        if (size == 1)
            return a;

        for (int i = 0; i < size; i++)
        {
            HeapPermutation(a, size - 1, n);

            // if size is odd, swap 0th i.e (first) and
            // (size-1)th i.e (last) element
            if (size % 2 == 1)
            {
                var temp = a[0];
                a[0] = a[size - 1];
                a[size - 1] = temp;
            }

            // If size is even, swap ith and
            // (size-1)th i.e (last) element
            else
            {
                var temp = a[i];
                a[i] = a[size - 1];
                a[size - 1] = temp;
            }
        }
        return a;
    }

    public static int CountArrangements(string springs, int[] groups, int index = 0, int start = 0)
    {
        if (index == groups.Length)
            return start == springs.Length ? 1 : 0;

        int total = 0;
        for (int i = start; i <= springs.Length - groups[index]; i++)
        {
            if (IsValid(springs, start, i, groups[index]))
            {
                string newSprings = springs.Substring(0, start) + new string('.', i - start) + new string('#', groups[index]) + springs.Substring(i + groups[index]);
                total += CountArrangements(newSprings, groups, index + 1, i + groups[index] + 1);
            }
        }

        return total;
    }

    private static bool IsValid(string springs, int start, int end, int group)
    {
        if (start > 0 && springs[start - 1] == '#')
            return false;

        for (int i = start; i < end; i++)
        {
            if (springs[i] != '.' && springs[i] != '?')
                return false;
        }

        for (int i = end; i < end + group; i++)
        {
            if (i >= springs.Length || (springs[i] != '#' && springs[i] != '?'))
                return false;
        }

        return end + group == springs.Length || springs[end + group] != '#';
    }

    //private static int CountArrangements(string springs, int[] groups, int groupIndex = 0, int springIndex = 0)
    //{
    //    if (groupIndex >= groups.Length)
    //    {
    //        for (int i = springIndex; i < springs.Length; i++)
    //        {
    //            if (springs[i] == '#') return 0;
    //        }
    //        return 1;
    //    }

    //    int count = 0;
    //    for (int i = springIndex; i <= springs.Length - groups[groupIndex]; i++)
    //    {
    //        if (CanPlaceGroup(springs, i, groups[groupIndex]))
    //        {
    //            string newSprings = PlaceGroup(springs, i, groups[groupIndex]);
    //            count += CountArrangements(newSprings, groups, groupIndex + 1, i + groups[groupIndex] + 1);
    //        }
    //    }
    //    return count;
    //}

    //private static bool CanPlaceGroup(string springs, int index, int groupSize)
    //{
    //    // Rest of the method implementation
    //    for (int i = index; i < index + groupSize; i++)
    //    {
    //        if (i >= springs.Length || (springs[i] != '?' && springs[i] != '#'))
    //        {
    //            return false;
    //        }
    //    }
    //    var continousGroup = groupSize > 1 && springs.Substring(index,groupSize-1).All(_ => _ == '#');
    //    return index == 0 || (index + groupSize <= springs.Length && (springs[index + groupSize - 1] != '#' || continousGroup));

    //}


    //private static string PlaceGroup(string springs, int index, int groupSize)
    //{
    //    return springs.Substring(0, index) + new string('#', groupSize) + springs.Substring(index + groupSize);
    //}

}