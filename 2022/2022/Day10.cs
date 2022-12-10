namespace AoC2022;
public static class Day10
{
    public static IEnumerable<Operation> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var parts = l.Split(' ');
            var (op, v) = GetOp(parts);
            yield return new Operation(op, v);
        }

        (Instruction instr, int value) GetOp(string[] parts)
        {
            return parts[0] switch
            {
                "noop" => (Instruction.NOOP, 0),
                "addx" => (Instruction.ADDX, int.Parse(parts[1])),
                _ => throw new ArgumentException($"Unknown op {parts[0]}")
            };
        }
    }

    public static int SolvePart1(string filename, int stopAfter) =>
        RunInstructions(filename, stopAfter).signalStrength;


    public static char?[,] SolvePart2(string filename, int stopAfter) =>
        RunInstructions(filename, stopAfter).screen;

    private static (int signalStrength, char?[,] screen) RunInstructions(string filename, int stopAfter)
    {
        var screen = new char?[6, 40];
        var instructions = ParseInput(filename);
        var valueX = 1;
        var cycles = 0;
        var signalStrength = 0;

        foreach (var instr in instructions)
        {
            if (cycles > stopAfter)
            {
                break;
            }

            if (instr.Instr == Instruction.NOOP)
            {
                cycles++;
                DrawPixel(valueX, GetRow(cycles), (cycles - 1) % 40);
                if (cycles % 40 == 20)
                {
                    signalStrength += cycles * valueX;
                }
            }
            else
            {
                cycles++;
                DrawPixel(valueX, GetRow(cycles), (cycles - 1) % 40);
                if (cycles % 40 == 20)
                {
                    signalStrength += cycles * valueX;
                }
                cycles++;
                DrawPixel(valueX, GetRow(cycles), (cycles - 1) % 40);
                if (cycles % 40 == 20)
                {
                    signalStrength += cycles * valueX;
                }
                valueX += instr.Value;
            }
        }
        return (signalStrength, screen);

        void DrawPixel(int pixelPosition, int row, int col)
        {
            var (x1, x2, x3) = GetPixel(pixelPosition);
            if (x1 == col || x2 == col || x3 == col)
            {
                screen[row, col] = '#';
            }
            else
            {
                screen[row, col] = null;
            }
        }

        (int x1, int x2, int x3) GetPixel(int pixelPosition) =>
            (pixelPosition - 1, pixelPosition, pixelPosition + 1);

        int GetRow(int cycle) =>
            ((cycle - 1) / 40);
    }
}

public enum Instruction
{
    NOOP = 1,
    ADDX = 2,

}

public record Operation(Instruction Instr, int Value) { }
