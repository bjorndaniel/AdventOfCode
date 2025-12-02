namespace AoC2015;

public class Day18
{
    public static char[,] ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var rows = lines.Length;
        var cols = lines.First().Length;
        var result = new char[rows, cols];
        for (var row = 0; row < lines.Count(); row++)
        {
            var line = lines[row];
            for (var col = 0; col < line.Length; col++)
            {
                result[row, col] = line[col];
            }
        }
        return result;
    }

    [Solveable("2015/Puzzles/Day18.txt", "Day18 part1", 18)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var stepCount = filename.Contains("test") ? 4 : 100;
        for (var steps = 0; steps < stepCount; steps++)
        {
            CheckLights(grid, keepCornersOn: false);
        }
        var onCount = 0;
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c] == '#')
                {
                    onCount++;
                }
            }
        }
        return new SolutionResult(onCount.ToString());
    }

    [Solveable("2015/Puzzles/Day18.txt", "Day18 part2", 18)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var grid = ParseInput(filename);
        var stepCount = filename.Contains("test") ? 5 : 100;
        // Turn on the four corners before starting
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        grid[0, 0] = '#';
        grid[0, cols - 1] = '#';
        grid[rows - 1, 0] = '#';
        grid[rows - 1, cols - 1] = '#';

        for (var steps = 0; steps < stepCount; steps++)
        {
            CheckLights(grid, keepCornersOn: true);
        }

        var onCount = 0;
        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c] == '#')
                {
                    onCount++;
                }
            }
        }
        return new SolutionResult(onCount.ToString());
    }

    private static void CheckLights(char[,] currentLights, bool keepCornersOn = false)
    {
        var rows = currentLights.GetLength(0);
        var cols = currentLights.GetLength(1);
        var next = new char[rows, cols];

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var onNeighbors = 0;
                for (var dr = -1; dr <= 1; dr++)
                {
                    for (var dc = -1; dc <= 1; dc++)
                    {
                        if (dr == 0 && dc == 0)
                        {
                            continue;
                        }
                        var nr = row + dr;
                        var nc = col + dc;
                        if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
                        {
                            continue;
                        }
                        if (currentLights[nr, nc] == '#')
                        {
                            onNeighbors++;
                        }
                    }
                }

                if (currentLights[row, col] == '#')
                {
                    if (onNeighbors == 2 || onNeighbors == 3)
                    {
                        next[row, col] = '#';
                    }
                    else
                    {
                        next[row, col] = '.';
                    }
                }
                else
                {
                    if (onNeighbors == 3)
                    {
                        next[row, col] = '#';
                    }
                    else
                    {
                        next[row, col] = '.';
                    }
                }
            }
        }

        if (keepCornersOn)
        {
            next[0, 0] = '#';
            next[0, cols - 1] = '#';
            next[rows - 1, 0] = '#';
            next[rows - 1, cols - 1] = '#';
        }

        // Copy next state back into currentLights
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                currentLights[r, c] = next[r, c];
            }
        }
    }
}