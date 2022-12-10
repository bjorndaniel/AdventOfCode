namespace Advent2021;
public class Day5
{
    private static object _lockObj = new object();
    public static int GetOverlappingPoints(string filename, bool includeDiagonal = false)
    {
        var allLines = GetLines(filename);
        var lines = allLines.Where(_ => (_.X1 == _.X2) || (_.Y1 == _.Y2)).ToList();
        if (includeDiagonal)
        {
            var diag = allLines.Where(_ =>
            ((_.Y2 - _.Y1) == (_.X2 - _.X1)) ||
            ((_.Y2 - _.Y1) == (_.X1 - _.X2))
                ).ToList();
            lines.AddRange(diag);
        }

        var maxX = lines.Select(_ => _.X1 > _.X2 ? _.X1 : _.X2).Max();
        var maxY = lines.Select(_ => _.Y1 > _.Y2 ? _.Y1 : _.Y2).Max();
        var max = maxX > maxY ? maxX : maxY;
        var board = CreateBoard(max, max);
        Parallel.ForEach(lines, (line) =>
        {
            Parallel.ForEach(board.Points.Where(_ => line.PointIsOnLine(_.X, _.Y)), (p) =>
            {
                lock (_lockObj)
                {
                    p.HitCount++;
                }
            });
        });
        return board.Points.Count(_ => _.HitCount > 1);
    }

    private static IEnumerable<Line> GetLines(string filename)
    {
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var points = line.Split(" -> ");
            var start = points[0].Split(',');
            var end = points[1].Split(',');
            var x1 = int.Parse(start[0]);
            var x2 = int.Parse(end[0]);
            var y1 = int.Parse(start[1]);
            var y2 = int.Parse(end[1]);
            yield return new Line(x1, y1, x2, y2);
        }
    }

    public static Board CreateBoard(int maxX, int maxY)
    {
        var board = new Board();
        for (var i = 0; i <= maxX; i++)
        {
            for (int j = 0; j <= maxY; j++)
            {
                board.Points.Add(new Point(i, j));
            }
        }
        return board;
    }

    public class Board
    {
        public List<Point> Points { get; set; } = new();
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int HitCount { get; set; }
    }

    public class Line
    {
        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            GetPoints(x1, y1, x2, y2);
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public int X1 { get; private set; }
        public int X2 { get; private set; }
        public int Y1 { get; private set; }
        public int Y2 { get; private set; }
        private Dictionary<(int x, int y), Point> Points { get; set; } = new();

        public bool PointIsOnLine(int x, int y) =>
            Points.ContainsKey((x, y));

        //Adapted from: https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
        private void GetPoints(int x, int y, int x2, int y2)
        {
            var w = x2 - x;
            var h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0)
            {
                dx1 = -1;
            }
            else if (w > 0)
            {
                dx1 = 1;
            }
            if (h < 0)
            {
                dy1 = -1;
            }
            else if (h > 0)
            {
                dy1 = 1;
            }
            if (w < 0)
            {
                dx2 = -1;
            }
            else if (w > 0)
            {
                dx2 = 1;
            }
            var longest = Math.Abs(w);
            var shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0)
                {
                    dy2 = -1;
                }
                else if (h > 0)
                {
                    dy2 = 1;
                }
                dx2 = 0;
            }
            var numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                Points.Add((x, y), new Point(x, y));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
    }
}
