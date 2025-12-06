namespace AoC2025;

public class Day4
{
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var grid = new char[lines.First().Length, lines.Length];
        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines.First().Length; col++)
            {
                grid[col, row] = lines[row][col];
            }
        }
        return grid;
    }

    [Solveable("2025/Puzzles/Day4.txt", "Day 4 part 1", 4)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var count = FindAndMarkPositionsToRemove(grid);

        return new SolutionResult(count.Count.ToString());
    }
    

    [Solveable("2025/Puzzles/Day4.txt", "Day 4 part 2", 4)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var totalRemoved = 0;
        
        while (true)
        {
            var removedPositions = FindAndMarkPositionsToRemove(grid);
            
            if (removedPositions.Count == 0)
            {
                break; 
            }
            foreach (var (x, y) in removedPositions)
            {
                grid[x, y] = '.';
            }
            
            totalRemoved += removedPositions.Count;
        }
        
        return new SolutionResult(totalRemoved.ToString());
    }

    private static List<(int x, int y)> FindAndMarkPositionsToRemove(char[,] grid)
    {
        var width = grid.GetLength(0);
        var height = grid.GetLength(1);
        var positionsToRemove = new List<(int x, int y)>();
        
        var directions = new (int dx, int dy)[]
        {
            (0, -1), 
            (1, -1),
            (1, 0),
            (1, 1),
            (0, 1),
            (-1, 1),
            (-1, 0),
            (-1, -1)
        };

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != '@')
                {
                    continue;
                }

                var neighborCount = directions.Count(dir =>
                {
                    var newX = x + dir.dx;
                    var newY = y + dir.dy;
                    return newX >= 0 && newX < width &&
                           newY >= 0 && newY < height &&
                           grid[newX, newY] == '@';
                });

                if (neighborCount < 4)
                {
                    positionsToRemove.Add((x, y));
                }
            }
        }

        return positionsToRemove;
    }
}