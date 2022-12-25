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
            var sp = new PointL(long.Parse(sensorString[0]), long.Parse(sensorString[1]));
            var beaconString = l.Split(':')[1].Replace("closest beacon is at x=", "").Replace("y=", "").Replace(" ", "").Split(',');
            var bp = new PointL(long.Parse(beaconString[0]), long.Parse(beaconString[1]));
            result.Add(new Sensor(sp, new Beacon(bp)));
        }

        return result;
    }

    public static long SolvePart1(string filename, long targetRow)
    {
        var sensors = ParseInput(filename);
        var unavailable = sensors.Where(_ => _.GetAllPointsCovered(targetRow).Any(_ => _.Y == targetRow)).SelectMany(_ => _.GetAllPointsCovered(targetRow).Select(_ => _.X));
        var beaconsOnRow = sensors.Where(_ => _.Beacon.Position.Y == targetRow).Select(_ => _.Beacon.Position.X).Distinct();
        return unavailable.Distinct().Except(beaconsOnRow).Count();
    }

    public static long SolvePart2(string filename, long lower, long upper)
    {
        var sensors = ParseInput(filename).ToList();
        var ranges = new BlockingCollection<(long low, long high)>();
        for (long i = lower; i < upper; i++)
        {
            //Parallel.ForEach(sensors, s =>
            //{
            foreach (var s in sensors)
            {
                var (success, range) = s.GetRange(i, lower, upper);
                if (success)
                {
                    ranges.Add(range);
                }
            }
            if (!ranges.Any())
            {
                continue;
            }
            var lowest = ranges.OrderBy(_ => _.low);
            var newRange = lowest.First();
            foreach (var r in lowest.Skip(1))
            {
                if (newRange.low == lower && newRange.high == upper)
                {
                    break;
                }
                if (Overlap(newRange, r))
                {
                    newRange.high = Math.Max(newRange.high, r.high);
                    newRange.low = Math.Min(newRange.low, r.low);
                    continue;
                }

                return (r.low - 1) * (long)4000000 + i;
            }
            ranges = new BlockingCollection<(long low, long high)>();
        }
        return 0;
    }

    public static long ManhattanDistance(PointL p1, PointL p2) =>
        Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);

    public static bool Overlap((long low, long high) r1, (long low, long high) r2)
    {
        if (r1.low <= r2.high && r2.low <= r1.high)
        {
            return true;
        }
        else if (r1.high - r2.low == 1)
        {
            return true;
        }
        return false;
    }

}

public record Sensor(PointL Position, Beacon Beacon)
{
    public long DistanceToBeacon() =>
        Day15.ManhattanDistance(Position, Beacon.Position);

    public IEnumerable<PointL> GetAllPointsCovered(long targetRow = -1, bool getBorder = false)//, long lower = 0, long upper = 0, bool isPart2 = false*/)
    {
        var minX = Position.X - (long)DistanceToBeacon() - 4;
        var maxX = Position.X + (long)DistanceToBeacon() + 4;
        var minY = Position.Y - (long)DistanceToBeacon() - 4;
        var maxY = Position.Y + (long)DistanceToBeacon() + 4;
        //if (isPart2)
        //{
        //    minX = lower;
        //    maxX = upper + 1;
        //}
        var PointLs = new List<PointL>();
        //if (targetRow > maxY || targetRow < minY)
        //{
        //    return PointLs;
        //}
        if (targetRow > -1)
        {
            for (long col = minX; col < maxX; col++)
            {
                PointLs.Add(new PointL(col, targetRow));
            }
        }
        else
        {
            for (long row = minY; row < maxY; row++)
            {
                for (long col = minX; col < maxX; col++)
                {
                    PointLs.Add(new PointL(col, row));
                }
            }
        }
        if (getBorder)
        {
            return PointLs.Where(p => Math.Abs(p.X - Position.X) + Math.Abs(p.Y - Position.Y) <= (DistanceToBeacon() + 1));
        }
        return PointLs.Where(p => Math.Abs(p.X - Position.X) + Math.Abs(p.Y - Position.Y) <= DistanceToBeacon());
    }

    public bool CoversPointL(PointL p)
    {
        var distance = DistanceToBeacon();
        if (Day15.ManhattanDistance(Position, p) <= distance)
        {
            return true;
        }
        return false;
    }

    public IEnumerable<PointL> GetBorders()
    {
        var startX = Position.X;
        var startY = Position.Y;
        var distance = DistanceToBeacon();
        for (long i = 0; i < distance; i++)
        {
            // Move right
            startX += 1;
            // Move up or down
            startY += (i % 2 == 0) ? 1 : -1;
            yield return new PointL(startX, startY);
        }
        //long startX = 0; // starting x coordinate
        //long startY = 0; // starting y coordinate
        //long distance = 3; // Manhattan distance to move

        //for (long i = 0; i < distance; i++)
        //{
        //    // Move right
        //    startX += 1;
        //    // Move up or down
        //    startY += (i % 2 == 0) ? 1 : -1;
        //}
    }

    public (bool inRange, (long low, long high)) GetRange(long row, long lowerBound, long upperBound)
    {
        var distance = DistanceToBeacon();
        var offset = Math.Abs(row - Position.Y);
        var l = Position.X - distance + offset;
        var h = Position.X + distance - offset;
        var (low, high) = (Position.X - distance + offset, Position.X + distance - offset);
        if (!Day15.Overlap((low, high), (lowerBound, upperBound)))
        {
            return (false, (0, 0));
        }
        if (low < lowerBound)
        {
            low = lowerBound;
        }
        if (high > upperBound)
        {
            high = upperBound;
        }
        if (high < low)
        {
            low = high;
        }
        return (true, (low, high));
    }

}

public record Beacon(PointL Position) { }

public record PointL(long X, long Y) { }



