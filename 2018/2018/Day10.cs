namespace AoC2018;
public class Day10
{
    public static Sky ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Point>();
        foreach(var l in lines)
        {
            result.Add(GetPoint(l));
        }
        return new Sky(result);

        Point GetPoint(string line)
        {
            var points = line.Split(">", StringSplitOptions.RemoveEmptyEntries);
            var xy = points.First().Split("<").Last().Split(",");
            var vel = points.Last().Split("<").Last().Split(",");
            var (x, y) = (int.Parse(xy[0]), int.Parse(xy[1]));
            var (velX, velY) = (int.Parse(vel[0]), int.Parse(vel[1]));
            return new Point(x, y, velX, velY);
        }
    }

    [Solveable("2018/Puzzles/Day10.txt", "Day 10 part 1", 10)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var sky = ParseInput(filename);
        var rounds = 1;
        while (true)
        {
            sky.Print(printer);
            sky = new Sky(sky.Points.Select(p => new Point(p.X + p.VelocityX, p.Y + p.VelocityY, p.VelocityX, p.VelocityY)).ToList());
            if(sky.Print(printer))
            {
                if(Console.ReadKey() == new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false))
                {
                    printer.Print("");
                    printer.Flush();
                    break;
                }
            }
            rounds++;
        }
        return new SolutionResult(rounds.ToString());
    }

    [Solveable("2018/Puzzles/Day10.txt", "Day 10 part 2", 10)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    public record Point(int X, int Y, int VelocityX, int VelocityY) { }

    public record Sky(List<Point> Points)
    {
        public bool Print(IPrinter printer)
        {
            var minX = Points.Min(p => p.X);
            var maxX = Points.Max(p => p.X);
            var minY = Points.Min(p => p.Y);
            var maxY = Points.Max(p => p.Y);
            var width = maxX - minX;
            var height = maxY - minY;
            if(width < 100 && height < 100)
            {
                var sky = new char[width + 1, height + 1];
                foreach (var p in Points)
                {
                    sky[p.X - minX, p.Y - minY] = '#';
                }
                for (int y = 0; y <= height; y++)
                {
                    var line = "";
                    for (int x = 0; x <= width; x++)
                    {
                        line += sky[x, y] == '#' ? '#' : '.';
                    }
                    printer.Print(line);
                    printer.Flush();
                }
                printer.Print("");
                printer.Flush();
                return true;
            }
            return false;
        }
    }

}