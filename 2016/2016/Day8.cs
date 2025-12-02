namespace AoC2016;

public class Day8
{
    public static List<Instruction> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Instruction>();
        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            if (parts[0] == "rect")
            {
                var dims = parts[1].Split('x');
                result.Add(new Instruction(Command.Rect, int.Parse(dims[0]), int.Parse(dims[1])));
            }
            else if (parts[0] == "rotate" && parts[1] == "row")
            {
                var row = int.Parse(parts[2].Split('=')[1]);
                var by = int.Parse(parts[4]);
                result.Add(new Instruction(Command.RotateRow, row, by));
            }
            else if (parts[0] == "rotate" && parts[1] == "column")
            {
                var col = int.Parse(parts[2].Split('=')[1]);
                var by = int.Parse(parts[4]);
                result.Add(new Instruction(Command.RotateColumn, col, by));
            }
        }
        return result;
    }

    private static bool[,] ProcessInstructions(List<Instruction> instructions, int width, int height)
    {
        var screen = new bool[width, height];
        
        foreach (var inst in instructions)
        {
            switch (inst.Command)
            {
                case Command.Rect:
                    // Turn on all pixels in rectangle from (0,0) to (First-1, Second-1)
                    for (int x = 0; x < inst.First; x++)
                    {
                        for (int y = 0; y < inst.Second; y++)
                        {
                            screen[x, y] = true;
                        }
                    }
                    break;
                    
                case Command.RotateRow:
                    // Rotate row inst.First by inst.Second positions to the right
                    var rowBackup = new bool[width];
                    for (int x = 0; x < width; x++)
                    {
                        rowBackup[(x + inst.Second) % width] = screen[x, inst.First];
                    }
                    for (int x = 0; x < width; x++)
                    {
                        screen[x, inst.First] = rowBackup[x];
                    }
                    break;
                    
                case Command.RotateColumn:
                    // Rotate column inst.First by inst.Second positions down
                    var colBackup = new bool[height];
                    for (int y = 0; y < height; y++)
                    {
                        colBackup[(y + inst.Second) % height] = screen[inst.First, y];
                    }
                    for (int y = 0; y < height; y++)
                    {
                        screen[inst.First, y] = colBackup[y];
                    }
                    break;
            }
        }
        
        return screen;
    }

    private static int CountLitPixels(bool[,] screen)
    {
        int count = 0;
        for (int x = 0; x < screen.GetLength(0); x++)
        {
            for (int y = 0; y < screen.GetLength(1); y++)
            {
                if (screen[x, y]) count++;
            }
        }
        return count;
    }

    [Solveable("2016/Puzzles/Day8.txt", "Day 8 part 1", 8)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        // Test uses 7x3, actual puzzle uses 50x6
        int width = filename.Contains("test") ? 7 : 50;
        int height = filename.Contains("test") ? 3 : 6;
        
        var screen = ProcessInstructions(instructions, width, height);
        var litPixels = CountLitPixels(screen);
        
        return new SolutionResult(litPixels.ToString());
    }

    [Solveable("2016/Puzzles/Day8.txt", "Day 8 part 2", 8)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        int width = filename.Contains("test") ? 7 : 50;
        int height = filename.Contains("test") ? 3 : 6;
        
        var screen = ProcessInstructions(instructions, width, height);
        
        // Print the screen to see the letters
        printer.Print("");
        for (int y = 0; y < height; y++)
        {
            var line = "";
            for (int x = 0; x < width; x++)
            {
                line += screen[x, y] ? "#" : ".";
            }
            printer.Print(line);
        }
        
        return new SolutionResult("");
    }


}

public record Instruction(Command Command, int First, int Second) { }

public enum Command
{
    Rect,
    RotateRow,
    RotateColumn
}
