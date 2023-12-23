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
            var name = filename.Contains("test") ? letters[counter].ToString() : counter.ToString();
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
            counter++;
        }
        return result;
    }

    [Solveable("2023/Puzzles/Day22.txt", "Day 22 part 1", 22)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var bricks = ParseInput(filename);
        bricks = bricks.OrderBy(b => b.Z1).ToList();
        SettleBricks(bricks);
        bricks = bricks.OrderBy(b => b.Z1).ToList();

        //This part was learned from https://www.youtube.com/watch?v=imz7uexX394&t=723s
        var keySupportsValues = new Dictionary<int, List<int>>();
        var valuesSupportKey = new Dictionary<int, List<int>>();
        foreach (var (brick, i) in bricks.WithIndex())
        {
            keySupportsValues[i] = new List<int>();
            valuesSupportKey[i] = new List<int>();
        }
        foreach (var (upper, j) in bricks.WithIndex())
        {
            foreach(var (lower,i) in bricks[..j].WithIndex())
            {
                if (lower.IsSupporting(upper))
                {
                    keySupportsValues[i].Add(j);
                    valuesSupportKey[j].Add(i);
                }
            }   
        }
        var total = 0;
        foreach(var (brick, i) in bricks.WithIndex())
        {
            var support = true;
            foreach(var s in keySupportsValues[i])
            {
                support = support && valuesSupportKey[s].Count >= 2;   
            }
            if (support)
            {
                total += 1;
            }
        }
        return new SolutionResult(total.ToString());

    }

    [Solveable("2023/Puzzles/Day22.txt", "Day 22 part 2", 22)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var bricks = ParseInput(filename);
        bricks = bricks.OrderBy(b => b.Z1).ToList();
        SettleBricks(bricks);
        bricks = bricks.OrderBy(b => b.Z1).ToList();

        //This part was learned from https://www.youtube.com/watch?v=imz7uexX394&t=723s
        var keySupportsValues = new Dictionary<int, List<int>>();
        var valuesSupportKey = new Dictionary<int, List<int>>();
        foreach(var (brick, i) in bricks.WithIndex())
        {
            keySupportsValues[i] = new List<int>();
            valuesSupportKey[i] = new List<int>();
        }

        foreach (var (upper, j) in bricks.WithIndex())
        {
            foreach (var (lower, i) in bricks[..j].WithIndex())
            {
                if (lower.IsSupporting(upper))
                {
                    keySupportsValues[i].Add(j);
                    valuesSupportKey[j].Add(i);
                }
            }
        }
        var total = 0;
        foreach(var (b,i) in bricks.WithIndex())
        {
            var q = new Queue<int>();
            foreach (var j in keySupportsValues[i].Where(j => valuesSupportKey[j].Count == 1))
            {
                q.Enqueue(j);
            };
            
            q.Enqueue(i);
            var falling = new HashSet<int>
            {
                i
            };
            while(q.Count > 0)
            {
                var j = q.Dequeue();
                foreach (var k in keySupportsValues[j].Where(_ => !falling.Contains(_)))
                {
                    if (valuesSupportKey[k].All(_ => falling.Contains(_)))
                    {
                        q.Enqueue(k);
                        falling.Add(k);
                    }
                }
            }
            total += falling.Count - 1;
        }

        return new SolutionResult(total.ToString());
    }

    public static void SettleBricks(List<Brick> bricks)
    {
        bool moved;
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
    }

    static bool CanMoveDown(Brick brick, List<Brick> bricks)
    {
        if (brick.Z1 == 1)
        {
            return false;
        }

        foreach (var other in bricks)
        {
            if (other.Id == brick.Id)
            {
                continue;
            }

            if (other.Z2 == brick.Z1 - 1 && brick.IsOverlapping(other))
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
    public List<Brick> Supporting { get; set; } = new();

    public bool IsSupporting(Brick upper) =>
        IsOverlapping(upper) && Z2 + 1 == upper.Z1;

    public bool IsOverlapping(Brick other) =>
        X1 <= other.X2 && X2 >= other.X1 && Y1 <= other.Y2 && Y2 >= other.Y1;

}
