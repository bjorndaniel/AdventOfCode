namespace AoC2022;
public static class Day15
{
    public static IEnumerable<Sensor> ParseInput(string filename)
    {
        var result = new List<Sensor>();
        var lines = File.ReadAllLines(filename);
        foreach (var l in lines)
        {
            var sensorString = l.Split(':')[0].Replace("Sensor at x=", "").Replace("y=", "").Replace(" ", "").Split(',');
            var sp = new Point(int.Parse(sensorString[0]), int.Parse(sensorString[1]));
            var beaconString = l.Split(':')[1].Replace("closest beacon is at x=", "").Replace("y=", "").Replace(" ", "").Split(',');
            var bp = new Point(int.Parse(beaconString[0]), int.Parse(beaconString[1]));
            result.Add(new Sensor(sp, new Beacon(bp)));
        }

        return result;
    }

    public static int SolvePart1(string filename, int targetRow)
    {
        var sensors = ParseInput(filename);
        var unavailable = sensors.Where(_ => _.GetAllPointsCovered(targetRow).Any(_ => _.Y == targetRow)).SelectMany(_ => _.GetAllPointsCovered(targetRow).Select(_ => _.X));
        var beaconsOnRow = sensors.Where(_ => _.Beacon.Position.Y == targetRow).Select(_ => _.Beacon.Position.X).Distinct();
        return unavailable.Distinct().Except(beaconsOnRow).Count();
    }

    public static long SolvePart2(string filename, int lower, int upper)
    {
        var sensors = ParseInput(filename);
        var queue = new Queue<Sensor>(sensors);
        var first = queue.Dequeue();
        var uncovered = new List<Point>();
        while (queue.Any())
        {
            foreach (var s in sensors)
            {
                if (s == first)
                {
                    continue;
                }

                var (covered, p) = first.GetBorder(s, upper);
                if (covered)
                {
                    if (uncovered.Contains(p.Value))
                    {
                        uncovered.Remove(p.Value);
                    }
                    first = queue.Dequeue();
                    continue;
                }
                if (p.HasValue && !uncovered.Contains(p.Value))
                {
                    uncovered.Add(p.Value);
                }
            }

            first = queue.Dequeue();
            Console.WriteLine($"Queue: {queue.Count()}");
        }
        return uncovered.First().X * 4000000 + uncovered.First().Y;
        //foreach (var s in sensors)
        //{
        //    var scannerMatrix =
        //}

        //var coveredPoints = sensors.SelectMany(_ => _.GetAllPointsCovered(i, lower, upper, true));



        ////var watch = new Stopwatch();
        ////var rows = new Dictionary<int, IEnumerable<int>>();
        ////watch.Start();
        //for (int i = lower; i < upper; i++)
        //{
        //    //    var x = sensors.SelectMany(_ => _.GetAllPointsCovered(i, lower, upper, true));
        //    //    var unavailable = x.Where(_ => _.Y == i && _.X >= 0 && _.X <= upper).Select(_ => _.X).Distinct().Order();
        //    //    ////var missing = all.Intersect(unavailable);
        //    //    if (!all.SequenceEqual(unavailable))
        //    //    {
        //    //        return all.Except(unavailable).First() * 4000000 + i;
        //    //    }

        //    //    //var all = Enumerable.Range(lower, upper);
        //    //    if (i % 10 == 0)
        //    //    {
        //    //        Console.WriteLine(DateTime.Now.Ticks.ToString());
        //    //    }
        //    //    //if (unavailable.Count() < 4000001)
        //    //    //{
        //    //    //    Console.Write("asdf");
        //    //    //}
        //    //    //var y = all.Except(unavailable.Distinct()).ToArray();
        //    //    //if (y.Any())
        //    //    //{
        //    //    //}
        //    //    //    var unavailable = sensors.Where(_ => _.GetAllPointsCovered(i, true).Any(_ => _.Y == i)).SelectMany(_ => _.GetAllPointsCovered(i).Select(_ => _.X));
        //    //    //    //var beaconsOnRow = sensors.Where(_ => _.Beacon.Position.Y == i).Select(_ => _.Beacon.Position.X).Distinct();
        //    //    //    //var count = unavailable.Distinct().Count();
        //    //    //    //var all = Enumerable.Range(0, 4000000);
        //    //    //    //if (all.Except(unavailable).Count() == 1)
        //    //    //    //{
        //    //    //    //    var y = all.Except(unavailable);
        //    //    //    //    return sensors.Select(_ => _.Beacon).Single(_ => _.Position.X == y.Single()).Position.X * 4000000 + 1;
        //    //    //    //}
        //}

        return 0;
    }

    public static long ManhattanDistance(Point p1, Point p2) =>
        Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);

}

public record Sensor(Point Position, Beacon Beacon)
{
    public long DistanceToBeacon() =>
        Day15.ManhattanDistance(Position, Beacon.Position);

    public IEnumerable<Point> GetAllPointsCovered(int targetRow = -1, bool getBorder = false)//, int lower = 0, int upper = 0, bool isPart2 = false*/)
    {
        var minX = Position.X - (int)DistanceToBeacon() - 4;
        var maxX = Position.X + (int)DistanceToBeacon() + 4;
        var minY = Position.Y - (int)DistanceToBeacon() - 4;
        var maxY = Position.Y + (int)DistanceToBeacon() + 4;
        //if (isPart2)
        //{
        //    minX = lower;
        //    maxX = upper + 1;
        //}
        var points = new List<Point>();
        //if (targetRow > maxY || targetRow < minY)
        //{
        //    return points;
        //}
        if (targetRow > -1)
        {
            for (int col = minX; col < maxX; col++)
            {
                points.Add(new Point(col, targetRow));
            }
        }
        else
        {
            for (int row = minY; row < maxY; row++)
            {
                for (int col = minX; col < maxX; col++)
                {
                    points.Add(new Point(col, row));
                }
            }
        }
        if (getBorder)
        {
            return points.Where(p => Math.Abs(p.X - Position.X) + Math.Abs(p.Y - Position.Y) <= (DistanceToBeacon() + 1));
        }
        return points.Where(p => Math.Abs(p.X - Position.X) + Math.Abs(p.Y - Position.Y) <= DistanceToBeacon());
    }

    public bool CoversPoint(Point p)
    {
        var distance = DistanceToBeacon();
        if (Day15.ManhattanDistance(Position, p) <= distance)
        {
            return true;
        }
        return false;
    }

    public (bool covers, Point? p) GetBorder(Sensor tester, int upper)
    {
        var points = new List<Point>();
        var distance = (int)DistanceToBeacon();
        var minX = Position.X - distance - 2;
        var maxX = Position.X + distance + 2;
        var minY = Position.Y - distance - 2;
        var maxY = Position.Y + distance + 2;
        //minX = minX < 0 ? 0 : minX;
        //minY = minY < 0 ? 0 : minY;
        //maxX = maxX > upper ? upper : maxX;
        //maxY = maxY > upper ? upper : maxY;
        Point? unCovered = null;
        for (int row = minY; row < maxY; row++)
        {
            for (int col = minX; col < maxX; col++)
            {
                if (col < 0 || col > upper || row < 0 || row > upper)
                {
                    continue;
                }
                unCovered = new Point(col, row);

                if (Day15.ManhattanDistance(unCovered.Value, Position) == distance + 1)
                {
                    if (!tester.CoversPoint(unCovered.Value) && tester.Beacon.Position != unCovered.Value)
                    {
                        return (false, unCovered);
                    }
                }
            }
        }
        return (true, unCovered);
    }

}
public record Beacon(Point Position) { }


