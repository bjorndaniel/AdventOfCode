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

        static DigDirection GetDirection(string direction) => direction switch
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
        var points = GetPoints(plan);
        var area = CalculatePolygonArea(points, CalculatePolygonPerimeter(points));
        return new SolutionResult(area.ToString());
    }

    [Solveable("2023/Puzzles/Day18.txt", "Day 18 part 2", 18)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var plan = ParseInput(filename);
        var newPlan = new List<DigInstruction>();

        foreach (var instruction in plan)
        {
            var newDirection = GetDirection(instruction.Color[^1]);
            var newDistance = long.Parse(instruction.Color[1..^1], System.Globalization.NumberStyles.HexNumber);
            newPlan.Add(new DigInstruction(newDirection, newDistance, instruction.Color));
        }
        List<(long x, long y)> points = GetPoints(newPlan);

        var area = CalculatePolygonArea(points, CalculatePolygonPerimeter(points));
        return new SolutionResult(area.ToString());

        static DigDirection GetDirection(char instruction) => instruction switch
        {
            '0' => DigDirection.East,
            '1' => DigDirection.South,
            '2' => DigDirection.West,
            '3' => DigDirection.North,
            _ => throw new Exception($"Unknown direction {instruction}")
        };

        static long CalculatePolygonArea(List<(long x, long y)> vertices, long perimeter)
        {
            var n = vertices.Count;
            var area = 0L;
            for (int i = 0; i < n; i++)
            {
                var j = (i + 1) % n;
                area += (vertices[i].x * vertices[j].y) - (vertices[j].x * vertices[i].y); // Calculate the signed area of the triangle formed by the current vertex, the next vertex, and the origin
            }
            return Math.Abs(area / 2) + (perimeter / 2) + 1;
        }
    }

    private static List<(long x, long y)> GetPoints(List<DigInstruction> newPlan)
    {
        var points = new List<(long x, long y)>
            {
                (0, 0)
            };
        foreach (var instruction in newPlan)
        {
            var p = (points.Last().x, points.Last().y);
            points.Add(GetPoint(p, instruction));
        }

        return points;
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
        return instruction.Direction switch
        {
            DigDirection.North => (point.x, point.y - instruction.Meters),
            DigDirection.South => (point.x, point.y + instruction.Meters),
            DigDirection.East => (point.x + instruction.Meters, point.y),
            DigDirection.West => (point.x - instruction.Meters, point.y),
            _ => throw new Exception($"Unknown direction {instruction.Direction}"),
        };
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

    private static long CalculatePolygonArea(List<(long x, long y)> vertices, long perimeter)
    {
        var n = vertices.Count;
        var area = 0L;
        for (int i = 0; i < n; i++)
        {
            var j = (i + 1) % n;
            area += (vertices[i].x * vertices[j].y) - (vertices[j].x * vertices[i].y);
        }
        return Math.Abs(area / 2) + (perimeter / 2) + 1;
    }

}