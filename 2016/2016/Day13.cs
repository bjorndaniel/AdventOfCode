namespace AoC2016;

public class Day13
{
    [Solveable("2016/Puzzles/Day13.txt", "Day 13 part 1", 13)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var endpoint = filename.Contains("test") ? (7, 4) : (31, 39);
        var favoriteNumber = filename.Contains("test") ? 10 : 1350;

        var steps = FindShortestPath((1, 1), endpoint, favoriteNumber);
        return new SolutionResult(steps.ToString());
    }

    [Solveable("2016/Puzzles/Day13.txt", "Day 13 part 2", 13)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var favoriteNumber = filename.Contains("test") ? 10 : 1350;

        var reachable = CountReachableLocations((1, 1), 50, favoriteNumber);
        return new SolutionResult(reachable.ToString());
    }

    private static int FindShortestPath((int x, int y) start, (int x, int y) end, int favoriteNumber)
    {
        var queue = new Queue<((int x, int y) pos, int steps)>();
        var visited = new HashSet<(int, int)>();
        
        queue.Enqueue((start, 0));
        visited.Add(start);
        
        var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        
        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();
            
            if (current == end)
            {
                return steps;
            }
            
            foreach (var (dx, dy) in directions)
            {
                var next = (x: current.x + dx, y: current.y + dy);
                
                if (next.x < 0 || next.y < 0 || visited.Contains(next))
                {
                    continue;
                }
                
                if (IsWall(next.x, next.y, favoriteNumber))
                {
                    continue;
                }
                
                visited.Add(next);
                queue.Enqueue((next, steps + 1));
            }
        }
        
        return -1; // No path found
    }
    
    private static bool IsWall(int x, int y, int favoriteNumber)
    {
        var value = x * x + 3 * x + 2 * x * y + y + y * y + favoriteNumber;
        var bitCount = CountBits(value);
        return bitCount % 2 == 1; 
    }
    
    private static int CountBits(int n)
    {
        var count = 0;
        while (n > 0)
        {
            count += n & 1;
            n >>= 1;
        }
        return count;
    }

    private static int CountReachableLocations((int x, int y) start, int maxSteps, int favoriteNumber)
    {
        var queue = new Queue<((int x, int y) pos, int steps)>();
        var visited = new HashSet<(int, int)>();
        
        queue.Enqueue((start, 0));
        visited.Add(start);
        
        var directions = new[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
        
        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();
            
            if (steps >= maxSteps)
            {
                continue;
            }
            
            foreach (var (dx, dy) in directions)
            {
                var next = (x: current.x + dx, y: current.y + dy);
                
                if (next.x < 0 || next.y < 0 || visited.Contains(next))
                {
                    continue;
                }
                
                if (IsWall(next.x, next.y, favoriteNumber))
                {
                    continue;
                }
                
                visited.Add(next);
                queue.Enqueue((next, steps + 1));
            }
        }
        
        return visited.Count;
    }
}