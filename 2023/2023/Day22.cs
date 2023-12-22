namespace AoC2023;
public class Day22
{
    public static List<Brick> ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Brick>();
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var counter = 0;
        foreach (var line in lines)
        {
            var parts = line.Split('~');
            var coords1 = parts[0].Split(',');
            var coords2 = parts[1].Split(',');
            var name = filename.Contains("test") ? letters[counter++].ToString() : Guid.NewGuid().ToString();
            var brick = new Brick
                (
                    name,
                    int.Parse(coords1[0]),
                    int.Parse(coords1[1]),
                    int.Parse(coords1[2]),
                    int.Parse(coords2[0]),
                    int.Parse(coords2[1]),
                    int.Parse(coords2[2])
            );
            result.Add(brick);
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day22.txt", "Day 22 part 1", 22)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var bricks = ParseInput(filename);
        SettleBricks(bricks);
        var result = new Dictionary<Brick, (List<string> supporting, List<string> supportedBy)>();
        bricks.ForEach(b => result.Add(b, ([], [])));
        foreach (var b1 in bricks)
        {
            foreach (var b2 in bricks)
            {
                if (b1 == b2)
                {
                    continue;
                }
                if (b1.IsSupporting(b2))
                {
                    result[b1].supporting.Add(b2.Id);
                    result[b2].supportedBy.Add(b1.Id);
                }
            }
        }
        var disintegrate = 0 ;
        foreach (var b in result)
        {
            if(b.)
        }
        
        return new SolutionResult(disintegrate.ToString());
    }

    [Solveable("2023/Puzzles/Day22.txt", "Day 22 part 2", 22)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        return new SolutionResult("");
    }

    public static void SettleBricks(List<Brick> bricks)
    {
        var moved = false;
        do
        {
            moved = false;
            foreach (var brick in bricks)
            {
                if (CanMoveDown(brick, bricks))
                {
                    brick.Z1--;
                    brick.Z2--;
                    moved = true;
                }
            }
        } while (moved);

        static bool CanMoveDown(Brick brick, List<Brick> bricks)
        {
            if (brick.Z1 == 1) return false;

            foreach (var other in bricks)
            {
                if (other == brick) continue;

                if (other.Z2 == brick.Z1 - 1 &&
                    other.X1 <= brick.X2 && other.X2 >= brick.X1 &&
                    other.Y1 <= brick.Y2 && other.Y2 >= brick.Y1)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Brick(string id, int x1, int y1, int z1, int x2, int y2, int z2)
    {
        public string Id { get; } = id;
        public int X1 { get; set; } = x1;
        public int Y1 { get; set; } = y1;
        public int Z1 { get; set; } = z1;
        public int X2 { get; set; } = x2;
        public int Y2 { get; set; } = y2;
        public int Z2 { get; set; } = z2;
        public int SizeX => X2 - X1 + 1;
        public int SizeY => Y2 - Y1 + 1;
        public int SizeZ => Z2 - Z1 + 1;

        public bool IsSupporting(Brick other) =>
                Z1 < other.Z1 && Z2 < other.Z1 
                && (other.Z1 - Z2) <= 1 &&
                ((X1 == other.X1 || X2 == other.X2)
                || (Y1 == other.Y1 || Y2 == other.Y2));
    }
}