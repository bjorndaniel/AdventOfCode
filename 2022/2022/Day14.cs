namespace AoC2022;
public static class Day14
{
    public static (char?[,] matrix, int floorLevel) ParseInput(string filename, bool isPart2 = false)
    {
        var lines = File.ReadAllLines(filename);
        var coordList = new List<CoOrdList>();
        var floorLevel = 0;
        foreach (var l in lines)
        {
            var coords = l
                .Split("->")
                .Select(_ => _.Trim())
                .Select(_ =>
                    new Point(
                        int.Parse(_.Split(',')[0]),
                        int.Parse(_.Split(',')[1])
                    )
                );
            coordList.Add(new CoOrdList(coords));
        }

        var rowCount = coordList.Max(_ => _.Coords.Max(c => c.Y)) + 1;
        if (isPart2)
        {
            rowCount += 2;
            floorLevel = rowCount - 1;
        }
        var matrix = new char?[rowCount, coordList.Max(_ => _.Coords.Max(c => c.X)) + 1];
        foreach (var c in coordList)
        {
            for (int i = 1; i < c.Coords.Count(); i++)
            {
                var start = c.Coords.ElementAt(i - 1);
                var to = c.Coords.ElementAt(i);
                for (int row = int.Min(start.Y, to.Y); row <= int.Max(start.Y, to.Y); row++)
                {
                    for (int col = int.Min(start.X, to.X); col <= int.Max(start.X, to.X); col++)
                    {
                        matrix[row, col] = '#';
                        if (floorLevel < row)
                        {
                            floorLevel = row;
                        }
                    }
                }
            }
        }
        return (matrix, floorLevel);
    }

    public static (int result, char?[,] matrix) SolvePart1(string filename)
    {
        var (matrix, floorLevel) = ParseInput(filename);
        return DropSand(matrix, floorLevel);
    }

    public static (int result, char?[,] matrix) SolvePart2(string filename)
    {
        var (matrix, floorLevel) = ParseInput(filename, true);
        for (int col = 0; col < matrix.GetLength(1); col++)
        {
            matrix[floorLevel, col] = '#';
        }
        return DropSand(matrix, floorLevel, true);
    }

    private static (int result, char?[,] matrix) DropSand(char?[,] matrix, int floorLevel, bool isPart2 = false)
    {
        var intoTheVoid = false;
        var sandUnits = 0;
        while (!intoTheVoid)
        {
            var dropPoint = new Point(500, 0);
            var dropIt = true;
            var y = 0;
            var x = dropPoint.X;
            while (dropIt)
            {
                if (x == 0 || x > matrix.GetLength(1) - 4 && isPart2)
                {
                    matrix = ExpandMatrix(matrix, x == 0);
                    x = x == 0 ? 1 : x;
                    break;
                }
                if (y >= floorLevel)
                {
                    intoTheVoid = true;
                    break;
                }
                if (y == 0 && x == dropPoint.X && matrix[y, x] == 'o')
                {
                    intoTheVoid = true;
                    break;
                }

                if (matrix[y + 1, x] == 'o' || matrix[y + 1, x] == '#')
                {
                    //Try left
                    if (x - 1 >= 0 && (matrix[y + 1, x - 1] == '#' && matrix[y + 1, x - 1] == 'o'))
                    {
                        matrix[y, x - 1] = 'o';
                        sandUnits++;
                        break;
                    }
                    else if (x - 1 >= 0 && matrix[y + 1, x - 1] == null)
                    {
                        x -= 1;
                        y++;
                        continue;
                    }
                    //Try right
                    if (x + 1 < matrix.GetLength(1) && (matrix[y + 1, x + 1] == '#' && matrix[y + 1, x + 1] == 'o'))
                    {
                        matrix[y, x + 1] = 'o';
                        sandUnits++;
                        break;
                    }
                    else if (x + 1 < matrix.GetLength(1) && matrix[y + 1, x + 1] == null)
                    {
                        x += 1;
                        y++;
                        continue;
                    }
                    //Tried left and right and both are blocked
                    matrix[y, x] = 'o';
                    sandUnits++;
                    break;
                }
                y++;
            }
        }
        return (sandUnits, matrix);
        char?[,] ExpandMatrix(char?[,] matrix, bool left)
        {
            var newMatrix = new char?[matrix.GetLength(0), matrix.GetLength(1) + 1];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (left)
                    {
                        newMatrix[row, col + 1] = matrix[row, col];
                    }
                    else
                    {
                        newMatrix[row, col] = matrix[row, col];
                    }
                }
            }
            for (int col = 0; col < newMatrix.GetLength(1); col++)
            {
                newMatrix[floorLevel, col] = '#';
            }
            return newMatrix;
        }

    }
}
public record CoOrdList(IEnumerable<Point> Coords) { }
