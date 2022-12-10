namespace Advent2021;
public class Day19
{
    public static (Scanner origin, long manhattan) CountBeacons(string filename)
    {
        var scanners = ReadScanners(filename);
        var beacons = scanners.SelectMany(_ => _.Beacons);
        var origin = scanners.First();
        var addedToOrigin = new List<string>();
        addedToOrigin.Add(origin.Name);
        var remaining = scanners.Where(_ => _.Name != origin.Name).ToList();
        while (addedToOrigin.Count < scanners.Count())
        {
            foreach (var s in remaining.Where(_ => !addedToOrigin.Contains(_.Name)))
            {
                var overlapping = GetOverlappingBeacons(origin, s);
                if (overlapping.Count() >= 12)
                {
                    origin.Beacons.AddRange(MoveBeacons(origin, s, overlapping));
                    addedToOrigin.Add(s.Name);
                    origin.GetDistances();
                }
            }
        }
        remaining.Add(origin);
        var manhattan = GetLargestManhattanDistance(remaining);
        return (origin, manhattan);
    }

    public static long GetLargestManhattanDistance(IEnumerable<Scanner> scanners)
    {
        var distances = new List<long>();
        foreach (var s1 in scanners)
        {
            foreach (var s2 in scanners.Where(_ => _.Name != s1.Name))
            {
                distances.Add(Math.Abs(s1.X - s2.X) + Math.Abs(s1.Y - s2.Y) + Math.Abs(s1.Z - s2.Z));
            }
        }

        return distances.Max();
    }

    public static long CountOverlappingBeacons(Scanner origin, Scanner target)
    {
        var result = new List<Beacon>();
        var count = 0;
        foreach (var b in origin.Beacons)
        {
            var targetBeacon = target.Beacons.FirstOrDefault(_ => _.Distances.Intersect(b.Distances).Count() > 9);
            if (targetBeacon != null)
            {
                count++;
            }
        }
        return count;
    }

    public static IEnumerable<(Beacon ob, Beacon tb)> GetOverlappingBeacons(Scanner origin, Scanner target)
    {
        var result = new List<Beacon>();
        foreach (var b in origin.Beacons)
        {
            var targetBeacon = target.Beacons.FirstOrDefault(_ => _.Distances.Intersect(b.Distances).Count() > 10);
            if (targetBeacon != null)
            {
                yield return (b, targetBeacon);
            }
        }
    }

    public static List<Beacon> MoveBeacons(Scanner origin, Scanner target, IEnumerable<(Beacon ob, Beacon tb)> beacons)
    {
        var overlapping = beacons.Select(_ => _.tb).Select(_ => _.Id);
        var toAdd = target.Beacons.Where(_ => !overlapping.Contains(_.Id));
        var referencePair = beacons.First();
        var referencePair1 = beacons.Last();
        var (x, y, z, rotation) = GetOffsets((referencePair.ob.Vector, referencePair.tb.Vector), (referencePair1.ob.Vector, referencePair1.tb.Vector));
        target.X = origin.X + x;
        target.Y = origin.Y + y;
        target.Z = origin.Z + z;
        Parallel.ForEach(target.Beacons, b =>
        {
            if (rotation != null)
            {
                var v = rotation(b.Vector);
                b.X = v.X;
                b.Y = v.Y;
                b.Z = v.Z;
            }
            b.X = target.X + b.X;
            b.Y = target.Y + b.Y;
            b.Z = target.Z + b.Z;
        });
        return toAdd.ToList();
    }

    private static (long x, long y, long z, Func<BeaconVector, BeaconVector> rotation) GetOffsets((BeaconVector s0v1, BeaconVector s1v1) group0, (BeaconVector s0v2, BeaconVector s1v2) group1)
    {
        foreach (var r in GetRotations())
        {
            var rotated0 = r(group0.s1v1);
            foreach (var r1 in GetRotations())
            {
                var rotated1 = r1(group1.s1v2);
                var distance = GetDistances(group0.s0v1, rotated0);
                var distance1 = GetDistances(group1.s0v2, rotated1);
                if (distance.x == distance1.x && distance.y == distance1.y && distance.z == distance1.z)
                {
                    return (distance1.x, distance1.y, distance1.z, r);
                }
            }
        }
        return (-1, -1, -1, null!);
    }

    private static (long x, long y, long z) GetDistances(BeaconVector v1, BeaconVector v2) =>
        (v1.X - (v2.X), v1.Y - v2.Y, v1.Z - v2.Z);

    public static List<Scanner> ReadScanners(string filename)
    {
        var result = new List<Scanner>();
        var lines = File.ReadAllLines(filename);
        var first = GetName(lines[0]);
        var scanner = new Scanner { Name = first };
        result.Add(scanner);
        for (long i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(scanner.Name))
            {
                scanner.Name = GetName(lines[i]);
                result.Add(scanner);
                continue;
            }
            if (string.IsNullOrEmpty(lines[i]))
            {
                scanner = new();
                continue;
            }
            var coords = lines[i].Split(',');
            scanner.Beacons.Add(new Beacon { X = long.Parse(coords[0]), Y = long.Parse(coords[1]), Z = long.Parse(coords[2]) });
        }
        result.ForEach(_ => _.GetDistances());
        return result;
    }

    private static string GetName(string v)
    {
        var words = v.Split(' ');
        return $"{words[1]} {words[2]}";
    }

    //Copied from 2 different answers on reddit to get all, there are probably some duplicates but at least it works...
    private static IEnumerable<Func<BeaconVector, BeaconVector>> GetRotations()
    {
        yield return v => new(v.X, v.Y, v.Z);
        yield return v => new(v.Y, v.Z, v.X);
        yield return v => new(-v.Y, v.X, v.Z);
        yield return v => new(-v.X, -v.Y, v.Z);

        yield return v => new(-v.X, -v.Y, v.Z);
        yield return v => new(v.Z, v.Y, -v.X);
        yield return v => new(v.Z, v.X, v.Y);
        yield return v => new(v.Z, -v.Y, v.X);

        yield return v => new(v.Z, -v.X, -v.Y);
        yield return v => new(-v.X, v.Y, -v.Z);
        yield return v => new(v.Y, v.X, -v.Z);
        yield return v => new(v.X, -v.Y, -v.Z);

        yield return v => new(-v.Y, -v.X, -v.Z);
        yield return v => new(-v.Z, v.Y, v.X);
        yield return v => new(-v.Z, v.X, -v.Y);
        yield return v => new(-v.Z, -v.Y, -v.X);

        yield return v => new(-v.Y, -v.Z, v.X);
        yield return v => new(-v.X, -v.Z, -v.Y);
        yield return v => new(-v.Z, -v.X, v.Y);
        yield return v => new(v.X, -v.Z, v.Y);

        yield return v => new(v.X, v.Z, -v.Y);
        yield return v => new(-v.Y, v.Z, -v.X);
        yield return v => new(-v.X, v.Z, v.Y);
        yield return v => new(v.Y, -v.Z, -v.X);

        yield return v => new(v.X, -v.Y, v.Z);
        yield return v => new(v.X, v.Y, -v.Z);
        yield return v => new(v.X, v.Z, v.Y);
        yield return v => new(v.X, -v.Z, -v.Y);

        yield return v => new(-v.X, -v.Y, v.Z);
        yield return v => new(-v.X, v.Y, -v.Z);
        yield return v => new(-v.X, v.Z, v.Y);
        yield return v => new(-v.X, -v.Z, -v.Y);

        yield return v => new(-v.Y, v.Z, -v.X);
        yield return v => new(-v.Y, -v.Z, v.X);
        yield return v => new(-v.Y, v.X, v.Z);
        yield return v => new(-v.Y, -v.X, -v.Z);

        yield return v => new(v.Y, -v.X, v.Z);
        yield return v => new(v.Y, v.X, -v.Z);
        yield return v => new(v.Y, v.Z, v.X);
        yield return v => new(v.Y, -v.Z, -v.X);

        yield return v => new(v.Z, -v.X, -v.Y);
        yield return v => new(v.Z, v.X, v.Y);
        yield return v => new(v.Z, -v.Y, v.X);
        yield return v => new(v.Z, v.Y, -v.X);

        yield return v => new(-v.Z, v.X, -v.Y);
        yield return v => new(-v.Z, -v.X, v.Y);
        yield return v => new(-v.Z, v.Y, v.X);
        yield return v => new(-v.Z, -v.Y, -v.X);
        yield return v => new(-v.X, -v.Y, -v.Z);
    }

    public class Scanner
    {
        public string Name { get; set; } = string.Empty;
        public List<Beacon> Beacons { get; set; } = new();
        public BeaconVector Vector => new(X, Y, Z);
        public (Beacon? b1, Beacon? b2) GetBeaconsWithDistance(double distance)
        {
            foreach (var b in Beacons)
            {
                foreach (var b1 in Beacons)
                {
                    if (b.DistanceTo((b1.X, b1.Y, b1.Z)) == distance)
                    {
                        return (b, b1);
                    }
                }
            }
            return (null, null);
        }
        public List<(string originScanner, List<Beacon> beacons)> MovedBeacons { get; set; } = new();
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public bool MovedlongoOrigin =>
            X != 0 && Y != 0 && Z != 0;
        public void GetDistances()
        {
            foreach (var b1 in Beacons)
            {
                foreach (var b2 in Beacons.Where(_ => _.Id != b1.Id))
                {
                    var d = b1.DistanceTo((b2.X, b2.Y, b2.Z));
                    if (d > 0)
                    {
                        b1.Distances.Add(d);
                    }
                }
            }
        }
        public long GetLargestManhattanDistance() =>
            (from a in Beacons
             from b in Beacons
             where a != b
             select Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z)).Max();
    }

    public class Beacon
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public List<double> Distances { get; set; } = new();
        public string Fingerprlong
        {
            get
            {
                var distance = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
                var x = Math.Abs(X);
                var y = Math.Abs(Y);
                var z = Math.Abs(Z);
                var min = x < y ? Math.Min(x, z) : Math.Min(y, z);
                var max = x > y ? Math.Max(x, z) : Math.Max(y, z);
                return $"{distance}{min}{max}";
            }
        }
        public BeaconVector Vector => new(X, Y, Z);
        public double DistanceTo((long x, long y, long z) other) =>
            Math.Sqrt(Math.Pow(X - other.x, 2) + Math.Pow(Y - other.y, 2) + Math.Pow(Z - other.z, 2));
        public override bool Equals(object? obj)
        {
            var target = obj as Beacon;
            return X == target?.X && Y == target?.Y && Z == target?.Z;
        }
        public override int GetHashCode() =>
            X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }

    public class BeaconVector
    {
        public BeaconVector(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
    }
}

