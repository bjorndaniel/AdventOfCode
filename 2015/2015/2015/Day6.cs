namespace AoC2015;
public class Day6
{
    public static List<Instruction> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Instruction>();
        foreach (var row in lines)
        {
            var parts = row.Split(' ');
            if (parts[0] == "turn")
            {
                var method = parts[1] == "on" ? LightMethod.TurnOn : LightMethod.TurnOff;
                var start = parts[2].Split(',').Select(int.Parse).ToArray();
                var end = parts[4].Split(',').Select(int.Parse).ToArray();
                result.Add(new Instruction(method, start[0], start[1], end[0], end[1]));
            }
            else
            {
                var method = LightMethod.Toggle;
                var start = parts[1].Split(',').Select(int.Parse).ToArray();
                var end = parts[3].Split(',').Select(int.Parse).ToArray();
                result.Add(new Instruction(method, start[0], start[1], end[0], end[1]));
            }
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day6.txt", "Day6  part 1", 5)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var lights = new int[1000, 1000];
        printer.Print(CountOn(lights).ToString());
        printer.Flush();
        foreach (var instruction in instructions)
        {
            SetRange(lights, instruction.StartX, instruction.StartY, instruction.EndX, instruction.EndY, instruction.Method);
        }

        return new SolutionResult(CountOn(lights).ToString());

        static void SetRange(int[,] lights, int startX, int startY, int endX, int endY, LightMethod method)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    var current = lights[x, y];
                    lights[x, y] = method switch
                    {
                        LightMethod.TurnOn => 1,
                        LightMethod.TurnOff => 0,
                        _ => current == 0 ? 1 : 0,
                    };
                }
            }
        }

        static int CountOn(int[,] lights)
        {
            var lightsOn = 0;
            foreach (var light in lights)
            {
                if (light == 1)
                {
                    lightsOn++;
                }
            }
            return lightsOn;
        }
    }

    [Solveable("2015/Puzzles/Day6.txt", "Day6  part 2", 5)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var instructions = ParseInput(filename);
        var lights = new int[1000, 1000];
        
        foreach (var instruction in instructions)
        {
            SetRange(lights, instruction.StartX, instruction.StartY, instruction.EndX, instruction.EndY, instruction.Method);
        }

        return new SolutionResult(CountBrightness(lights).ToString());

        static void SetRange(int[,] lights, int startX, int startY, int endX, int endY, LightMethod method)
        {
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    var current = lights[x, y];
                    lights[x, y] = method switch
                    {
                        LightMethod.TurnOn => current++,
                        LightMethod.TurnOff => current > 0 ? current-- : 0,
                        _ => current +=2,
                    };
                    lights[x, y] = current;
                }
            }
        }

        static int CountBrightness(int[,] lights)
        {
            var lightsOn = 0;
            foreach (var light in lights)
            {
                lightsOn += light;
            }
            return lightsOn;
        }
    }

    public record Instruction(LightMethod Method, int StartX, int StartY, int EndX, int EndY);

    public enum LightMethod
    {
        TurnOn,
        TurnOff,
        Toggle
    }
}
