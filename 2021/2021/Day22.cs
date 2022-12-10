namespace Advent2021;
public class Day22
{
    private static Dictionary<Cube, bool> _cubes = new();

    public static long Initialize(string filename)
    {
        //Leaving first solution in as a reminder...
        //ReadCubes(File.ReadAllLines(filename));
        //return _cubes.Where(_ => _.Value).Count();
        return Reboot(filename, true);
    }

    public static long Reboot(string filename, bool initialize = false)
    {
        var swarms = ReadCubes2(File.ReadAllLines(filename), initialize).ToArray();
        var cubes = new List<CubeSwarm>();
        foreach (var c in swarms)
        {
            var toAdd = new List<CubeSwarm>();
            if (c.IsOn)
            {
                toAdd.Add(c);
            }
            foreach (var added in cubes)
            {
                if (Intersects(c, added))
                {
                    toAdd.Add(CreateIntersectionCube(c, added));
                }
            }
            cubes.AddRange(toAdd);
        }
        return cubes.Where(_ => _.IsOn).Sum(_ => _.GetCubeCount()) - cubes.Where(_ => !_.IsOn).Sum(_ => _.GetCubeCount());
    }

    public static void ReadCubes(IEnumerable<string> input)
    {
        var counter = 1;
        foreach (var line in input)
        {
            Console.WriteLine($"instrucion: {counter} of {input.Count()}");
            var lineCubes = new List<Cube>();
            var turnOn = line.Split(' ')[0] == "on" ? true : false;
            var dimensions = line.Split(' ')[1].Split(',');

            var temp = dimensions.Select(_ =>
            {
                var t1 = int.Parse(_.Split("=")[1].Split("..")[0]);
                var t2 = int.Parse(_.Split("=")[1].Split("..")[1]);
                return (t1, t2);
            }).Where(_ => IsInBounds(_)).ToArray();
            if (temp.Length > 0)
            {
                for (int x = temp[0].t1; x < temp[0].t2 + 1; x++)
                {
                    for (int y = temp[1].t1; y < temp[1].t2 + 1; y++)
                    {
                        for (int z = temp[2].t1; z < temp[2].t2 + 1; z++)
                        {
                            var cube = new Cube { IsOn = turnOn };
                            cube.X = x;
                            cube.Y = y;
                            cube.Z = z;
                            if (_cubes.ContainsKey(cube))
                            {
                                _cubes[cube] = turnOn;
                            }
                            else
                            {
                                _cubes.Add(cube, turnOn);
                            }
                        }
                    }
                }
            }
            counter++;
        }
    }

    //After a 100 failed attemps I borrowed this from
    //https://topaz.github.io/paste/#XQAAAQCeCQAAAAAAAAAX4HyCZDlA0DjWF7Uex1t2hvV6PgXFmsOKCu6h1w9SHLhwm9D5DMo5XPm0mHFeBorDrQ2KvK4LZKZ0g4UjFrHOHToypLTCOcR7IQd94jzGRRDp+mNIqgouKQAjO5CksoUKAnGdXx66s2nBFrq/LH1IGTIG/ZdJ6ZFyf3nkUisYxwp79ThUyrCQTsKeiKqWuLNWEPCdWlvKJYIdMeL9CQuWqRpZT+EE8M7wc5+ilfzBc8j7+9Jvpkn96EcGFAbRcGG9HgNTIWCa0o7qsyZoe4Y1Qcz3UaAHSbCOGuuYyyedy3J39Rcc0BK7dnI8U82t5qjZKvqwwtiTNIv9dszCFCt3gYztLgtVY3F6uhW8nvrAV94RVEGfw64oO6Ewu970qWbWVt6KiUNgP5JWmDWspJLAaVKi7pdT5SR0QzTq3EYRYcOOfE4fLVyTH7sMP4h9cmhckrTALaarPyLIPZpSxOpPt1tJLaxhElka3gTtlelq8CFiRWS48CtZst8odQFjzaC6HYKvJA/UyHKjuFbG0IBLJw/i6s/28CX8KgyjpmXxdHK+i1QioqxQpJCzRKa0c7SibfC/4d6T9ftgJYggiludts/oW0Wrmz9GXf5w/wd0FEoGwrRIXbCZhLWpPPH+VmKvrnOwcA3EvjtTVw9F8jR6zipPzkRxZClKjLbtooW2STzbh1mY0knX+2RKhoYqY9tanf80LKsiwdfqvFxeKfITxWB1WbiLQe3qHx7Ipu60O1y132xGIaMOPY78IPJK04LbbPhBXd6jrg9Vb/aObxRzKUIBw3ExsZDAA5i/jzszlvQ7XOZ/RsMzRD9ukkEqFG5aXKN8GHgr2tRbskvS3gojIINrJCSVa1xCLN1UnBu8h3jHhGhMJcZyFJ9brNh+Xmt2R5tEuacbl19dQldb+XsKByvMlF0vTqZwqsOjk4ZbB8H70Wrzm+hh2r0fX7HDSZ7imKrqDU52/yD5PRI16Z2GcnphUb8RxzXu7qy3octe/e7WNmxEJwm04oyt9DN4txecfc7iYyR/H47vfdjHGC94GWJpUBTcZlFefH9xGSYB4FjqRrCrRI60dN6RFyVNU1atZ/+UXIrwhzGzyUAnl/9tis+N/2Q9a37NpQdC5zCxaDs2tqW7obD8UwIw0MLlyiwICMKxhCl0toa8R8ozOaZ7OMEErZ41EEq+1k4aD//53Krc
    public static CubeSwarm CreateIntersectionCube(CubeSwarm c1, CubeSwarm c2)
    {
        return new CubeSwarm
        {
            XMin = Math.Max(c1.XMin, c2.XMin),
            XMax = Math.Min(c1.XMax, c2.XMax),
            YMin = Math.Max(c1.YMin, c2.YMin),
            YMax = Math.Min(c1.YMax, c2.YMax),
            ZMin = Math.Max(c1.ZMin, c2.ZMin),
            ZMax = Math.Min(c1.ZMax, c2.ZMax),
            IsOn = !c2.IsOn
        };
    }

    public static IEnumerable<CubeSwarm> ReadCubes2(IEnumerable<string> input, bool initialize)
    {
        var counter = 1;
        var swarms = new List<CubeSwarm>();
        foreach (var line in input)
        {
            var lineCubes = new List<Cube>();
            var turnOn = line.Split(' ')[0] == "on" ? true : false;
            var dimensionString = line.Split(' ')[1].Split(',');
            var dimensions = dimensionString.Select(_ =>
            {
                var min = int.Parse(_.Split("=")[1].Split("..")[0]);
                var max = int.Parse(_.Split("=")[1].Split("..")[1]);
                return (min, max);
            }).ToArray();
            if (initialize)
            {
                dimensions = dimensions.Where(_ => IsInBounds(_)).ToArray();
            }
            if (dimensions.Length > 0)
            {
                var swarm = new CubeSwarm
                {

                    XMin = dimensions[0].min,
                    XMax = dimensions[0].max,
                    YMin = dimensions[1].min,
                    YMax = dimensions[1].max,
                    ZMin = dimensions[2].min,
                    ZMax = dimensions[2].max,
                    IsOn = turnOn,
                };
                counter++;
                yield return swarm;
            }
        }
    }

    public static bool Intersects(CubeSwarm c1, CubeSwarm c2)
    {
        if (
           ((c1.XMin <= c2.XMin && c2.XMin <= c1.XMax) || (c2.XMin <= c1.XMin && c1.XMin <= c2.XMax)) &&
           ((c1.YMin <= c2.YMin && c2.YMin <= c1.YMax) || (c2.YMin <= c1.YMin && c1.YMin <= c2.YMax)) &&
           ((c1.ZMin <= c2.ZMin && c2.ZMin <= c1.ZMax) || (c2.ZMin <= c1.ZMin && c1.ZMin <= c2.ZMax))
        )
        {
            return true;
        }
        return false;

    }

    private static bool IsInBounds((int t1, int t2) c) =>
        c.t1 >= -50 && c.t2 <= 50;
}

public class CubeSwarm
{
    public long XMin { get; set; }
    public long XMax { get; set; }
    public long YMin { get; set; }
    public long YMax { get; set; }
    public long ZMin { get; set; }
    public long ZMax { get; set; }
    public bool IsOn { get; set; }
    public long Count => GetCubeCount();
    public long GetCubeCount()
    {
        var x = Math.Abs(XMin - XMax - 1);
        var y = Math.Abs(YMin - YMax - 1);
        var z = Math.Abs(ZMin - ZMax - 1);
        return x * y * z;
    }
}

public class Cube
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public bool IsOn { get; set; }
    public override bool Equals(object? obj)
    {
        var c = obj as Cube;
        if (c == null)
        {
            return false;
        }
        return X == c.X && Y == c.Y && Z == c.Z;
    }
    public override int GetHashCode() =>
        X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
}
