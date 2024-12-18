namespace AoC2024;
public class Day14
{
    public static (List<Robot> robots, (int x, int y) gridSize) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new List<Robot>();
        var (x, y) = (0, 0);
        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var start = parts[0].Replace("p=", "").Split(",");
            var velocity = parts[1].Replace("v=", "").Split(",");
            result.Add(new Robot(int.Parse(start[0]), int.Parse(start[1]), int.Parse(velocity[0]), int.Parse(velocity[1])));
        }
        if (filename.Contains("test"))
        {
            return (result, (11, 7));
        }
        return (result, (101, 103));
    }

    [Solveable("2024/Puzzles/Day14.txt", "Day 14 part 1", 14)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (robots, gridSize) = ParseInput(filename);
        var positions = new List<(int x, int y)>();
        var result = 0L;
        foreach (var robot in robots)
        {
            var (x, y) = robot.CalculatePositionAt(100, gridSize);
            positions.Add((x, y));
        }

        var midX = gridSize.x / 2;
        var midY = gridSize.y / 2;

        var topLeft = positions.Count(p => p.x < midX && p.y < midY && p.x != midX && p.y != midY);
        var topRight = positions.Count(p => p.x >= midX && p.y < midY && p.x != midX && p.y != midY);
        var bottomLeft = positions.Count(p => p.x < midX && p.y >= midY && p.x != midX && p.y != midY);
        var bottomRight = positions.Count(p => p.x >= midX && p.y >= midY && p.x != midX && p.y != midY);

        result = topLeft * topRight * bottomLeft * bottomRight;

        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day14.txt", "Day 14 part 2", 14, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (robots, gridSize) = ParseInput(filename);

        var grid = new char[gridSize.x, gridSize.y];
        var time = 0L;
        var timeStep = 1L;
        var found = false;

        while (!found)
        {
            // Reset the grid
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    grid[i, j] = '.';
                }
            }

            // Calculate positions of robots at the current time
            var positions = new List<(int x, int y)>();
            foreach (var robot in robots)
            {
                var (x, y) = robot.CalculatePositionAt(time, gridSize);
                positions.Add((x, y));
                grid[x, y] = 'O';
            }

            // Check for 2x4 square with at least 8 robots
            for (int i = 0; i < gridSize.x - 1; i++)
            {
                for (int j = 0; j < gridSize.y - 3; j++)
                {
                    var count = 0;
                    for (int dx = 0; dx < 2; dx++)
                    {
                        for (int dy = 0; dy < 4; dy++)
                        {
                            if (grid[i + dx, j + dy] == 'O')
                            {
                                count++;
                            }
                        }
                    }
                    if (count >= 8)
                    {
                        found = true;
                        break;
                    }
                }
                if (found) break;
            }

            if (!found)
            {
                time += timeStep;
            }
        }

        // Print the grid with the robots' positions at the found time
        printer.PrintMatrixXY(grid);
        return new SolutionResult(time.ToString());
    }
}

    public record Robot(int StartX, int StartY, int VelocityX, int VelocityY)
{
    public (int x, int y) CalculatePositionAt(long time, (int x, int y) gridSize)
    {
        var newX = (StartX + VelocityX * time) % gridSize.x;
        var newY = (StartY + VelocityY * time) % gridSize.y;

        if (newX < 0)
        {
            newX += gridSize.x;
        }
        if (newY < 0)
        {
            newY += gridSize.y;
        }
        return ((int)newX, (int)newY);
    }
}