namespace AoC2022;
public static class Day7
{
    public static Directory ParseInput(string filename)
    {
        var result = new List<Directory>();
        var lines = File.ReadAllLines(filename);
        Directory currentDirectory = null!;
        for (int i = 0; i < lines.Length; i++)
        {
            var instr = lines[i].Split(" ");
            if (instr[1] == "cd")
            {
                if (instr[2] == "..")
                {
                    currentDirectory = currentDirectory?.Parent!;
                }
                else
                {
                    currentDirectory = currentDirectory?.SubDirectories.FirstOrDefault(_ => _.Name == instr[2])! ?? null!;
                    if (currentDirectory == null)
                    {
                        currentDirectory = new Directory { Name = instr[2], Parent = currentDirectory };
                        result.Add(currentDirectory);
                    }
                }
            }
            else if (instr[1] == "ls")
            {
                continue;
            }
            else
            {
                if (instr[0] == "dir")
                {
                    var exists = currentDirectory.SubDirectories.FirstOrDefault(_ => _.Name == instr[1])!;
                    if (exists == null)
                    {
                        var directory = new Directory { Name = instr[1], Parent = currentDirectory };
                        result.Add(directory);
                        currentDirectory.SubDirectories.Add(directory);
                    }
                }
                else
                {
                    var existingFile = currentDirectory.Files.FirstOrDefault(_ => _.Name == instr[1]);
                    if (existingFile == null)
                    {
                        currentDirectory.Files.Add(new AoCFile(instr[1], long.Parse(instr[0])));
                    }
                }
            }
        }
        return result.First(_ => _.Name == "/");
    }

    public static long SolvePart1(string filename)
    {
        var root = ParseInput(filename);
        return CalculateSum(root);

        static long CalculateSum(Directory d)
        {
            var sum = d.TotalSize <= 100000 ? d.TotalSize : 0;
            foreach (var directory in d.SubDirectories)
            {
                sum += CalculateSum(directory);
            }
            return sum;
        }
    }

    public static long SolvePart2(string day7File)
    {
        var root = ParseInput(day7File);
        var sizeLeft = 70000000 - root.TotalSize;
        var sizeNeeded = 30000000 - sizeLeft;
        var currentSize = long.MaxValue;
        return FindSmallestToDelete(root, currentSize, sizeNeeded);

        static long FindSmallestToDelete(Directory d, long currentSize, long sizeNeeded)
        {
            if (d.TotalSize >= sizeNeeded && d.TotalSize < currentSize)
            {
                currentSize = d.TotalSize;
            }
            foreach (var directory in d.SubDirectories)
            {
                var size = FindSmallestToDelete(directory, currentSize, sizeNeeded);
                if (size < currentSize && size >= sizeNeeded)
                {
                    currentSize = size;
                }
            }
            return currentSize;
        }
    }
}

public class Directory
{
    public string Name { get; set; } = string.Empty;
    public Directory? Parent { get; set; }
    public List<Directory> SubDirectories { get; set; } = new();
    public List<AoCFile> Files { get; set; } = new();
    public long TotalSize =>
        Files.Sum(_ => _.Size) + SubDirectories.Sum(_ => _.TotalSize);
}

public record AoCFile(string Name, long Size) { }

