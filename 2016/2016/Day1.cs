namespace AoC2016;

public class Day1
{
    public static List<Coord> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var coords = new List<Coord>();
        var parts = lines[0].Split(" , ".Substring(1));
        foreach (var part in parts)
        {
            var direction = part[0] == 'R' ? Direction.Right : Direction.Left;
            var distance = int.Parse(part[1..]);
            coords.Add(new Coord(direction, distance));
        }
        return coords;
    }

    [Solveable("2016/Puzzles/Day1.txt", "Day 1 part 1", 1)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var coords = ParseInput(filename);
        var startingPoint = (X: 0, Y: 0);
        var facing = 0; // 0 = North, 1 = East, 2 = South, 3 = West
        startingPoint = Move(coords, startingPoint, facing, []);
        return new SolutionResult(Helpers.ManhattanDistance((0, 0), startingPoint).ToString());
    }

    [Solveable("2016/Puzzles/Day1.txt", "Day 1 part 2", 1)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var coords = ParseInput(filename);
        var startingPoint = (X: 0, Y: 0);
        var facing = 0; // 0 = North, 1 = East, 2 = South, 3 = West
        startingPoint = Move(coords, startingPoint, facing, [ startingPoint ], true);
        printer.Print($"First location visited twice: {startingPoint}");
        printer.Flush();
        return new SolutionResult(Helpers.ManhattanDistance((0, 0), startingPoint).ToString());
    }

    private static (int X, int Y) Move(List<Coord> coords, (int X, int Y) startingPoint, int facing, HashSet<(int X, int Y)> visited, bool isPart2 = false)
    {
        foreach (var coord in coords)
        {
            facing += coord.Direction == Direction.Right ? 1 : -1;
            if (facing < 0)
            {
                facing += 4;
            }
            if (facing > 3)
            {
                facing -= 4;
            }

            // Move one step at a time and check visited positions for part 2
            for (int step = 0; step < coord.Distance; step++)
            {
                switch (facing)
                {
                    case 0:
                        startingPoint.Y += 1;
                        break;
                    case 1:
                        startingPoint.X += 1;
                        break;
                    case 2:
                        startingPoint.Y -= 1;
                        break;
                    case 3:
                        startingPoint.X -= 1;
                        break;
                }

                if (isPart2)
                {
                    if (visited.Contains(startingPoint))
                    {
                        return startingPoint;
                    }
                    visited.Add(startingPoint);
                }
            }
        }
        return startingPoint;
    }
}

public record Coord(Direction Direction, int Distance);

public enum Direction
{
    Right,
    Left,
}