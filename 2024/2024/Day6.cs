namespace AoC2024;
public class Day6
{
    public static (char[,] grid, (int y, int x) start) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var result = new char[lines.Length, lines.First().Length];
        var start = (0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines.First().Length; j++)
            {
                result[i, j] = lines[i][j];
                if (result[i,j] == '^')
                {
                    start = (i, j);
                }

            }
        }
        return (result, start);
    }

    [Solveable("2024/Puzzles/Day6.txt", "Day 6 part 1", 6)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var result = 0;
        var (grid, position) = ParseInput(filename);
        var direction = grid[position.y, position.x];
        while (IsInsideGrid(grid, position))
        {
            (int y, int x) nextPosition = direction switch
            {
                '^' => (position.y - 1, position.x),
                'v' => (position.y + 1, position.x),
                '>' => (position.y, position.x + 1),
                '<' => (position.y, position.x - 1),
                _ => position
            };

            if (IsInsideGrid(grid, nextPosition) is false)
            {
                result++;
                break;
            }
            else if (grid[nextPosition.y, nextPosition.x] == '#')
            {
                direction = direction switch
                {
                    '^' => '>',
                    'v' => '<',
                    '>' => 'v',
                    '<' => '^',
                    _ => direction
                };
                grid[position.y, position.x] = 'X';
                nextPosition = direction switch
                {
                    '^' => (position.y - 1, position.x),
                    'v' => (position.y + 1, position.x),
                    '>' => (position.y, position.x + 1),
                    '<' => (position.y, position.x - 1),
                    _ => position
                };
            }
            else if (grid[nextPosition.y, nextPosition.x] == '.')
            {
                result++;
                grid[position.y, position.x] = 'X';
                position = nextPosition;
            }
            else if (grid[nextPosition.y, nextPosition.x] == 'X')
            {
                grid[position.y, position.x] = 'X';
                position = nextPosition;
            }
        }

        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day6.txt", "Day 6 part 2", 6)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (originalGrid, start) = ParseInput(filename);
        var infiniteLoopPositions = new List<(int y, int x)>();

        for (int i = 0; i < originalGrid.GetLength(0); i++)
        {
            for (int j = 0; j < originalGrid.GetLength(1); j++)
            {
                if (originalGrid[i, j] == '.')
                {
                    var grid = (char[,])originalGrid.Clone();
                    grid[i, j] = '#';
                    if (CausesInfiniteLoop(grid, start))
                    {
                        infiniteLoopPositions.Add((i, j));
                        printer.Print(infiniteLoopPositions.Count().ToString());
                        printer.Flush();
                    }
                }
            }
        }
        return new SolutionResult(infiniteLoopPositions.Distinct().Count().ToString());
    }

    private static bool CausesInfiniteLoop(char[,] grid, (int y, int x) start)
    {
        //TODO: Refactor this horribly inefficient code :)
        var visited = new HashSet<((int y, int x) position, char direction)>();
        var path = new List<((int y, int x) position, char direction)>();
        var direction = grid[start.y, start.x];
        var position = start;
        grid[start.y, start.x] = 'X';

        while (IsInsideGrid(grid, position))
        {
            var currentStep = (position, direction);
            path.Add(currentStep);

            if (path.Count >= 4)
            {
                var lastFourSteps = path.Skip(path.Count - 4).ToList();
                if (path.Take(path.Count - 4).Contains(lastFourSteps[0]) &&
                    path.Take(path.Count - 4).Contains(lastFourSteps[1]) &&
                    path.Take(path.Count - 4).Contains(lastFourSteps[2]) &&
                    path.Take(path.Count - 4).Contains(lastFourSteps[3]))
                {
                    return true;
                }
            }

            (int y, int x) nextPosition = direction switch
            {
                '^' => (position.y - 1, position.x),
                'v' => (position.y + 1, position.x),
                '>' => (position.y, position.x + 1),
                '<' => (position.y, position.x - 1),
                _ => position
            };

            if (IsInsideGrid(grid, nextPosition) is false)
            {
                break;
            }
            else if (grid[nextPosition.y, nextPosition.x] == '#')
            {
                direction = direction switch
                {
                    '^' => '>',
                    'v' => '<',
                    '>' => 'v',
                    '<' => '^',
                    _ => direction
                };
                nextPosition = direction switch
                {
                    '^' => (position.y - 1, position.x),
                    'v' => (position.y + 1, position.x),
                    '>' => (position.y, position.x + 1),
                    '<' => (position.y, position.x - 1),
                    _ => position
                };
            }
            else if (grid[nextPosition.y, nextPosition.x] == '.')
            {
                position = nextPosition;
            }
            else if (grid[nextPosition.y, nextPosition.x] == 'X')
            {
                position = nextPosition;
            }
        }

        return false;
    }

    private static bool IsInsideGrid(char[,] grid, (int y, int x) position)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        return position.y >= 0 && position.y < rows && position.x >= 0 && position.x < cols;
    }
}