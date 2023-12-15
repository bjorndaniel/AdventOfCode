namespace AoC2023;
public class Day14
{
    public static List<Platform> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Platform>();
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                if (lines[row][col] == 'O')
                {
                    result.Add(new Platform(col, row, RockType.Round));
                }
                else if (lines[row][col] == '#')
                {
                    result.Add(new Platform(col, row, RockType.Cube));
                }
            }
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day14.txt", "Day 14 part 1", 14)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var platforms = ParseInput(filename);
        TiltNorth(platforms);
        return new SolutionResult(platforms.Where(_ => _.Type == RockType.Round).Sum(_ => platforms.Max(_ => _.Y) + 1 - _.Y).ToString());
    }

    [Solveable("2023/Puzzles/Day14.txt", "Day 14 part 2", 14, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var timer = new Stopwatch();
        timer.Start();
        var cache = new Dictionary<string, int>();
        var platforms = ParseInput(filename);
        for (int i = 1; i < 1001; i++)
        {
            TiltNorth(platforms);
            TiltWest(platforms);
            TiltSouth(platforms);
            TiltEast(platforms);
            var key = platforms.Select(_ => _.GenerateKey()).Aggregate((a, b) => $"{a};{b}");
            if (cache.TryGetValue(key, out int value))
            {
                var cycle = i - value;
                var remaining = 1000000000 % cycle;
                var state = cache.First(_ => _.Value == remaining).Key; 
                var remainingPlatforms = state.Split(';').Select(_ => Platform.FromKey(_)).ToList();
                return new SolutionResult(remainingPlatforms.Where(_ => _.Type == RockType.Round).Sum(_ => remainingPlatforms.Max(_ => _.Y) + 1 - _.Y).ToString());
            }
            else
            {
                cache.Add(key, i);
            }
        }
        return new SolutionResult(platforms.Where(_ => _.Type == RockType.Round).Sum(_ => platforms.Max(_ => _.Y) + 1 - _.Y).ToString());
    }

    public class Platform(int x, int y, RockType type)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public RockType Type { get; set; } = type;
        public string GenerateKey() => $"{X},{Y},{Type}";

        public static Platform FromKey(string key)
        {
            var parts = key.Split(',');
            return new Platform(int.Parse(parts[0]), int.Parse(parts[1]), (RockType)Enum.Parse(typeof(RockType), parts[2]));
        }   
    }

    public enum RockType
    {
        Round,
        Cube
    }

    private static void TiltNorth(List<Platform> platforms)
    {
        var sortedPlatforms = platforms.OrderBy(p => p.Y).ToList();
        var occupiedPositions = new HashSet<(int X, int Y)>(platforms.Select(p => (p.X, p.Y)));
        var toCheck = sortedPlatforms.Where(_ => _.Type == RockType.Round).ToList();
        foreach (var platform in toCheck)
        {
            occupiedPositions.Remove((platform.X, platform.Y));

            while (platform.Y > 0 && !occupiedPositions.Contains((platform.X, platform.Y - 1)))
            {
                platform.Y--;
            }

            occupiedPositions.Add((platform.X, platform.Y));
        }
    }

    private static void TiltSouth(List<Platform> platforms)
    {
        var sortedPlatforms = platforms.OrderByDescending(p => p.Y).ToList();
        var occupiedPositions = new HashSet<(int X, int Y)>(platforms.Select(p => (p.X, p.Y)));
        var maxY = platforms.Max(_ => _.Y);
        var toCheck = sortedPlatforms.Where(_ => _.Type == RockType.Round).ToList();
        foreach (var platform in toCheck)
        {
            occupiedPositions.Remove((platform.X, platform.Y));

            while (platform.Y < maxY && !occupiedPositions.Contains((platform.X, platform.Y + 1)))
            {
                platform.Y++;
            }

            occupiedPositions.Add((platform.X, platform.Y));
        }
    }

    private static void TiltWest(List<Platform> platforms)
    {
        var sortedPlatforms = platforms.OrderBy(p => p.X).ToList();
        var occupiedPositions = new HashSet<(int X, int Y)>(platforms.Select(p => (p.X, p.Y)));
        var toCheck = sortedPlatforms.Where(_ => _.Type == RockType.Round).ToList();
        foreach (var platform in toCheck)
        {
            occupiedPositions.Remove((platform.X, platform.Y));

            while (platform.X > 0 && !occupiedPositions.Contains((platform.X - 1, platform.Y)))
            {
                platform.X--;
            }

            occupiedPositions.Add((platform.X, platform.Y));
        }
    }

    private static void TiltEast(List<Platform> platforms)
    {
        var sortedPlatforms = platforms.OrderByDescending(p => p.X).ToList();
        var occupiedPositions = new HashSet<(int X, int Y)>(platforms.Select(p => (p.X, p.Y)));
        var maxX = platforms.Max(_ => _.X);
        var toCheck = sortedPlatforms.Where(_ => _.Type == RockType.Round).ToList();
        foreach (var platform in toCheck)
        {
            occupiedPositions.Remove((platform.X, platform.Y));

            while (platform.X < maxX && !occupiedPositions.Contains((platform.X + 1, platform.Y)))
            {
                platform.X++;
            }

            occupiedPositions.Add((platform.X, platform.Y));
        }
    }

    //private static void TiltNorth(List<Platform> platforms)
    //{
    //    var targets = platforms.Where(_ => _.Type == RockType.Round).OrderBy(_ => _.Y);
    //    foreach (var platform in targets)
    //    {
    //        while (platform.Y > 0)
    //        {
    //            if (platforms.Any(_ => _.X == platform.X && _.Y == platform.Y - 1))
    //            {
    //                break;
    //            }
    //            platform.Y--;
    //        }
    //    }
    //}

    //private static void TiltSouth(List<Platform> platforms)
    //{
    //    var targets = platforms.Where(_ => _.Type == RockType.Round).OrderByDescending(_ => _.Y);
    //    foreach (var platform in targets)
    //    {
    //        while (platform.Y < platforms.Max(_ => _.Y))
    //        {
    //            if (platforms.Any(_ => _.X == platform.X && _.Y == platform.Y + 1))
    //            {
    //                break;
    //            }
    //            platform.Y++;
    //        }
    //    }
    //}

    //private static void TiltWest(List<Platform> platforms)
    //{
    //    var targets = platforms.Where(_ => _.Type == RockType.Round).OrderBy(_ => _.X);
    //    foreach (var platform in targets)
    //    {
    //        while (platform.X > 0)
    //        {
    //            if (platforms.Any(_ => _.X == platform.X - 1 && _.Y == platform.Y))
    //            {
    //                break;
    //            }
    //            platform.X--;
    //        }
    //    }
    //}

    //private static void TiltEast(List<Platform> platforms)
    //{
    //    var targets = platforms.Where(_ => _.Type == RockType.Round).OrderByDescending(_ => _.X);
    //    foreach (var platform in targets)
    //    {
    //        while (platform.X < platforms.Max(_ => _.X))
    //        {
    //            if (platforms.Any(_ => _.X == platform.X + 1 && _.Y == platform.Y))
    //            {
    //                break;
    //            }
    //            platform.X++;
    //        }
    //    }
    //}
}