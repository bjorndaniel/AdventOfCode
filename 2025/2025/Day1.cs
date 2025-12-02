namespace AoC2025;

public class Day1
{
    public static List<Rotation> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Rotation>();
        foreach (var line in lines)
        {
            var dir = line[0] == 'L' ? Direction.Left : Direction.Right;
            var steps = int.Parse(line[1..]);
            result.Add(new Rotation(dir, steps));
        }
        return result;
    }

    [Solveable("2025/Puzzles/Day1.txt", "Day 1 part 1", 1)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var rotations = ParseInput(filename);
        var starting = 50;
        var answer = 0;
        foreach (var rot in rotations)
        {
            if (rot.Direction == Direction.Left)
            {
                starting -= rot.Steps;
            }
            else
            {
                starting += rot.Steps;
            }
            starting = ((starting % 100) + 100) % 100;

            if (starting == 0)
            {
                answer++;
            }

        }
        return new SolutionResult(answer.ToString());
    }

    [Solveable("2025/Puzzles/Day1.txt", "Day 1 part 2", 1)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var rotations = ParseInput(filename);
        var position = 50;
        var answer = 0;

        foreach (var rot in rotations)
        {
            var prev = position;

            if (rot.Direction == Direction.Left)
            {
                position -= rot.Steps;
            }
            else
            {
                position += rot.Steps;
            }

            var a = Math.Min(prev, position);
            var b = Math.Max(prev, position);

            var strictUpper = Math.Floor((b - 1) / 100.0);
            var strictLower = Math.Floor(a / 100.0);
            var strictCount = (int)(strictUpper - strictLower);

            answer += strictCount;

            if (position % 100 == 0)
            {
                answer++;
            }
        }
        return new SolutionResult(answer.ToString());
    }


}

public record Rotation(Direction Direction, int Steps);

public enum Direction
{
    Left,
    Right,
}