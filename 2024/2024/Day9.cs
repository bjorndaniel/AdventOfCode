namespace AoC2024;
public class Day9
{
    public static List<Block> ParseInput(string filename, bool part2 = false)
    {
        var line = File.ReadAllLines(filename).First();
        var result = new List<Block>();//new Dictionary<int, string>();
        var id = 0;

        for (int i = 0; i < line.Length; i += 2)
        {
            var length = int.Parse(line[i].ToString());
            var number = new List<int>();
            if (i == line.Length - 1)
            {
                for (int j = 0; j < length; j++)
                {
                    result.Add(new Block(id));
                }
                //result.Add(new(id, number));
            }
            else
            {
                var freeSpace = int.Parse(line[i + 1].ToString());
                number = new();
                for (int j = 0; j < length; j++)
                {
                    result.Add(new Block(id));
                }
                for (int j = 0; j < freeSpace; j++)
                {
                    //number.Add(-1);
                    result.Add(new Block(-1));
                }
                //result.Add(new(id, number));
            }
            id++;
        }
        return result;
    }

    [Solveable("2024/Puzzles/Day9.txt", "Day 9 part 1", 9)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var input = ParseInput(filename);
        var result = SwapBlocks(input, printer);//.Select(_ => _.Number == -1 ? "." : _.Number.ToString()).Aggregate((a, b) => $"{a}{b}");
        long checksum = 0;
        for (int i = 0; i < result.Count; i++)
        {
            if (result[i].Number == -1)
            {
                break;
            }
            checksum += result[i].Number * i;
        }
        return new SolutionResult(checksum.ToString());
    }

    [Solveable("2024/Puzzles/Day9.txt", "Day 9 part 2", 9, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var input = ParseInput(filename, true);
        var groupedBlocks = GroupBlocks(input);
        var result = SwapGroups(groupedBlocks, printer);
        //var ts = result.Select(_ => _.Number == -1 ? "." : _.Number.ToString()).Aggregate((a, b) => $"{a}{b}");
        //printer.Print(ts);
        //printer.Flush();
        long counter = 0;
        for (int i = 0; i < result.Count; i++)
        {
            if (result[i].Number == -1)
            {
                continue;
            }
            counter += result[i].Number * i;
        }
        return new SolutionResult(counter.ToString());
    }

    private static List<List<Block>> GroupBlocks(List<Block> input)
    {
        var groupedBlocks = new List<List<Block>>();
        if (input.Count == 0)
        {
            return groupedBlocks;
        }

        var currentGroup = new List<Block> { input[0] };
        for (int i = 1; i < input.Count; i++)
        {
            if (input[i].Number == input[i - 1].Number)
            {
                currentGroup.Add(input[i]);
            }
            else
            {
                groupedBlocks.Add(currentGroup);
                currentGroup = new List<Block> { input[i] };
            }
        }

        groupedBlocks.Add(currentGroup);
        return groupedBlocks;
    }

    private static List<Block> SwapGroups(List<List<Block>> groupedBlocks, IPrinter printer)
    {
        var frontIndex = 0;
        var backIndex = groupedBlocks.Count - 1;

        while (frontIndex < backIndex)
        {
            while (frontIndex < backIndex && groupedBlocks[frontIndex].Count(_ => _.Number == -1) < groupedBlocks[backIndex].Count(_ => _.Number != -1))
            {
                frontIndex++;
                if (frontIndex == backIndex)
                {
                    frontIndex = 0;
                    backIndex--;
                }
            }

            while (frontIndex < backIndex && groupedBlocks[backIndex].All(_ => _.Number == -1))
            {
                backIndex--;
            }
            if (groupedBlocks[backIndex].All(_ => _.Number == -1))
            {
                backIndex--;
            }

            if (frontIndex < backIndex && groupedBlocks[backIndex].Count(_ => _.Number != -1) <= groupedBlocks[frontIndex].Count(_ => _.Number == -1))
            {
                var backCounter = 0;
                for(int i = 0; i < groupedBlocks[frontIndex].Count; i++)
                {
                    if(groupedBlocks[frontIndex][i].Number == -1 && groupedBlocks[backIndex].Count > backCounter)
                    {
                        groupedBlocks[frontIndex][i] = groupedBlocks[backIndex][backCounter];
                        groupedBlocks[backIndex][backCounter] = new(-1);
                        backCounter++;
                    }
                }
                //groupkedBlocks[backIndex] = Enumerable.Range(0, groupedBlocks[backIndex].Count).Select(_ => groupedBlocks[frontIndex].Count > _ ? groupedBlocks[frontIndex][_] : new Block(-1)).ToList();
                //var t = groupedBlocks.SelectMany(group => group).ToList();
                //var ts = t.Select(_ => _.Number == -1 ? "." : _.Number.ToString()).Aggregate((a, b) => $"{a}{b}");
                //printer.Print(ts);
                //printer.Flush();
                backIndex--;
                frontIndex = 0;
            }
        }

        return groupedBlocks.SelectMany(group => group).ToList();
    }

    private static List<Block> SwapBlocks(List<Block> input, IPrinter printer)
    {
        int frontIndex = 0;
        int backIndex = input.Count - 1;

        while (frontIndex < backIndex)
        {
            while (frontIndex < backIndex && input[frontIndex].Number != -1)
            {
                frontIndex++;
            }

            while (frontIndex < backIndex && input[backIndex].Number == -1)
            {
                backIndex--;
            }

            if (frontIndex < backIndex)
            {
                var temp = input[frontIndex];
                input[frontIndex] = input[backIndex];
                input[backIndex] = temp;
            }
        }

        return input;
    }
}

public record Block(int Number);