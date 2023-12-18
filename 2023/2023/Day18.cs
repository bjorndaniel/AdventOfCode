namespace AoC2023;
public class Day18
{
    public static List<DigInstruction> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<DigInstruction>();
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var direction = GetDirection(parts[0]);
            var meters = long.Parse(parts[1]);
            result.Add(new DigInstruction(direction, meters, parts[2][1..^1]));
        }
        return result;

        DigDirection GetDirection(string direction) => direction switch
        {
            "U" => DigDirection.North,
            "D" => DigDirection.South,
            "R" => DigDirection.East,
            "L" => DigDirection.West,
            _ => throw new Exception($"Unknown direction {direction}")
        };
    }

    [Solveable("2023/Puzzles/Day18.txt", "Day 18 part 1", 18)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var plan = ParseInput(filename);
        var points = new List<(long x, long y)>
        {
            (0, 0)
        };
        foreach (var instruction in plan)
        {
            var p = (points.Last().x, points.Last().y);
            points.Add(GetPoint(p, instruction));
        }
        var perimeter = CalculatePolygonPerimeter(points);
        var cubes = CalculateCubesInsidePolygon(points);
        return new SolutionResult((perimeter + cubes).ToString());
    }

    [Solveable("2023/Puzzles/Day18.txt", "Day 18 part 2", 18)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var plan = ParseInput(filename);
        var newPlan = new List<DigInstruction>();
        var points = new List<(long x, long y)>
            {
                (0, 0)
            };
        foreach (var instruction in plan)
        {
            var newDirection = GetDirection(instruction.Color[^1]);
            var newDistance = long.Parse(instruction.Color[1..^1], System.Globalization.NumberStyles.HexNumber);
            newPlan.Add(new DigInstruction(newDirection, newDistance, instruction.Color));
        }
        foreach (var instruction in newPlan)
        {
            var p = (points.Last().x, points.Last().y);
            points.Add(GetPoint(p, instruction));
        }
        var perimeter = CalculatePolygonPerimeter(points);
        //var cubes = CalculateCubesInsidePolygon(points);
        return new SolutionResult((perimeter /*+ cubes*/).ToString());

        DigDirection GetDirection(char instruction) => instruction switch
        {
            '0' => DigDirection.East,
            '1' => DigDirection.South,
            '2' => DigDirection.West,
            '3' => DigDirection.North,
            _ => throw new Exception($"Unknown direction {instruction}")
        };
    }

    public record DigInstruction(DigDirection Direction, long Meters, string Color) { }

    public enum DigDirection
    {
        North,
        South,
        East,
        West
    }

    private static (long x, long y) GetPoint((long x, long y) point, DigInstruction instruction)
    {
        switch (instruction.Direction)
        {
            case DigDirection.North:
                return (point.x, point.y - instruction.Meters);
            case DigDirection.South:
                return (point.x, point.y + instruction.Meters);
            case DigDirection.East:
                return (point.x + instruction.Meters, point.y);
            case DigDirection.West:
                return (point.x - instruction.Meters, point.y);
            default:
                throw new Exception($"Unknown direction {instruction.Direction}");
        };
    }

    private static long CalculateCubesInsidePolygon(List<(long x, long y)> points)
    {
        var minX = points.Min(p => p.x);
        var maxX = points.Max(p => p.x);
        var minY = points.Min(p => p.y);
        var maxY = points.Max(p => p.y);

        var count = 0L;

        for (var x = minX; x <= maxX; x++)
        {
            for (var y = minY; y <= maxY; y++)
            {
                if (IsPointInPolygon(points, (x, y)))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool IsPointInPolygon(List<(long x, long y)> points, (long x, long y) point)
    {

        var isInside = false;
        for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
        {
            if (((points[i].y > point.y) != (points[j].y > point.y)) &&
                (point.x < (points[j].x - points[i].x) * (point.y - points[i].y) / (points[j].y - points[i].y) + points[i].x))
            {
                isInside = !isInside;
            }
        }
        for (int i = 0, j = points.Count - 1; i < points.Count; j = i++)
        {
            if ((points[i].x - point.x) * (points[j].y - point.y) == (points[j].x - point.x) * (points[i].y - point.y) &&
                (point.x - points[i].x) * (point.x - points[j].x) <= 0 && (point.y - points[i].y) * (point.y - points[j].y) <= 0)
            {
                return false; // The point is on the perimeter, so it's not considered inside
            }
        }

        return isInside;
    }

    private static long CalculatePolygonPerimeter(List<(long x, long y)> points)
    {
        var perimeter = 0.0;

        for (int i = 0; i < points.Count; i++)
        {
            var nextIndex = (i + 1) % points.Count;
            var dx = points[nextIndex].x - points[i].x;
            var dy = points[nextIndex].y - points[i].y;
            perimeter += Math.Sqrt(dx * dx + dy * dy);
        }

        return (long)perimeter;
    }

}