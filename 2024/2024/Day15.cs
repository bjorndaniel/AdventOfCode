namespace AoC2024;
public class Day15
{
    public static (char[,] grid, List<char> moves, (int x, int y) robotStart) ParseInput(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var count = 0;
        var moves = new List<char>();
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                break;
            }
            count++;
        }
        var robotStart = (0, 0);
        var grid = new char[count, lines.First().Length];
        for (int row = 0; row < count; row++)
        {
            for (int col = 0; col < lines.First().Length; col++)
            {
                grid[row, col] = lines[row][col];
                if (grid[row, col] == '@')
                {
                    robotStart = (col, row);
                }
            }
        }

        for (int i = count; i < lines.Length; i++)
        {
            foreach (var c in lines[i])
            {
                moves.Add(c);
            }
        }
        return (grid, moves, robotStart);
    }

    [Solveable("2024/Puzzles/Day15.txt", "Day 15 part 1", 15)]
    public static SolutionResult Part1(string filename, IPrinter printer)
    {
        var (grid, moves, robotStart) = ParseInput(filename);
        var resultGrid = BFS(grid, moves, robotStart, printer);

        //printer.PrintMatrixYX(resultGrid);
        //printer.Flush();

        var boxPositions = FindBoxPositions(resultGrid);
        var result = boxPositions.Sum(p => p.x + p.y * 100);
        return new SolutionResult(result.ToString());
    }

    [Solveable("2024/Puzzles/Day15.txt", "Day 15 part 2", 15, true)]
    public static SolutionResult Part2(string filename, IPrinter printer)
    {
        var (grid, moves, robotStart) = ParseInput(filename);
        var (newGrid, newRobotPosition) = ScaleUp(filename, printer);

        //printer.PrintMatrixYX(newGrid);
        //printer.Flush();
        var gridItems = GetGridItems(newGrid);

        var (finalGridItems, finalRobotPosition) = BFSPart2(gridItems, moves, newRobotPosition, printer);

        var resultGrid = GetGrid(finalGridItems, finalRobotPosition);
        //printer.PrintMatrixYX(resultGrid);
        //printer.Flush();

        var boxPositions = FindBoxPositions(resultGrid, true);
        var result = boxPositions.Sum(p => p.x + p.y * 100);
        return new SolutionResult(result.ToString());
    }

    public static (char[,] newGrid, (int x, int y) newRobotPosition) ScaleUp(string filename, IPrinter printer)
    {
        var (grid, _, robotStart) = ParseInput(filename);
        int originalRows = grid.GetLength(0);
        int originalCols = grid.GetLength(1);
        var scaledRows = new List<string>();
        (int x, int y) newRobotPosition = (0, 0);

        for (int row = 0; row < originalRows; row++)
        {
            var scaledRow = new StringBuilder();
            for (int col = 0; col < originalCols; col++)
            {
                switch (grid[row, col])
                {
                    case '#':
                        scaledRow.Append("##");
                        break;
                    case '.':
                        scaledRow.Append("..");
                        break;
                    case 'O':
                        scaledRow.Append("[]");
                        break;
                    case '@':
                        scaledRow.Append("@.");
                        newRobotPosition = (scaledRow.Length - 2, row);
                        break;
                    default:
                        scaledRow.Append(grid[row, col]);
                        break;
                }
            }
            scaledRows.Add(scaledRow.ToString());
        }

        var newCols = scaledRows.Max(row => row.Length);
        var newGrid = new char[originalRows, newCols];
        for (int row = 0; row < originalRows; row++)
        {
            for (int col = 0; col < scaledRows[row].Length; col++)
            {
                newGrid[row, col] = scaledRows[row][col];
            }
        }
        //printer.PrintMatrixYX(newGrid);
        //printer.Flush();
        return (newGrid, newRobotPosition);
    }

    private static char[,] BFS(char[,] grid, List<char> moves, (int x, int y) robotStart, IPrinter printer)
    {
        var directions = new Dictionary<char, (int, int)>
        {
            { '<', (0, -1) },
            { '>', (0, 1) },
            { '^', (-1, 0) },
            { 'v', (1, 0) }
        };

        var queue = new Queue<((int x, int y) position, int moveIndex)>();
        queue.Enqueue((robotStart, 0));
        var visited = new HashSet<((int x, int y), int index)>
        {
            (robotStart, 0)
        };

        while (queue.Count > 0)
        {
            var (currentPosition, moveIndex) = queue.Dequeue();
            if (moveIndex == moves.Count)
            {
                return grid;
            }

            var move = moves[moveIndex];
            var (dy, dx) = directions[move];
            var newPosition = (currentPosition.x + dx, currentPosition.y + dy);

            if (IsValidMove(grid, currentPosition, newPosition))
            {
                MoveRobot(grid, currentPosition, newPosition, (dx, dy));
                if (!visited.Contains((newPosition, moveIndex + 1)))
                {
                    queue.Enqueue((newPosition, moveIndex + 1));
                    visited.Add((newPosition, moveIndex + 1));
                }
            }
            else
            {
                if (!visited.Contains((currentPosition, moveIndex + 1)))
                {
                    queue.Enqueue((currentPosition, moveIndex + 1));
                    visited.Add((currentPosition, moveIndex + 1));
                }
            }
            //printer.PrintMatrixYX(grid);
            //printer.Flush();
        }

        return grid;
    }

    private static List<(int x, int y)> FindBoxPositions(char[,] grid, bool part2 = false)
    {
        var boxPositions = new List<(int x, int y)>();
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == 'O' || (part2 && grid[row, col] == '['))
                {
                    boxPositions.Add((col, row));
                }
            }
        }
        return boxPositions;
    }

    private static void MoveRobot(char[,] grid, (int x, int y) currentPosition, (int x, int y) newPosition, (int x, int y) direction)
    {
        if (grid[newPosition.y, newPosition.x] == 'O')
        {
            MoveBox(grid, newPosition, direction);
        }

        grid[newPosition.y, newPosition.x] = '@';
        grid[currentPosition.y, currentPosition.x] = '.';
    }

    private static void MoveBox(char[,] grid, (int x, int y) boxPosition, (int x, int y) direction)
    {
        var newBoxPosition = (boxPosition.x + direction.x, boxPosition.y + direction.y);
        if (grid[newBoxPosition.Item2, newBoxPosition.Item1] == 'O')
        {
            MoveBox(grid, newBoxPosition, direction);
        }
        grid[newBoxPosition.Item2, newBoxPosition.Item1] = 'O';
        grid[boxPosition.y, boxPosition.Item1] = '.';
    }

    private static bool IsValidMove(char[,] grid, (int x, int y) currentPosition, (int x, int y) newPosition)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        if (newPosition.x < 0 || newPosition.x >= cols || newPosition.y < 0 || newPosition.y >= rows)
        {
            return false;
        }

        if (grid[newPosition.y, newPosition.x] == '#')
        {
            return false;
        }

        if (grid[newPosition.y, newPosition.x] == 'O')
        {
            var direction = (newPosition.x - currentPosition.x, newPosition.y - currentPosition.y);
            return CanMoveBox(grid, newPosition, direction);
        }

        return true;
    }

    private static bool CanMoveBox(char[,] grid, (int x, int y) boxPosition, (int x, int y) direction)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        var newBoxPosition = (boxPosition.x + direction.x, boxPosition.y + direction.y);

        if (newBoxPosition.Item2 < 0 || newBoxPosition.Item2 >= rows || newBoxPosition.Item1 < 0 || newBoxPosition.Item1 >= cols)
        {
            return false;
        }

        if (grid[newBoxPosition.Item2, newBoxPosition.Item1] == '#')
        {
            return false;
        }

        if (grid[newBoxPosition.Item2, newBoxPosition.Item1] == 'O')
        {
            return CanMoveBox(grid, newBoxPosition, direction);
        }

        return grid[newBoxPosition.Item2, newBoxPosition.Item1] == '.';
    }

    private static List<GridItem> GetGridItems(char[,] grid)
    {
        var result = new List<GridItem>();
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == '#')
                {
                    result.Add(new GridItem(col, col, row, ItemType.Wall, Guid.NewGuid()));
                }
                else if (grid[row, col] == '[')
                {
                    result.Add(new GridItem(col, col + 1, row, ItemType.Box, Guid.NewGuid()));
                }
                //else if (grid[row, col] == '@')
                //{
                //    result.Add(new GridItem(col, col + 1, row, ItemType.Robot));
                //}
            }
        }
        return result;
    }

    private static char[,] GetGrid(List<GridItem> items, (int x, int y) robotPosition)
    {
        var maxRows = items.Max(_ => _.Y) + 1;
        var maxCols = items.Max(_ => _.RightX) + 1;
        var grid = new char[maxRows, maxCols];
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
            {
                var item = items.FirstOrDefault(_ => _.Y == row && _.LeftX == col);
                if (item != null)
                {
                    switch (item.Type)
                    {
                        case ItemType.Wall:
                            grid[row, col] = '#';
                            break;
                        case ItemType.Box:
                            grid[row, col] = '[';
                            grid[row, col + 1] = ']';
                            col++;
                            break;
                        case ItemType.Robot:
                            grid[row, col] = '@';
                            break;
                    }
                }
                else if (col == robotPosition.x && row == robotPosition.y)
                {
                    grid[row, col] = '@';
                }
                else
                {
                    grid[row, col] = '.';
                }
            }
        }
        return grid;
    }

    private static (List<GridItem> gridItems, (int x, int y) robotPosition) BFSPart2(List<GridItem> gridItems, List<char> moves, (int x, int y) robotStart, IPrinter printer)
    {
        var directions = new Dictionary<char, (int, int)>
    {
        { '<', (0, -1) },
        { '>', (0, 1) },
        { '^', (-1, 0) },
        { 'v', (1, 0) }
    };

        var queue = new Queue<((int x, int y) position, int moveIndex)>();
        queue.Enqueue((robotStart, 0));
        var visited = new HashSet<((int x, int y), int index)>
    {
        (robotStart, 0)
    };

        while (queue.Count > 0)
        {
            var (currentPosition, moveIndex) = queue.Dequeue();
            if (moveIndex == moves.Count)
            {
                return (gridItems, currentPosition);
            }

            var move = moves[moveIndex];
            var (dy, dx) = directions[move];
            var newPosition = (currentPosition.x + dx, currentPosition.y + dy);

            if (IsValidMovePart2(gridItems, currentPosition, newPosition))
            {
                MoveRobotPart2(gridItems, currentPosition, newPosition, (dx, dy));
                if (!visited.Contains((newPosition, moveIndex + 1)))
                {
                    queue.Enqueue((newPosition, moveIndex + 1));
                    visited.Add((newPosition, moveIndex + 1));
                }
                //var grid = GetGrid(gridItems, newPosition);
                //printer.PrintMatrixYX(grid);
                //printer.Flush();
            }
            else
            {
                if (!visited.Contains((currentPosition, moveIndex + 1)))
                {
                    queue.Enqueue((currentPosition, moveIndex + 1));
                    visited.Add((currentPosition, moveIndex + 1));
                }
                //var grid = GetGrid(gridItems, currentPosition);
                //printer.PrintMatrixYX(grid);
                //printer.Flush();
            }

        }

        return (gridItems, robotStart);
    }

    private static bool IsValidMovePart2(List<GridItem> gridItems, (int x, int y) currentPosition, (int x, int y) newPosition)
    {
        var wallItem = gridItems.FirstOrDefault(item => item.Type == ItemType.Wall && item.IsInPosition(newPosition));
        if (wallItem != null)
        {
            return false;
        }

        var boxItem = gridItems.FirstOrDefault(item => item.Type == ItemType.Box && item.IsInPosition(newPosition));
        if (boxItem != null)
        {
            var direction = (newPosition.x - currentPosition.x, newPosition.y - currentPosition.y);
            return CanMoveBoxPart2(gridItems, boxItem, direction);
        }

        return true;
    }

    private static void MoveRobotPart2(List<GridItem> gridItems, (int x, int y) currentPosition, (int x, int y) newPosition, (int x, int y) direction)
    {
        var boxItem = gridItems.FirstOrDefault(item => item.Type == ItemType.Box && item.IsInPosition(newPosition));
        if (boxItem != null)
        {
            MoveBoxPart2(gridItems, boxItem, direction);
        }

        var robotItem = gridItems.FirstOrDefault(item => item.Type == ItemType.Robot);
        if (robotItem != null)
        {
            robotItem.LeftX = newPosition.x;
            robotItem.Y = newPosition.y;
        }
    }

    private static bool CanMoveBoxPart2(List<GridItem> gridItems, GridItem boxItem, (int x, int y) direction, HashSet<GridItem> visited = null)
    {
        if (direction.y == -1)
        {
            var boxAboveLeft = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX && _.Y == boxItem.Y - 1);
            var boxAboveRight = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX && _.Y == boxItem.Y - 1);
            var directlyAbove = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.LeftX && _.Y == boxItem.Y - 1);
            var middleAbove = gridItems.FirstOrDefault(_ => (_.LeftX == boxItem.RightX || _.RightX == boxItem.LeftX) && _.Y == boxItem.Y - 1);

            var wallAbove = gridItems.FirstOrDefault(_ => _.Type == ItemType.Wall && _.Y == boxItem.Y - 1 && (_.LeftX == boxItem.LeftX || _.LeftX == boxItem.RightX));
            if (wallAbove != null)
            {
                return false;
            }
            if (boxAboveLeft != null && boxAboveRight != null)
            {
                var canMoveLeft = CanMoveBoxPart2(gridItems, boxAboveLeft, direction, visited);
                var canMoveRight = CanMoveBoxPart2(gridItems, boxAboveRight, direction, visited);
                if (canMoveLeft && canMoveRight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (directlyAbove != null)
            {
                return CanMoveBoxPart2(gridItems, directlyAbove, direction, visited);
            }
            else if (middleAbove != null)
            {
                return CanMoveBoxPart2(gridItems, middleAbove, direction, visited);
            }
        }
        else if (direction.y == 1)
        {
            var boxBelowLeft = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX && _.Y == boxItem.Y + 1);
            var boxBelowRight = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX && _.Y == boxItem.Y + 1);
            var directlyBelow = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.LeftX && _.Y == boxItem.Y + 1);
            var middleBelow = gridItems.FirstOrDefault(_ => (_.LeftX == boxItem.RightX || _.RightX == boxItem.LeftX) && _.Y == boxItem.Y + 1);
            var wallBelow = gridItems.FirstOrDefault(_ => _.Type == ItemType.Wall && _.Y == boxItem.Y + 1 && (_.LeftX == boxItem.LeftX || _.LeftX == boxItem.RightX));

            if (wallBelow != null)
            {
                return false;
            }

            if (boxBelowLeft != null && boxBelowRight != null)
            {
                var canMoveLeft = CanMoveBoxPart2(gridItems, boxBelowLeft, direction, visited);
                var canMoveRight = CanMoveBoxPart2(gridItems, boxBelowRight, direction, visited);
                if (canMoveLeft && canMoveRight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (directlyBelow != null)
            {
                return CanMoveBoxPart2(gridItems, directlyBelow, direction, visited);
            }
            else if (middleBelow != null)
            {
                return CanMoveBoxPart2(gridItems, middleBelow, direction, visited);
            }

        }
        else if (direction.x == -1)
        {
            var left = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX - 1 && _.Y == boxItem.Y);
            if (left != null)
            {
                return left.Type == ItemType.Wall ? false : CanMoveBoxPart2(gridItems, left, direction, visited);
            }
            return true;
        }
        else
        {
            var right = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX + 1 && _.Y == boxItem.Y);
            if (right != null)
            {
                return right.Type == ItemType.Wall ? false : CanMoveBoxPart2(gridItems, right, direction, visited);
            }
            return true;
        }

        return true;
    }

    private static void MoveBoxPart2(List<GridItem> gridItems, GridItem boxItem, (int x, int y) direction)
    {
        var newBoxPosition = (boxItem.LeftX + direction.x, boxItem.Y + direction.y);

        if (direction.y == -1)
        {
            var boxAboveLeft = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX && _.Y == boxItem.Y - 1);
            var boxAboveRight = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX && _.Y == boxItem.Y - 1);
            var directlyAbove = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.LeftX && _.Y == boxItem.Y - 1);
            var middleAbove = gridItems.FirstOrDefault(_ => (_.LeftX == boxItem.RightX || _.RightX == boxItem.LeftX) && _.Y == boxItem.Y - 1);

            var wallAbove = gridItems.FirstOrDefault(_ => _.Type == ItemType.Wall && _.Y == boxItem.Y - 1 && (_.LeftX == boxItem.LeftX || _.LeftX == boxItem.RightX));
            if (wallAbove != null)
            {
                return;
            }
            if (boxAboveLeft != null && boxAboveRight != null)
            {
                MoveBoxPart2(gridItems, boxAboveLeft, direction);
                MoveBoxPart2(gridItems, boxAboveRight, direction);
            }
            else if (directlyAbove != null)
            {
                MoveBoxPart2(gridItems, directlyAbove, direction);
            }
            else if (middleAbove != null)
            {
                MoveBoxPart2(gridItems, middleAbove, direction);
            }
        }
        else if (direction.y == 1)
        {
            var boxBelowLeft = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX && _.Y == boxItem.Y + 1);
            var boxBelowRight = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX && _.Y == boxItem.Y + 1);
            var directlyBelow = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.LeftX && _.Y == boxItem.Y + 1);
            var middleBelow = gridItems.FirstOrDefault(_ => (_.LeftX == boxItem.RightX || _.RightX == boxItem.LeftX) && _.Y == boxItem.Y + 1);

            var wallBelow = gridItems.FirstOrDefault(_ => _.Type == ItemType.Wall && _.Y == boxItem.Y + 1 && (_.LeftX == boxItem.LeftX || _.LeftX == boxItem.RightX));

            if (wallBelow != null)
            {
                return;
            }

            if (boxBelowLeft != null && boxBelowRight != null)
            {
                MoveBoxPart2(gridItems, boxBelowLeft, direction);
                MoveBoxPart2(gridItems, boxBelowRight, direction);
            }
            else if (directlyBelow != null)
            {
                MoveBoxPart2(gridItems, directlyBelow, direction);
            }
            else if (middleBelow != null)
            {
                MoveBoxPart2(gridItems, middleBelow, direction);
            }
        }
        else if (direction.x == -1)
        {
            var left = gridItems.FirstOrDefault(_ => _.RightX == boxItem.LeftX - 1 && _.Y == boxItem.Y);
            if (left != null)
            {
                MoveBoxPart2(gridItems, left, direction);
            }
        }
        else
        {
            var right = gridItems.FirstOrDefault(_ => _.LeftX == boxItem.RightX + 1 && _.Y == boxItem.Y);
            if (right != null)
            {
                MoveBoxPart2(gridItems, right, direction);
            }
        }

        boxItem.LeftX = newBoxPosition.Item1;
        boxItem.RightX = newBoxPosition.Item1 + 1;
        boxItem.Y = newBoxPosition.Item2;
    }
}

public class GridItem(int leftX, int rightX, int y, ItemType type, Guid id)
{
    public Guid Id { get; init; } = id;
    public int LeftX { get; set; } = leftX;
    public int RightX { get; set; } = rightX;
    public int Y { get; set; } = y;
    public ItemType Type { get; init; } = type;
    public bool IsInPosition((int x, int y) position) =>
        Y == position.y && (LeftX == position.x || RightX == position.x);
}

public enum ItemType
{
    Wall,
    Box,
    Robot
}